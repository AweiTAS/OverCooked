using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace CustomInput
{
    public class Player : MonoBehaviour ,IKitchenObjectParent
    {
        public event EventHandler OnPick;
        public static Player Instance { get; private set; }
        public bool isWalking;

        private const string IS_WALK = "isWalk";

        private AbstractInputReader _abstractInputReader;
        private Vector3 moveDir;
        private Animator animator;
        private BaseCounter selectedCounter;
        private KitchenObject kitchenObject;

        [SerializeField] private float moveSpeed = 8f;
        [SerializeField] private float rotateSpeed = 8f;
        [SerializeField] private float playerHeight = 2f;
        [SerializeField] private float playerRadius = .7f;
        [SerializeField] private float interactDis = 2f;

        [SerializeField] private Transform kitchenObjectHoldPoint;
        public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;
        public class OnSelectedCounterChangedEventArgs : EventArgs
        {
            public BaseCounter selectedCounter;
        }
        private void Awake()
        {
            if (Instance != null) Debug.Log("Error! More than one Player!!!");
            Instance = this;
            _abstractInputReader = GetComponent<AbstractInputReader>();
            animator = GetComponentInChildren<Animator>();
            moveDir.y = 0;
        }
        private void OnEnable()
        {
            _abstractInputReader.OnInteract += Interact;
            _abstractInputReader.OnInteractAlternative += InteractAlt;
        }
        private void OnDisable()
        {
            _abstractInputReader.OnInteract -= Interact;
            _abstractInputReader.OnInteractAlternative -= InteractAlt;

        }
        private void Update()
        {
            MoveAndRotate();
            isWalking = moveDir != Vector3.zero;
            SetAnimator();
            UpdateSelect();
        }
        private void MoveAndRotate()
        {

            float moveDistance = moveSpeed * Time.deltaTime;
            moveDir.x = _abstractInputReader.movement.x;
            if (Physics.CapsuleCast(transform.position,
                transform.position + Vector3.up * playerHeight, playerRadius, moveDir, moveDistance))
                moveDir.x = 0;
            moveDir.z = _abstractInputReader.movement.y;
            if (Physics.CapsuleCast(transform.position,
                transform.position + Vector3.up * playerHeight, playerRadius, moveDir, moveDistance))
                moveDir.z = 0;
            transform.position += moveDir * moveDistance;

            moveDir.x = _abstractInputReader.movement.x;
            moveDir.z = _abstractInputReader.movement.y;
            transform.forward = Vector3.Lerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed);
        }
        private void SetAnimator()
        {
            animator.SetBool(IS_WALK, moveDir != Vector3.zero);
        }
        private void Interact(object sender, EventArgs e)
        {
            if (selectedCounter != null) selectedCounter.Interact(this);
        }
        private void InteractAlt(object sender, EventArgs e)
        {
            if (selectedCounter != null) selectedCounter.InteractAlt(this);
        }
        private void UpdateSelect()
        {
            if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hitInfo, interactDis))
            {
                if (hitInfo.transform.TryGetComponent(out BaseCounter baseCounter))
                {
                    if (baseCounter != selectedCounter)
                    {
                        SetSelectedCounter(baseCounter);
                    }
                    return;
                }
            }
            if(selectedCounter != null) SetSelectedCounter(null);
        }
        private void SetSelectedCounter(BaseCounter baseCounter)
        {
            this.selectedCounter = baseCounter;
            OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs
            {
                selectedCounter = baseCounter
            });
        }

        public Transform GetKitchenObjectFollowTransform()
        {
            return kitchenObjectHoldPoint;
        }
        public KitchenObject GetKitchenObject()
        {
            return kitchenObject;
        }
        public void SetKitchenObject(KitchenObject kitchenObject)
        {
            this.kitchenObject = kitchenObject;
            if(kitchenObject != null)
            {
                OnPick?.Invoke(this, EventArgs.Empty);
            }
        }
        public void ClearKitchenObject()
        {
            kitchenObject = null;
        }
        public bool HasKitchenObject()
        {
            return kitchenObject != null;
        }
    }
}