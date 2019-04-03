using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class extraLifeTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D colli)
    {
        if (colli.gameObject.CompareTag("player"))
        {
            RocketPhysics.lives++;
            Destroy(gameObject);
        }
    }
}
