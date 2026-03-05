using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlSettings : MonoBehaviour
{
    public static ControlSettings instance;
    public Button tiltButton;
    public Button buttonButton;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }


    private void Start()
    {
        string controlMethod = PlayerPrefs.GetString("ControlMethod", "Button");
        if (controlMethod == "Tilt")
        {
            tiltButton.interactable = false;
            buttonButton.interactable = true;
        }
        else if (controlMethod == "Button")
        {
            tiltButton.interactable = true;
            buttonButton.interactable = false;
        }
    }
    private void Update()
    {
       
    }
    
    public void OnTiltButtonClicked()
    {
        SetControl("Tilt");
    }

    public void OnButtonButtonClicked()
    {
        SetControl("Button");
    }

    private void SetControl(string control)
    {
        PlayerPrefs.SetString("ControlMethod", control);
        PlayerPrefs.Save();
        ApplyControlMethod(control);
       
    }

    public void ApplyControlMethod(string controlMethod)
    {
        if (controlMethod == "Tilt")
        {
            tiltButton.interactable = false;
            buttonButton.interactable = true;
        }
        else if (controlMethod == "Button")
        {
            tiltButton.interactable = true;
            buttonButton.interactable = false;
        }
        if (GameManager.instance != null)
        {
            GameManager.instance.SetControlMethod(controlMethod);
        }
    }
}
