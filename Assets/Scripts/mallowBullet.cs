using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mallowBullet : MonoBehaviour
{
    [Range(1, 10)]
    [SerializeField] private float speed = 10f;

    [Range(1, 10)]
    [SerializeField] private float life = 3f; //despawn after 3f

    private Rigidbody2D bullet;

    private void Start()
    {
        bullet = GetComponent<Rigidbody2D>();
        //Destroy(gameObject, life);

       // gameObject.SetActive(false);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Bullet hit: " + collision.gameObject.name + ", Tag: " + collision.gameObject.tag);

        if (collision.gameObject.CompareTag("Enemy"))
        { 
            gameObject.SetActive(false);
            Debug.Log("set to false");
        }
        else if (collision.gameObject.CompareTag("Ceiling"))
        {
            gameObject.SetActive(false);
            Debug.Log("set to false");
        }
    }

    private void OnDisable()
    {
        Debug.Log("Bullet disabled");
    }

    private void FixedUpdate()
    {
        bullet.velocity = transform.up * speed;
    }

    //destroy onece hits enemy
    
}
