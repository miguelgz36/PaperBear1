using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    [SerializeField] private float maxZoomIn = 10f;
    [SerializeField] private float maxZoomOut = 20f;

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
        float currentSize = camera.m_Lens.OrthographicSize;
        if (direction == 1 && currentSize >= maxZoomOut) return;
        if (direction == -1 && currentSize <= maxZoomIn) return;

        camera.m_Lens.OrthographicSize = currentSize + direction; 
    }
}
