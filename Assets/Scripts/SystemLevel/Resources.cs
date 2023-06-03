using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Resources : Singleton<Resources>
{
    [SerializeField] private float initialBoxResources;
    [SerializeField] private float initialSourcePerSecond;
    [SerializeField] TextMeshProUGUI textBoxes;

    private float currentResources;
    private float currentSourcePerSecond;

    public float CurrentResources { get => currentResources; set => currentResources = value; }

    protected override void Awake()
    {
        base.Awake();
    }
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
