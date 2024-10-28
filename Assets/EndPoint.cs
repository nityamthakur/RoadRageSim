using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPoint : MonoBehaviour
{
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
     gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }


    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {  
            //Temporarily take damage for testing purposes
            gameManager.takeDamage(0.1f);
            gameManager.endGame();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
