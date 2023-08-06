using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestController : MonoBehaviour
{
    public bool isOpen = false; // Flag to track if the chest is currently open
    public float interactionDistance = 2f;
    public bool Interactable;
    public GameObject ChestInventory;
    public GameObject PlayerInventory;
    void Update()
    {
        if (Interactable == true && Input.GetKeyDown(KeyCode.E))
        {
            ToggleChest();
        }
    }

    // Function to open the chest
    private void ToggleChest()
    {
        isOpen = isOpen ? false : true;
        ChestInventory.SetActive(!ChestInventory.activeSelf);
        PlayerInventory.SetActive(!PlayerInventory.activeSelf);
        Debug.Log("Toggling chest");
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
        isOpen = false;
        SetInteractable(false);
        ChestInventory.SetActive(false);
        PlayerInventory.SetActive(false);
        Debug.Log("Player too far, closing chest");
    }

    private void SetInteractable(bool value)
    {
        Interactable = value;
    }




    /*private bool IsPlayerNear()
    {
        // Get the player's position from the player GameObject directly
        GameObject player = GameObject.Find("PF Player"); // Replace "Player" with the name of your player GameObject
        if (player != null)
        {
            // Calculate the distance between the player and the chest
            float distance = Vector3.Distance(transform.position, player.transform.position);

            // Return true if the player is within the interaction distance
            return distance <= interactionDistance;
        }

        return false;
    }*/
}
