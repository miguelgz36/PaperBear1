using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelStateManager : Singleton<LevelStateManager>
{

    [SerializeField] private Canvas canvasWin;
    [SerializeField] private Canvas canvasLose;

    protected override void Awake()
    {
        base.Awake();
    }

    public void Win()
    {
        SelectManager.Instance.enabled = false;
        canvasWin.gameObject.SetActive(true);
    }

    public void Lose()
    {
        SelectManager.Instance.enabled = false;
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
