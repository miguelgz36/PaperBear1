using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Resources : MonoBehaviour
{
    [SerializeField] private float initialBoxResources;
    [SerializeField] private float initialSourcePerSecond;
    [SerializeField] TextMeshProUGUI textBoxes;

    private float currentResources;
    private float currentSourcePerSecond;

    public void ConsumeBox(float amount)
    {
        currentResources -= amount;
    }

    public bool IsOutOfResources()
    {
        return currentResources <= 0;
    }

    private void Start()
    {
        currentResources = initialBoxResources;
        currentSourcePerSecond = initialSourcePerSecond;
        textBoxes.text = currentResources.ToString();
    }

    private void Update()
    {
        currentResources += currentSourcePerSecond * Time.deltaTime;
        textBoxes.text = currentResources.ToString();
    }
}