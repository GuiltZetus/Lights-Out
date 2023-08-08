using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGrid : MonoBehaviour
{
    public const float tileSizeWidth = 120;
    public const float tileSizeHeight = 120;
    InventoryItem[,] inventoryItemSlot;
    InventoryItem selectedItem;
    InventoryController inventoryController;
    RectTransform rectTransform;

    [SerializeField] Transform canvasTransform;
    public GameObject lootItemPrefab;
    public List<ItemData> lootList = new List<ItemData>(); 


    [SerializeField] int gridWidth = 9; 
    [SerializeField] int gridHeight = 6;
    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        Init(gridWidth, gridHeight);
    }
    public InventoryItem PickUpItem(int x, int y)
    {
        InventoryItem toReturn = inventoryItemSlot[x, y];

        if (toReturn == null) { return null; }

        CleanGridReference(toReturn);
        return toReturn;
    }

    private void CleanGridReference(InventoryItem item)
    {
        for (int ix = 0; ix < item.WIDTH; ix++)
        {
            for (int iy = 0; iy < item.HEIGHT; iy++)
            {
                inventoryItemSlot[item.onGridPositionX + ix, item.onGridPositionY + iy] = null;
            }
        }
    }

    private void Init(int width, int height)
    {
        inventoryItemSlot = new InventoryItem[width, height];
        Vector2 size = new Vector2(width * tileSizeWidth, height * tileSizeHeight);
        rectTransform.sizeDelta = size;
    } 

    internal InventoryItem GetItem(int x, int y)
    {
        return inventoryItemSlot[x, y];
    }

    List<ItemData> GetDroppedItems()
    {
        int randomNumber; 
        List<ItemData> possibleItem = new List<ItemData>();
        foreach(ItemData item in lootList)
        {
            randomNumber = UnityEngine.Random.Range(1, 101); //1-101
            if (randomNumber <= item.dropChance)
            {
                possibleItem.Add(item);
                Debug.Log("Some item dropped");                              
            }
        }
        return possibleItem;// will return an empty List if no item is dropped
    }

    public void CreateLoot()
    {
        List<ItemData> droppedItems = GetDroppedItems();
        if (droppedItems.Count > 0)
        {
            foreach (ItemData droppedItemData in droppedItems)
            {
                CreateItem(droppedItemData);
                InventoryItem itemToInsert = selectedItem;
                selectedItem = null;
                inventoryController.InsertItem(itemToInsert);
            }
        }
    }

    private void CreateItem(ItemData droppedItemData)
    {
        InventoryItem inventoryItem = Instantiate(lootItemPrefab).GetComponent<InventoryItem>();
        selectedItem = inventoryItem;
        rectTransform = inventoryItem.GetComponent<RectTransform>();
        rectTransform.SetParent(canvasTransform);

        inventoryItem.Set(droppedItemData);
    }

    Vector2 positionOnTheGrid = new Vector2();
    Vector2Int tileGridPosition = new Vector2Int();
    public Vector2Int GetTileGridPosition(Vector2 mousePosition)
    {
        positionOnTheGrid.x = mousePosition.x - rectTransform.position.x;
        positionOnTheGrid.y = rectTransform.position.y - mousePosition.y;

        tileGridPosition.x = (int)(positionOnTheGrid.x / tileSizeWidth);
        tileGridPosition.y = (int)(positionOnTheGrid.y / tileSizeHeight);

        return tileGridPosition;
    }

    public Vector2Int? FindSpaceForObject(InventoryItem itemToInsert)// ? is for nullable int
    {
        int height = gridHeight - itemToInsert.HEIGHT + 1;
        int width = gridWidth - itemToInsert.WIDTH + 1;

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                if (CheckAvailableSpace(x, y, itemToInsert.WIDTH, itemToInsert.HEIGHT) == true)
                {
                    return new Vector2Int(x, y); 
                }
            }
        }
        return null;
    }

    /*
Basically how this work is it find position of the mouse on the grid and then take it X and Y and divide that shit with the size of a single inventory slot
and then round it up with Int so you get the position of the slot (i don't have to explain shit go figure out how the math work yourself i graduated from highschool
2 years ago and im not going back
*/

    public bool PlaceItem(InventoryItem inventoryItem, int posX, int posY, ref InventoryItem overlapItem)
    {
        if (BoundaryCheck(posX, posY, inventoryItem.WIDTH, inventoryItem.HEIGHT) == false)
        {
            //check if the object is outside the inventory boundary or not
            return false;
        }

        if (OverlapCheck(posX, posY, inventoryItem.WIDTH, inventoryItem.HEIGHT, ref overlapItem) == false)
        {
            //check if there're overlaping item when placing
            overlapItem = null;
            return false;
        }

        if (overlapItem != null)
        {
            //imma keep it real witchu chief. I do not know what this does
            CleanGridReference(overlapItem);
        }
        PlaceItem(inventoryItem, posX, posY);
        return true;
    }

    public void PlaceItem(InventoryItem inventoryItem, int posX, int posY)
    {
        RectTransform rectTransform = inventoryItem.GetComponent<RectTransform>();
        rectTransform.SetParent(this.rectTransform);

        for (int x = 0; x < inventoryItem.WIDTH; x++)
        {
            for (int y = 0; y < inventoryItem.HEIGHT; y++)
            {
                inventoryItemSlot[posX + x, posY + y] = inventoryItem;
            }
        }

        inventoryItem.onGridPositionX = posX;
        inventoryItem.onGridPositionY = posY;
        Vector2 position = CalculatedPositionOnGrid(inventoryItem, posX, posY);

        rectTransform.localPosition = position;
        Debug.Log("item placed");
    }

    public Vector2 CalculatedPositionOnGrid(InventoryItem inventoryItem, int posX, int posY)
    {
        Vector2 position = new Vector2();
        position.x = (posX * tileSizeWidth) + (tileSizeWidth * inventoryItem.WIDTH / 2);
        position.y = (-posY * tileSizeHeight) - (tileSizeHeight * inventoryItem.HEIGHT / 2);
        return position;
    }

    private bool OverlapCheck(int posX, int posY, int width, int height, ref InventoryItem overlapItem)
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if(inventoryItemSlot[posX+x, posY+y] != null)
                {
                    if (overlapItem == null)
                    {
                        overlapItem = inventoryItemSlot[posX + x, posY + y];
                    }
                    else
                    {
                        if (overlapItem != inventoryItemSlot[posX + x, posY + y])
                        {
                            return false;
                        }
                    }
                }
            }
        }

        return true;
    }

    private bool CheckAvailableSpace(int posX, int posY, int width, int height)
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (inventoryItemSlot[posX + x, posY + y] != null)
                {                   
                    return false;
                }
            }
        }
        Debug.Log("there is available space");
        return true;
    }

    bool PositionCheck (int posX, int posY)
    {
        if (posX<0 || posY < 0)
        {
            return false;
        }
        if (posX >= gridWidth || posY >= gridHeight)
        {
            return false;
        }
        return true;
    }

    public bool BoundaryCheck(int posX, int posY, int width, int height)
    {
        if(PositionCheck(posX,posY) == false)
        {
            return false;
        }
        posX += width - 1;
        posY += height - 1;

        if(PositionCheck(posX, posY) == false)
        {
            return false;
        }

        return true;
    }
    public void checking()
    {
        Debug.Log("yeah the script is active");
    }
}
 