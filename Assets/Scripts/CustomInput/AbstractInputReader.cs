using System;
using UnityEngine;

namespace CustomInput
{
    public abstract class AbstractInputReader : MonoBehaviour
    {
        public event EventHandler OnInteract;
        public event EventHandler OnInteractAlternative;
        [Header("Configruation")]
        public bool hideMouse;

        [Header("Output Signal")]
        public Vector2 movement;
        public bool isSprinting;
        public bool isJumping;
        public bool isCurShowed;
        public bool isESC;

        protected void InvokeOnInteract()
        {
            OnInteract?.Invoke(this, EventArgs.Empty);
        }
        protected void InvokeOnInteractAlternative()
        {
            OnInteractAlternative?.Invoke(this, EventArgs.Empty);
        }
    }
}