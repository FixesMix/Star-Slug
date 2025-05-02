using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemybullets : MonoBehaviour
{
    public GameObject player;
    private Rigidbody2D body;
    public float force;
    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");

        Vector3 direction = player.transform.position - transform.position;
        body.velocity = new Vector2(direction.x, direction.y).normalized * force;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > 10)
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other){
        if (other.gameObject.CompareTag("Player"))
        {
            //other.GameObject.GetComponent<playerHealth>().health -= 20;
            Destroy(gameObject);
        }
    }
}
