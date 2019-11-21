﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CreditController : MonoBehaviour
{
    [SerializeField] private Button restartButton, exitButton;

    // Start is called before the first frame update
    void Start()
    {
        restartButton.onClick.AddListener(restartClicked);
        exitButton.onClick.AddListener(exitClicked);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void restartClicked()
    {
        SceneManager.LoadSceneAsync(1);
    }

    void exitClicked()
    {
        Application.Quit();
    }
}
