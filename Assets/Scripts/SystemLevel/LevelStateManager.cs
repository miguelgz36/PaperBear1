using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelStateManager : MonoBehaviour
{

    [SerializeField] private Canvas canvasWin;
    [SerializeField] private Canvas canvasLose;

    private PlacementManager placementManager;


    private void Awake()
    {
        placementManager = FindAnyObjectByType<PlacementManager>();
    }

    public void Win()
    {
        placementManager.enabled = false;
        canvasWin.gameObject.SetActive(true);
    }

    public void Lose()
    {
        placementManager.enabled = false;
        canvasLose.gameObject.SetActive(true);
    }

    public void LoadSameGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Exit()
    {
        Application.Quit();
        Debug.Log("Quiting");
    }
}
