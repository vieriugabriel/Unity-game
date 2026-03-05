using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private GameObject _gameOver;

    [SerializeField] private GameObject pause;
    [SerializeField] private Button brake;
    [SerializeField] private GameObject left;
    [SerializeField] private GameObject right;
    [SerializeField] private GameObject []backgroud;
    [SerializeField] private GameObject finish;
    public bool isForwardPressed = false;
    public bool isBackPressed = false;
    public bool isLeftPressed = false;
    public bool isRightPressed = false;

    private void Start()
    {
        string controlMethod = PlayerPrefs.GetString("ControlMethod", "Button");
        SetControlMethod(controlMethod);
        SelectMapBackgroud(MainMenu.instance.mapIndex);
    }

    public void SetControlMethod(string controlMethod)
    {
        if (controlMethod == "Tilt")
        {
            Tilt();
        }
        else if (controlMethod == "Button")
        {
            Button();
        }
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;   
        }
 
        Time.timeScale = 1.0f;
    }
    public void GameOver()
    {
        AudioManager.Instance.PlayAudio(11);
        _gameOver.SetActive(true);
        FuelController.instance.lowFuel.SetActive(false);
        Time.timeScale = 0f;
    }
    public void Finish()
    {
        finish.SetActive(true);
        FuelController.instance.lowFuel.SetActive(false);
        Time.timeScale = 0f;
    }
    public void ResetGame()
    {
        SceneManager.LoadSceneAsync(2);
    }
    public void Pause()
    {
        AudioManager.Instance.PlayAudio(0);
        FuelController.instance.lowFuel.SetActive(false);
        Time.timeScale = 0;
        pause.SetActive(true);
    }

    public void Resume()
    {
        AudioManager.Instance.StopAudio(0);
        Time.timeScale = 1;
        pause.SetActive(false);
    }
    public void Options()
    {
        MainMenu.instance.scene = SceneManager.GetActiveScene().buildIndex;
        pause.SetActive(false);
        SceneManager.LoadSceneAsync(1,LoadSceneMode.Additive);
    }
    public void Exit()
    {
        SceneManager.LoadSceneAsync(0);
    }
    public void Back()
    {
        pause.SetActive(true);
        SceneManager.UnloadSceneAsync(1);
    }
    public void Tilt()
    {
        left.SetActive(false);
        right.SetActive(false);
        RectTransform rectTransform = brake.GetComponent<RectTransform>();
        rectTransform.anchorMin = Vector2.zero;
        rectTransform.anchorMax = Vector2.zero;
        rectTransform.anchoredPosition = new Vector2(140f, rectTransform.anchoredPosition.y);
    }
    public void Button()
    {
        left.SetActive(true);
        right.SetActive(true);  
        RectTransform rectTransform = brake.GetComponent<RectTransform>();
        rectTransform.anchorMin = new Vector2(1f,0f);
        rectTransform.anchorMax = new Vector2(1f,0f);
        rectTransform.anchoredPosition = new Vector2(-400f, rectTransform.anchoredPosition.y);
    }
    void Update()
    {
        
    }
    public void ForwardDown()
    {
        AudioManager.Instance.PlayAudio(PlayerPrefs.GetInt("SelectedCarIndex") + 6);
        isForwardPressed = true;
    }

    public void ForwardUp()
    {
        AudioManager.Instance.StopAudio(PlayerPrefs.GetInt("SelectedCarIndex") + 6);
        isForwardPressed = false;
    }
    public void BackDown()
    {
        AudioManager.Instance.PlayAudio(PlayerPrefs.GetInt("SelectedCarIndex") + 6);
        isBackPressed = true;

    }

    public void BackUp()
    {
        AudioManager.Instance.StopAudio(PlayerPrefs.GetInt("SelectedCarIndex") + 6);
        isBackPressed = false;
    }
    public void LeftDown()
    {
        isLeftPressed = true;
    }

    public void LeftUp()
    {
        isLeftPressed = false;
    }
    public void RightDown()
    {
        isRightPressed = true;
    }

    public void RightUp()
    {
        isRightPressed = false;
    }
    public void SelectMapBackgroud(int x)
    {
        for(int i = 0;i<backgroud.Length;i++)
        {
            backgroud[i].SetActive(false);
        }
        backgroud[x].SetActive(true);
    }
}
