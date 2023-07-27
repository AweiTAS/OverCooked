using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveSound : MonoBehaviour
{
    [SerializeField] private StoveCounter stoveCounter;
    private AudioSource audioSource;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        stoveCounter.OnStateChangeEvent += StoveCounter_OnStateChangeEvent;
    }

    private void StoveCounter_OnStateChangeEvent(object sender, StoveCounter.OnStateChangeEventArgs e)
    {
        if (e.state)
        {
            audioSource.Play();
        }
        else
        {
            audioSource.Pause();
        }
    }
}
