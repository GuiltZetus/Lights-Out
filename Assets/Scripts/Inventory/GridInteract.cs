using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(ItemGrid))]
public class GridInteract : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    InventoryController inventoryController;
    ItemGrid itemGrid;

    private void Awake()
    {
        inventoryController = FindObjectOfType(typeof(InventoryController)) as InventoryController;
        itemGrid = GetComponent<ItemGrid>();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Mouse enter");
        inventoryController.SelectedItemGrid = itemGrid;

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Mouse exit");
        inventoryController.SelectedItemGrid = null;
    }
    public void OnDisable()
    {
        inventoryController.SelectedItemGrid = null;
    }
}
