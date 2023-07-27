using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomInput;
public class PlayerSound : MonoBehaviour
{
    private Player player;
    private float footStepTimer;
    [SerializeField] private float footStepTimerMax = .2f;
    private void Awake()
    {
        player = GetComponent<Player>();
    }
    private void Update()
    {
        if (player.isWalking)
        {
            footStepTimer += Time.deltaTime;
            if (footStepTimer > footStepTimerMax)
            {
                footStepTimer = 0;
                SoundManager.Instance.PlayFootstep(this.transform.position);
            }
        }
        
    }
}
