using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateCounterVisual : MonoBehaviour
{
    [SerializeField] private PlateCounter plateCounter;
    [SerializeField] private Transform topPoint;
    [SerializeField] private Transform platePrefab;
    [SerializeField] private float plateOffset = 0.1f;

    private List<Transform> plateVisualList;
    private void Awake()
    {
        plateVisualList = new List<Transform>();
    }
    private void Start()
    {
        plateCounter.OnPlateGenerateEvent += PlateCounter_OnPlateGenerateEvent;
        plateCounter.OnPlateRemoveEvent += PlateCounter_OnPlateRemoveEvent;
    }

    private void PlateCounter_OnPlateRemoveEvent(object sender, System.EventArgs e)
    {
        Transform visualTransform = plateVisualList[plateVisualList.Count - 1];
        plateVisualList.Remove(visualTransform);
        Destroy(visualTransform.gameObject);
    }

    private void PlateCounter_OnPlateGenerateEvent(object sender, System.EventArgs e)
    {
        Transform visualTransform = Instantiate(platePrefab, topPoint);

        visualTransform.localPosition = new Vector3(0, plateOffset * plateVisualList.Count, 0);
        plateVisualList.Add(visualTransform);
    }
}
