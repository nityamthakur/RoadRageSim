using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public enum timeMode
    {
        beatTime,
        timeTrial
    }
    [Header("Time Mode")]
    public timeMode timeModeSetting = timeMode.beatTime;
    [Header("Game Objects")]
    public GameObject playerCar;
    public TextMeshProUGUI speedText;
    public TextMeshProUGUI healthText;
    public Canvas countDownCanvas;
    public Canvas TimerCanvas;
    public Canvas gameEndedCanvas;
    public static GameManager instance;

    [Header("Game Variables")]
    public float playerHealth = 100.0f;
    public int timeLimitMinutes = 10;
    private int countDownToStart = 3;
    private float timer = 0.0f;
    [NonSerialized] public bool gameStarted = false;
    private bool gameEnded = false;




    public void startGame()
    {
        gameStarted = true;
        switch (timeModeSetting)
        {
            case timeMode.beatTime:
                timer = timeLimitMinutes * 60.0f;
                break;
            case timeMode.timeTrial:
                timer = 0.0f;
                break;
        }
    }

    public void takeDamage(float damage)
    {
        playerHealth -= damage;
        if (playerHealth <= 0)
        {
            endGame();
        }
    }

    public void endGame()
    {
        gameStarted = false;
        gameEnded = true;
    }

    public void restartGame()
    {
        playerHealth = 100.0f;
        gameStarted = true;
    }

    void handleSpeedText()
    {
        if (speedText != null)
        {
            speedText.text = System.Math.Round(playerCar.GetComponent<PrometeoCarController>().carSpeed, 2).ToString() + "MPH";
        }
    }

    void handleHealthText()
    {
        if (healthText != null)
        {
            healthText.text = "Health: " + System.Math.Round(playerHealth, 2).ToString();
        }
    }

    void updateTimerText()
    {
        if (TimerCanvas != null)
        {
            int minutes = Mathf.FloorToInt(timer / 60F);
            int seconds = Mathf.FloorToInt(timer - minutes * 60);
            string niceTime;
            if (timeModeSetting == timeMode.beatTime) niceTime = "Time Left: ";
            else niceTime = "Time: ";
            niceTime += string.Format("{0:00}:{1:00}", minutes, seconds);
            TimerCanvas.GetComponentInChildren<TextMeshProUGUI>().text = niceTime;
        }
        if (countDownCanvas.enabled) countDownCanvas.enabled = false;
        if (!TimerCanvas.enabled) TimerCanvas.enabled = true;
    }

    void countDownText()
    {
        TextMeshProUGUI instructionsText = countDownCanvas.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI countDownText = countDownCanvas.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        if (instructionsText != null)
        {
            if (timeModeSetting == timeMode.beatTime) instructionsText.text = "Deliever the pizza in " + timeLimitMinutes + " minutes!";
            else instructionsText.text = "Get to the end as fast as you can!";
        }
        if (countDownText != null)
        {
            countDownText.text = countDownToStart.ToString() + "!";
        }
        if (!countDownCanvas.enabled) countDownCanvas.enabled = true;
        if (TimerCanvas.enabled) TimerCanvas.enabled = false;
    }

    void handleGameEnded()
    {
        if (gameEndedCanvas.enabled == false)
        {
            gameEndedCanvas.enabled = true;
            TimerCanvas.enabled = false;
            countDownCanvas.enabled = false;
        }
        if (gameEndedCanvas != null)
        {
            TextMeshProUGUI timeRemainingText = gameEndedCanvas.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI winOrLose = gameEndedCanvas.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
            switch (timeModeSetting)
            {
                case timeMode.beatTime:
                    timeRemainingText.text = "Time Remaining: " + System.Math.Round(timer, 2).ToString();
                    if (timer > 0.0f)
                    {
                        winOrLose.text = "Congratulations!";
                        winOrLose.color = Color.green;
                        timeRemainingText.color = Color.green;
                    }
                    else
                    {
                        winOrLose.text = "Failure!";
                        winOrLose.color = Color.red;
                        timeRemainingText.color = Color.red;
                    }
                    break;
                case timeMode.timeTrial:
                    timeRemainingText.text = "Time: " + System.Math.Round(timer, 2).ToString();
                    break;
            }
        }
    }

    void handleAllText()
    {
        handleSpeedText();
        handleHealthText();
        if (gameStarted)
        {
            updateTimerText();
        }
        else if (gameEnded == false)
        {
            countDownText();
        }
        else
        {
            handleGameEnded();
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        gameEndedCanvas.enabled = false;
        if (instance == null)
        {
            instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        handleAllText();
        if (gameStarted == false && countDownToStart > 0)
        {
            timer += Time.deltaTime;
            if (timer >= 1.0f)
            {
                timer = 0.0f;
                countDownToStart--;
                Debug.Log(countDownToStart);
                if (countDownToStart <= 0)
                {
                    startGame();
                }
            }
        }
        else if (gameEnded == false)
        {
            if (timeModeSetting == timeMode.beatTime)
            {
                timer -= Time.deltaTime;
                if (timer <= 0.0f)
                {
                    endGame();
                }
            }
            else if (timeModeSetting == timeMode.timeTrial)
            {
                timer += Time.deltaTime;
            }
        }



    }
}
