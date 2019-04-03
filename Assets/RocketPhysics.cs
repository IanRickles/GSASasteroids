using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;

public class RocketPhysics : MonoBehaviour
{
    GameObject enemy;
    public GameObject enemyPrefab;
    public GameObject asteroidPrefab;
    public GameObject bulletPrefab;
    Vector2 bounds = new Vector2(9f, 5f);
    public static GameObject s;
    public static bool respawning = false;
    public static int level = 1;
    public static int numberOfAsteroids = 0;
    public Text text;
    public static float score;
    public float highscore;
    public Text endscreen;
    public bool gameover;
    public AudioSource explosion;
    public AudioSource laser;

    public static int lives = 3;
    // Start is called before the first frame update
    void Start()
    {
        
        highscore = PlayerPrefs.GetFloat("hs", 0);
        endscreen.gameObject.SetActive(false);
        gameover = false;
        if (s == null) s = gameObject;
        else
        {
            Debug.LogError("Error: Already instantiated player");
            return;
        }
        transform.position = myMath.Wrap(transform.position, bounds);

        SpawnAsteroidFieldIfNecessary();
        Invoke("SpawnEnemy",3f);
        respawning = false;
    }

   

    void SpawnAsteroidFieldIfNecessary()
    {
       
        if (numberOfAsteroids <= 0)
        {
            level++;
            numberOfAsteroids = (int)(Random.Range(2f,2f * level));
            numberOfAsteroids = (int)Mathf.Min(16f, (float)numberOfAsteroids);
            for (int i = 0; i < numberOfAsteroids; i++)
            {
                Instantiate(asteroidPrefab);
            }
            
        }
        
       
    }

    

    void SpawnEnemy()
    {
        enemy = Instantiate(enemyPrefab, new Vector2(Random.Range(-8,8),Random.Range(-5,5)),Quaternion.identity);
        Invoke("SpawnEnemy", 5f +Random.value);
    }

    void RespawnPlayer()
    {
        
        GetComponent<Rigidbody2D>().AddForce(transform.up * 10f);
        if (lives-- > 0)
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
            respawning = false;
        }
        else
        {
            Debug.Log("Out of Lives");
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (lives == 0)
        {
            endscreen.gameObject.SetActive(true);
            PlayerPrefs.SetFloat("hs", highscore);
            respawning = true;
            gameover = true;
            Time.timeScale = 0;
            return;
        }
        if (score > highscore) highscore = score;
        text.text = System.String.Format("Lives: {0}        Level: {1}        Score: {2}        High Score: {3}", lives, level - 1, score, highscore);
        endscreen.text = System.String.Format("You Lose!    Score: {0}", score);
        transform.position = myMath.Wrap(transform.position);
        SpawnAsteroidFieldIfNecessary();
        
        if (Input.GetKey(KeyCode.W))
        {
            GetComponent<Rigidbody2D>().AddForce(transform.up*10f);
        }
        if (Input.GetKey(KeyCode.A))
        {
            GetComponent<Rigidbody2D>().AddTorque(5);
        }
        if (Input.GetKey(KeyCode.D))
        {
            GetComponent<Rigidbody2D>().AddTorque(-5);
        }
        if (respawning) return;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            laser.Play();
            GameObject bullet;
            bullet = Instantiate(bulletPrefab,transform.position,transform.rotation);
            bullet.GetComponent<Rigidbody2D>().AddForce(bullet.transform.up * 150f);
            Destroy(bullet, .75f);
        }
    }

    
        

    private void OnCollisionEnter2D(Collision2D collision)
    {
        string objectTag = collision.gameObject.tag;
            if (respawning == true) return;

        if (objectTag == "Asteroid")
        {
            SpawnAsteroidFieldIfNecessary();
        }

        if (objectTag != "PlayerBullet")
        {
                Destroy(collision.gameObject);
                gameObject.GetComponent<SpriteRenderer>().enabled = false;
                respawning = true;
                Invoke("RespawnPlayer", 2f);
        }
        if (objectTag == "Asteroid")
        {
            numberOfAsteroids--;
        }
        explosion.Play();
    }
    
}
