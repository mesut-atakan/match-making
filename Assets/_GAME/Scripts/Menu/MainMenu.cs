using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button btnPlay;

    private void OnEnable()
    {
        btnPlay.onClick.AddListener(OnPlay);
    }

    private void OnDisable()
    {
        btnPlay.onClick.RemoveListener(OnPlay);
    }

    private void OnPlay()
    {
        SceneManager.LoadScene(1);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
