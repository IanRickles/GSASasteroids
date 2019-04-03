using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidFun : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector2(Random.Range(-8, 8), Random.Range(-5, 5));
        transform.Rotate(0, 0, Random.value * 360);
        GetComponent<Rigidbody2D>().AddForce(transform.up * (Random.value*50f));
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = myMath.Wrap(transform.position);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "PlayerBullet")
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);

            RocketPhysics.numberOfAsteroids--;
            RocketPhysics.score += 5;
            
        }
    }
}
