using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBlink : MonoBehaviour
{
    [SerializeField] private Material whiteFlashMat;
    [SerializeField] private float restoreDefualtMatTime = .3f;

    private Material defaultMat;
    private SpriteRenderer spriteRenderer;

    public float RestoreDefualtMatTime { get => restoreDefualtMatTime; }

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        defaultMat = spriteRenderer.material;
    }

    public IEnumerator FlashRoutine()
    {
        spriteRenderer.material = whiteFlashMat;
        yield return new WaitForSeconds(RestoreDefualtMatTime);
        spriteRenderer.material = defaultMat;
    }

}
