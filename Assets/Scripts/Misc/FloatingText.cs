using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingText : MonoBehaviour
{
    private MeshRenderer meshRendererText;

    private void Start()
    {
        meshRendererText = GetComponent<MeshRenderer>();
    }
    public void Show()
    {
        meshRendererText.enabled = true;
    }

    public void Hide()
    {
        meshRendererText.enabled = false;
    }
}
