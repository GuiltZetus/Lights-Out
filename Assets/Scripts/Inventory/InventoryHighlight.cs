using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryHighlight : MonoBehaviour
{
    [SerializeField] RectTransform highlighter;
    public void Show(bool b)
    {
        highlighter.gameObject.SetActive(b);
    }
    public void SetSize(InventoryItem targetItem)
    {
        Vector2 size = new Vector2();
        size.x = targetItem.WIDTH * ItemGrid.tileSizeWidth;
        size.y = targetItem.HEIGHT * ItemGrid.tileSizeHeight;
        highlighter.sizeDelta = size;
    }

    public void SetPositionItem(ItemGrid targetGrid, InventoryItem targetItem)//highlight the item
    {
        SetParent(targetGrid);

        Vector2 pos = targetGrid.CalculatedPositionOnGrid(targetItem, targetItem.onGridPositionX, targetItem.onGridPositionY);

        highlighter.localPosition = pos;

    }

    public void SetParent(ItemGrid targetGrid)
    {
        if(targetGrid == null) { return; }
        highlighter.SetParent(targetGrid.GetComponent<RectTransform>());
    }

    public void SetPositionGrid(ItemGrid targetGrid, InventoryItem targetItem, int posX, int posY)//highlight the position that the item will be placed
    {
        Vector2 pos = targetGrid.CalculatedPositionOnGrid(targetItem, posX, posY);
        highlighter.localPosition = pos;
    }    
}
