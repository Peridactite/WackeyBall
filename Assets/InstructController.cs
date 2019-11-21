using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InstructController : MonoBehaviour
{
    [SerializeField] private Button backButton;
    [SerializeField] private GameObject titleCanvas, instructCanvas;

    private void Start()
    {
        backButton.onClick.AddListener(BackClicked);
    }

    void BackClicked()
    {
        Debug.Log("Back Clicked");
        titleCanvas.SetActive(true);
        instructCanvas.SetActive(false);
    }
}
