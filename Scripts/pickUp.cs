using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickUp : MonoBehaviour
{
 
    public int scoreValue;
    private GameController gameController;

    void Start()
    {
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
        }
        if (gameController == null)
        {
            Debug.Log("Cannot find 'GameController' script");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Boundary") || other.CompareTag("Enemy"))
        {
            return;
        }
       
        if (other.tag == "Player")
        {
            gameController.AddScore(scoreValue);

            Destroy(gameObject);

        }
        
    }
}
