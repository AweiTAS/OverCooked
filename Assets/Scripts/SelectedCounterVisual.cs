using CustomInput;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{
    [SerializeField] private BaseCounter baseCounter;
    [SerializeField] private GameObject visual;
    private void Start()
    {
        Player.Instance.OnSelectedCounterChanged += OnPlayerSelectedCounterChanged;
    }
    private void OnPlayerSelectedCounterChanged(object sender, Player.OnSelectedCounterChangedEventArgs e)
    {
        if(baseCounter == e.selectedCounter)
        {
            visual.SetActive(true);
        }
        else
        {
            visual.SetActive(false);
        }
    }
}
