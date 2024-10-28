using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject playerCar;


    private bool gameStarted = false;
    public GameObject pedestrianCarPrefab;
    public float playerHealth = 100.0f;
    public GameObject endPoint;


    //To Do: Add parameters to the function to spawn the pedestrian car at a specific location
    public void spawnPedestrianCar()
    {
        GameObject tempCar = Instantiate(pedestrianCarPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        //tempCar.GetComponent<PedestrianCar>().
    }

    public void startGame()
    {
        gameStarted = true;
    }

    public void takeDamage(float damage)
    {
        playerHealth -= damage;
        if(playerHealth <= 0)
        {
            endGame();
        }
    }

    public void endGame()
    {
        gameStarted = false;
        Debug.Log("Game Over");
    }

    public void restartGame()
    {
        playerHealth = 100.0f;
        gameStarted = true;
    }




    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gameStarted)
        {
            //To Do: Implement the game logic here
        }
    }
}
