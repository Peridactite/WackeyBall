using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] Text ScoreText;
    [SerializeField] Text announcerText;
    [SerializeField] int playerScore = 0;
    [SerializeField] int enemyScore = 0;

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
    }
}
