using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public static MainMenu instance;
    public int scene;
    public GameObject mainMenu;
    public GameObject MapMenu;
    private GameObject currentMenu;
    public int carIndex;
    public int mapIndex;
    private string menu;
    public TextMeshProUGUI buttonText;
    public TextMeshProUGUI moneyText;
    public Button start;
    public Button mapstart;
    public GameObject buy;
    public TextMeshProUGUI connect;
    public TextMeshProUGUI connectbutton;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        if (GameManager.instance == null)
        {
            SceneManager.LoadSceneAsync(2, LoadSceneMode.Additive);
        }
        if (AuthManager.instance == null)
            SceneManager.LoadSceneAsync(3, LoadSceneMode.Additive);
    }
    private void Start()
    {
        AudioManager.Instance.PlayAudio(0);
        ScoreManager.instance.UpdateMenuScore();
        SceneManager.UnloadSceneAsync(2);
        if (SceneManager.GetSceneByBuildIndex(3).isLoaded)
            SceneManager.UnloadSceneAsync(3);
        StartCoroutine(InitializeMainMenu());
    }
    private IEnumerator InitializeMainMenu()
    {
        // Wait until Firebase is initialized
        yield return new WaitUntil(() => AuthManager.instance != null && AuthManager.instance.firebaseInitialized);

        // Update the menu score


        // Check if the user is logged in
        if (AuthManager.instance.IsUserLoggedIn())
        {
            connect.text = "Connected as " + PlayerPrefs.GetString("User");
            SetLogout();
        }
        else
        {
            connect.text = "Not connected";
            SetLogin();
        }
    }
    private void FixedUpdate()
    {
        if (!CarManager.instance.cars[carIndex].isUnlocked)
            SetBuy();
        else
            SetSelect();
        if (!MapManager.instance.map[mapIndex].isUnlocked)
        {
            mapstart.interactable = false;
            buy.SetActive(true);
        }
        else
        {
            mapstart.interactable = true;
            buy.SetActive(false);
        }
        ScoreManager.instance.UpdateMenuScore();
    }
    public void SelectMap()
    {
        if (CarManager.instance.cars[carIndex].isUnlocked)
        {
            MapMenu.SetActive(true);
        }
    }
    public void PlayGame()
    {
        mainMenu.SetActive(false);
        MapMenu.SetActive(false);
        AudioManager.Instance.StopAudio(0);
        SceneManager.LoadSceneAsync(2);
    }
    public void Connect()
    {
        if (connectbutton.text == "Login")
            SceneManager.LoadSceneAsync(3, LoadSceneMode.Additive);
        else if (connectbutton.text == "Logout")
            Logout();
    }

    public void MapBack()
    {
        MapMenu.SetActive(false);
    }
    public void Options()
    {
        scene = SceneManager.GetActiveScene().buildIndex;
        mainMenu.SetActive(false);
        SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);

    }
    public void ConfirmCarSelection()
    {
        if (buttonText.text == "Select")
        {
            PlayerPrefs.SetInt("SelectedCarIndex", carIndex);
            PlayerPrefs.Save();
        }
        else if (buttonText.text == "Buy")
        {
            if (CarManager.instance.UnlockCar(carIndex))
            {
                AudioManager.Instance.PlayAudio(4);
                PlayerPrefs.SetInt("SelectedCarIndex", carIndex);
                PlayerPrefs.Save();
            }
        }
    }
    public void ConfirmMapSelection()
    {
        PlayerPrefs.SetInt("MapIndex", mapIndex);
        PlayerPrefs.Save();
    }
    public void BuyMap()
    {
        if (MapManager.instance.UnlockMap(mapIndex))
        {
            PlayerPrefs.SetInt("MapIndex", mapIndex);
            PlayerPrefs.Save();
            AudioManager.Instance.PlayAudio(4);
        }
    }
    public void Back()
    {
        mainMenu.SetActive(true);
        SceneManager.UnloadSceneAsync(1);
    }
    public void Exit()
    {
        Application.Quit();
    }
    public string GetMenu()
    {
        return menu;
    }
    public void SetBuy()
    {
        buttonText.text = "Buy";
        start.interactable = false;

    }
    public void SetSelect()
    {
        buttonText.text = "Select";
        start.interactable = true;
    }
    public void DisplayUser()
    {
        connect.text = "Conectat ca " + PlayerPrefs.GetString("User");
        SetLogout();
    }
    public void SetLogin()
    {
        connectbutton.text = "Login";
    }
    public void SetLogout()
    {
        connectbutton.text = "Logout";
    }
    public void Logout()
    {
        SetLogin();
        AuthManager.instance.Logout();
        PlayerPrefs.SetString("User", null);
        connect.text = "Conecteaza-te";
    }
}
