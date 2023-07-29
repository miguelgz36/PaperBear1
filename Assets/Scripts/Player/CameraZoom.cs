using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    private PlayerControls playerControls;
    private CinemachineVirtualCamera camera;

    protected  void Awake()
    {
        playerControls = new PlayerControls();
        camera = GetComponent<CinemachineVirtualCamera>();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    private void Start()
    {
        playerControls.Mouse.Zoom.started += _ => Zoom();
    }

    private void Zoom()
    {
        float value = playerControls.Mouse.Zoom.ReadValue<float>();
        int direction = Math.Sign(value);
        camera.m_Lens.OrthographicSize += direction; 
    }
}
