using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CreditController : MonoBehaviour
{
    [SerializeField] private Button restartButton, exitButton, titleButton;

    // Start is called before the first frame update
    void Start()
    {
        restartButton.onClick.AddListener(restartClicked);
        exitButton.onClick.AddListener(exitClicked);
        titleButton.onClick.AddListener(titleClicked);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void restartClicked()
    {
        SceneManager.LoadSceneAsync(1);
    }

    void titleClicked()
    {
        SceneManager.LoadSceneAsync(0);
    }

    void exitClicked()
    {
        Application.Quit();
    }
}
