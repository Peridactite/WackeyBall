using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] Text ScoreText;
    [SerializeField] int playerScore = 0;
    [SerializeField] int enemyScore = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ScoreText.text = "Player: " + playerScore + " Enemy: " + enemyScore;
    }
}
