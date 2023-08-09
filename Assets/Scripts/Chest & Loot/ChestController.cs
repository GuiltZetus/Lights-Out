using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestController : MonoBehaviour
{
    public bool isOpen = false; // Flag to track if the chest is currently open
    public bool Interactable;
    public GameObject lootPrefab;
    public List<Loot> lootList = new List<Loot>();



    void Update()
    {
        if (Interactable == true && Input.GetKeyDown(KeyCode.F) && isOpen == false)
        {
            OpenChest();
            SpawnLoot(transform.position);
        }
    }

    

    // Function to open the chest
    private void OpenChest()
    {
        isOpen = true;
        
        //PlayerInventory.SetActive(!PlayerInventory.activeSelf);
        Debug.Log("Opening chest");
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            SetInteractable(true);
            Debug.Log("Player enter the trigger");

        }
        else { SetInteractable(false); }

    }
    

    private void OnTriggerExit2D(Collider2D other)
    {
        SetInteractable(false);
        //PlayerInventory.SetActive(false);
        Debug.Log("Player too far, closing chest");
    }

    private void SetInteractable(bool value)
    {
        Interactable = value;
    }


    public List<Loot> GetDroppedItems()
    {
        int randomNumber;
        List<Loot> possibleItem = new List<Loot>();
        foreach (Loot item in lootList)
        {
            randomNumber = UnityEngine.Random.Range(1, 101); //1-100
            if (randomNumber <= item.dropChance)
            {
                possibleItem.Add(item);
                Debug.Log("Some item dropped");
            }
        }
        if (possibleItem.Count > 0)
        {
            Debug.Log("Number of possible items: " + possibleItem.Count);
            return possibleItem;// return the list of dropped items
        }
        Debug.Log("No item dropped ");
        return null;// return null if no item dropped
    }
    private void SpawnLoot(Vector3 spawnPoint)
    {
        List<Loot> droppedItems = GetDroppedItems();
        float dropForce = 100f;
        if (droppedItems != null)
        {
            foreach(Loot item in droppedItems)
            {
                GameObject lootGameObject = Instantiate(lootPrefab, spawnPoint, Quaternion.identity);
                lootGameObject.GetComponent<SpriteRenderer>().sprite = item.lootSprite;
                lootGameObject.transform.localScale = new Vector3(0.5f / item.lootSprite.bounds.size.x, 0.6f / item.lootSprite.bounds.size.y, 1f);
                SpriteRenderer itemRenderer = lootGameObject.GetComponent<SpriteRenderer>();
                itemRenderer.sortingOrder = 2;
                Vector2 dropDirection = new Vector2(UnityEngine.Random.Range(-1f, 1f), 
                    UnityEngine.Random.Range(-1f, 1f)); // launch dropped item at a random direction when open chest

                lootGameObject.GetComponent<Rigidbody2D>().AddForce(dropDirection * dropForce, ForceMode2D.Impulse);
            }
            
        }
        
    }
}
