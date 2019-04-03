using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject player;
    public GameObject bulletPrefab;
    public AudioSource explosion;
    public AudioSource laser;
    public GameObject powerPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
        player = RocketPhysics.s.gameObject;

        Invoke("Shoot", 3f);
        Invoke("SpawnPowerup", 5f + Random.value);

    }
    void Shoot()
    {
        if (!RocketPhysics.respawning)
        {
            
            GameObject bullet;
            bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
            bullet.GetComponent<Rigidbody2D>().AddForce(bullet.transform.up * 50f);
            Destroy(bullet, 1.5f);

            Invoke("Shoot", 1.5f + Random.value);
            
        }
    }
    void SpawnPowerup()
    {
        GameObject PU;
        PU = Instantiate(powerPrefab, transform.position, Quaternion.identity);
    }
    // Update is called once per frame
    void Update()
    {
        if (!RocketPhysics.respawning)
        {
            

            Vector2 newVector;

            newVector = player.transform.position - transform.position;

            transform.up = newVector.normalized;
        }
        //GetComponent<Rigidbody2D>().AddForce(transform.up * 2f);

        transform.position = myMath.Wrap(transform.position);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "PlayerBullet")
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
            
            RocketPhysics.score += 10f;
        }
    }
}
