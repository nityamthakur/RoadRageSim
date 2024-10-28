using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject playerCar;


    public TextMeshProUGUI speedText;
    public TextMeshProUGUI healthText;
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

    void handleSpeedText(){
        if (speedText != null) {
            speedText.text = System.Math.Round(playerCar.GetComponent<PrometeoCarController>().carSpeed,2).ToString() + "MPH";
        }
    }

    void handleHealthText(){
        if (healthText != null) {
            healthText.text = "Health: " + System.Math.Round(playerHealth,2).ToString();
        }
    }

    void handleAllText(){
        handleSpeedText();
        handleHealthText();
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        handleAllText();
        if (gameStarted)
        {
            //To Do: Implement the game logic here
        }
    }
}
