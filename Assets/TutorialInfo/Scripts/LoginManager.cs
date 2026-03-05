using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoginManager : MonoBehaviour
{
    public static LoginManager Instance;

    public GameObject loginPanel;

    public GameObject registrationPanel;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    public void OpenLoginPanel()
    {
        loginPanel.SetActive(true);
        registrationPanel.SetActive(false);
    }

    public void OpenRegistrationPanel()
    {
        registrationPanel.SetActive(true);
        loginPanel.SetActive(false);
    }
    public void Back()
    {
        SceneManager.UnloadSceneAsync(3);
    }
}
