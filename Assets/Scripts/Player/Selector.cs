using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selector : MonoBehaviour
{

    private Interactable interactableToSelect;
    private PlaceableZone placableToSelect;

    public Interactable InteractableToSelect { get => interactableToSelect; }
    public PlaceableZone PlaceableZoneToSelect { get => placableToSelect; }


    void Update()
    {
        Vector3 mousePosition = Input.mousePosition;

        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        mousePosition.z = 0f;

        transform.position = mousePosition;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Interactable interactable = collision.GetComponent<Interactable>();
        if (interactable)
        {
            interactableToSelect = interactable;
        }
        PlaceableZone placeableZone = collision.GetComponent<PlaceableZone>();
        if (placeableZone)
        {
            placableToSelect = placeableZone;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Interactable interactable = collision.GetComponent<Interactable>();
        if (interactable && interactable == interactableToSelect)
        {
            interactableToSelect = null;
        }
        PlaceableZone placeableZone = collision.GetComponent<PlaceableZone>();
        if (placeableZone && placeableZone == placableToSelect)
        {
            placableToSelect = null;
        }
    }
}
