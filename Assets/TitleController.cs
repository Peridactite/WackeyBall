using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleController : MonoBehaviour
{
    [SerializeField] private Button startButton;
    [SerializeField] private Button instructButton;
    [SerializeField] private Button quitButton;

    [SerializeField] private GameObject instructCanvas;
    [SerializeField] private GameObject titleCanvas;
    // Start is called before the first frame update
    void Start()
    {
        startButton.onClick.AddListener(startClicked);
        instructButton.onClick.AddListener(instructClicked);
        quitButton.onClick.AddListener(quitClicked);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void startClicked()
    {
        Debug.Log("Start clicked");
        SceneManager.LoadSceneAsync(1, LoadSceneMode.Single);
        SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(1));
    }

    void instructClicked()
    {
        Debug.Log("Instruct Clicked");
        instructCanvas.SetActive(true);
        titleCanvas.SetActive(false);
    }

    void quitClicked()
    {
        Application.Quit();
    }
}
