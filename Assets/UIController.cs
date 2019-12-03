using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    [SerializeField] Text ScoreText;
    [SerializeField] Text announcerText;
    [SerializeField] Text timerText;
    [SerializeField] AudioSource audioSrc;
    [SerializeField] AudioClip victory, defeat, draw;

    [SerializeField] int playerScore = 0;
    [SerializeField] int enemyScore = 0;
    [SerializeField] float timer = 180;
    [SerializeField] float waitTime = 20.0f;

    private bool canCount = true;
    private bool doOnce = false;

    enum announcements
    {
        defaultMessage,
        playerLoss,
        playerWon,
        draw
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
            case announcements.draw:
                announcerText.text = "A Draw";
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
            StartCoroutine(Victory());
        }
    }

    public void AddPlayer()
    {
        playerScore++;
    }

    public void AddEnemy()
    {
        enemyScore++;
    }

    private IEnumerator Victory()
    {
        if (playerScore > enemyScore)
        {
            audioSrc.loop = false;
            audioSrc.clip = victory;
            audioSrc.Play();
            currentAnnouncment = announcements.playerWon;
        }
        else if (enemyScore > playerScore)
        {
            audioSrc.loop = false;
            audioSrc.clip = defeat;
            audioSrc.Play();
            currentAnnouncment = announcements.playerLoss;
        }
        else
        {
            audioSrc.loop = false;
            audioSrc.clip = draw;
            audioSrc.Play();
            currentAnnouncment = announcements.draw;
        }
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
