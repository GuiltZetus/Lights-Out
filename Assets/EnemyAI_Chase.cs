using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI_Chase : MonoBehaviour
{

    public GameObject player;
    public float speed;
    public float detectionRange;

    private float distance;
    private Rigidbody2D rb;
    private bool isColliding = false;
    private bool isChasing = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = player.transform.position - transform.position;

        
        while (distance < detectionRange && distance !< 5)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
            isChasing = true;
            
        }
        
        

    }

    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == player)
        {
            isColliding = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject == player)
        {
            isColliding = false;
        }
    }
}
