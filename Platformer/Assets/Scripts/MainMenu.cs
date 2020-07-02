using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button startBtn;
    [SerializeField] private Button settingsBtn;
    [SerializeField] private Button exitBtn;

    private void Awake() {
        startBtn.onClick.AddListener(StartClick);
        settingsBtn.onClick.AddListener(SettingsClick);
        exitBtn.onClick.AddListener(ExitClick);
    }

    private void StartClick() {
        SceneManager.LoadScene(1);
    }

    private void SettingsClick() {
        
    }

    private void ExitClick() {
        Application.Quit();
    }

}
