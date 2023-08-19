using UnityEngine;

public class SquadCooldown
{
    private readonly Placeable placeable;
    private float currentTime;
    private float currentPopulation;
    private bool isInfiniteCap;
    private string textButton;

    public string TextButton { get => textButton; set => textButton = value; }
    public bool IsInfiniteCap { get => isInfiniteCap; set => isInfiniteCap = value; }
    public float CurrentPopulation { get => currentPopulation; set => currentPopulation = value; }
    public float CurrentTime { get => currentTime; set => currentTime = value; }

    public SquadCooldown(Placeable placeable)
    {
        this.placeable = placeable;
    }

    public void Start()
    {
        currentTime = 0;
        currentPopulation = 0;
        isInfiniteCap = placeable.MaxCapPopulation == 0;
        if (!isInfiniteCap)
        {
            SetTextCap();
        }
    }

    public void Update()
    {
        if ((currentTime < placeable.CoolDown && currentPopulation < placeable.MaxCapPopulation) || isInfiniteCap)
        {
            currentTime += (Time.deltaTime);
        }
        if (!isInfiniteCap)
        {
            SetTextCap();
        }
    }

    public void ResetCooldown()
    {
        currentPopulation++;
        currentTime = 0f;
    }

    public void DecreasePopulation()
    {
        if(currentPopulation >= placeable.MaxCapPopulation)
        {
            currentTime = 0f;
        }
        currentPopulation--;
    }

    public bool CapValid()
    {
        return currentPopulation < placeable.MaxCapPopulation || isInfiniteCap;
    }

    private void SetTextCap()
    {
        textButton = currentPopulation + "/" + placeable.MaxCapPopulation.ToString();
    }
}
