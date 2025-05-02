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
        if (collision.gameObject.CompareTag("Enemy"))
        { //pooling is working if object set to player. double check collision is set correctly for otehr gameobjects. compare ebverything to player
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
