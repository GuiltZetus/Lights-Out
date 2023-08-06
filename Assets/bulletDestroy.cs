using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletDestroy : MonoBehaviour
{

    [SerializeField] private int lifetime;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the bullet collides with any object
        // Perform any desired logic upon collision
        // For example, you can apply damage, play a particle effect, etc.

        // Destroy the bullet GameObject

        Destroy(gameObject);
    }
}
