using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cainos.PixelArtTopDown_Basic
{
    public class TopDownCharacterController : MonoBehaviour
    {
        public float speed;
        public float maxHP = 100;
        public float currentHP;
        public HealthBar hpbar;

        public GameObject inventoryUI;
        private bool inventoryActive = false;

        private Animator animator;

        private void Start()
        {
            animator = GetComponent<Animator>();
            currentHP = maxHP;
            hpbar.setMaxHP(maxHP);
        }

        
        private void Update()
        {
            //player movement
            Vector2 dir = Vector2.zero;
            if (Input.GetKey(KeyCode.A))
            {
                dir.x = -1;
                animator.SetInteger("Direction", 3);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                dir.x = 1;
                animator.SetInteger("Direction", 2);
            }

            if (Input.GetKey(KeyCode.W))
            {
                dir.y = 1;
                animator.SetInteger("Direction", 1);
            }
            else if (Input.GetKey(KeyCode.S))
            {
                dir.y = -1;
                animator.SetInteger("Direction", 0);
            }
            dir.Normalize();
            animator.SetBool("IsMoving", dir.magnitude > 0);

            GetComponent<Rigidbody2D>().velocity = speed * dir;

            //Open Inventory
            if (Input.GetKeyDown(KeyCode.I))
            {
                ToggleInventory();
            }

            //player hp
            if (Input.GetKeyDown(KeyCode.Space) && currentHP > 0)
            {
                takeDamage(20);
            }
        }
        private void takeDamage(float damage)
        {
            currentHP -= damage;
            hpbar.setHP(currentHP);
        }
        public void ToggleInventory()
        {
            inventoryActive = !inventoryActive;
            inventoryUI.SetActive(inventoryActive);
        }
    }
}
