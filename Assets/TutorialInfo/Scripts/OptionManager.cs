using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionManager : MonoBehaviour
{
    public static OptionManager instance;
    [SerializeField] private GameObject optionMenu;
    [SerializeField] private GameObject ControlMenu;
    private GameObject currentMenu;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void ControlOption()
    {
       
        currentMenu = ControlMenu;
        optionMenu.SetActive(false);
        ControlMenu.SetActive(true);
    }
    public void Back()
    {
        if (MainMenu.instance.scene == 0)
        {
            optionMenu.SetActive(false);
            MainMenu.instance.Back();
        }
        else
        {
            optionMenu.SetActive(false);
            GameManager.instance.Back();
        }    
    }

    public void ControlsBack()
    {
        
        optionMenu.SetActive(true);
        ControlMenu.SetActive(false);

    }
    public void Exit()
    {
        Application.Quit();
    }

}
