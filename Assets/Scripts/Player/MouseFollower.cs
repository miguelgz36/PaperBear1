using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//TODO: REFACTOR TO OnMouseEnter and OnMouseExit
public class MouseFollower : Singleton<MouseFollower>
{

    private Interactable interactableToSelect;
    private PlaceableZone placableToSelect;
    private Cell mouseInCell;

    public Interactable InteractableToSelect { get => interactableToSelect; }
    public PlaceableZone PlaceableZoneToSelect { get => placableToSelect; }
    public Cell MouseInCell { get => mouseInCell; }

    void Update()
    {
        Vector3 mousePosition = Input.mousePosition;

        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        mousePosition.z = 0f;

        transform.position = mousePosition;
    }

    private void OnTriggerStay2D(Collider2D collision)
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
        Cell cell = collision.GetComponent<Cell>();
        if (cell)
        {
            mouseInCell = cell;
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
        Cell cell = collision.GetComponent<Cell>();
        if (cell)
        {
            mouseInCell = null;
        }
    }
}
