using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] Text ScoreText;
    [SerializeField] Text announcerText;
    [SerializeField] Text timerText;

    [SerializeField] int playerScore = 0;
    [SerializeField] int enemyScore = 0;
    [SerializeField] float timer = 180;

    private bool canCount = true;
    private bool doOnce = false;

    enum announcements
    {
        defaultMessage,
        playerLoss,
        playerWon
    }

    [SerializeField] announcements currentAnnouncment;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ScoreText.text = "Player: " + playerScore + " Enemy: " + enemyScore;

        switch(currentAnnouncment)
        {
            case announcements.defaultMessage:
                announcerText.text = "";
                break;
            case announcements.playerLoss:
                announcerText.text = "You lose";
                break;
            case announcements.playerWon:
                announcerText.text = "You won!!";
                break;
        }

        if (timer >= 0.0f && canCount)
        {
            timer -= Time.deltaTime;
            timerText.text = ((int)timer/60).ToString() + ":" + (timer%60).ToString();
        }

        else if(timer <= 0.0f && !doOnce)
        {
            canCount = false;
            doOnce = true;
            timerText.text = "0:00";
            currentAnnouncment = announcements.playerWon;
        }
    }
}
