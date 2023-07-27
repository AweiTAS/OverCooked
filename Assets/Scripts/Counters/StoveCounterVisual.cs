using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterVisual : MonoBehaviour
{
    [SerializeField] private StoveCounter stove;
    [SerializeField] private GameObject[] particles;
    private void Start()
    {
        stove.OnStateChangeEvent += Stove_OnStateChangeEvent;
    }

    private void Stove_OnStateChangeEvent(object sender, StoveCounter.OnStateChangeEventArgs e)
    {
        foreach(GameObject particle in particles)
        {
            particle.SetActive(e.state);
        }
    }
}
