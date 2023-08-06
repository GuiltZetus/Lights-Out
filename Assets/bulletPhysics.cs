using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletPhysics : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
    [SerializeField] public float bulletSpeed;
    [SerializeField] public float fireRate;

    private float nextFireTime;

    private void Update()
    {
        // Check if the player can shoot
        if (Input.GetMouseButton(0) && Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + 1f / fireRate;

            Vector2 cursorPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = (cursorPosition - (Vector2)bulletSpawnPoint.position).normalized;

            GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);

            Rigidbody2D bulletRigidbody = bullet.GetComponent<Rigidbody2D>();
            bulletRigidbody.velocity = direction * bulletSpeed;


        }

    }

}
