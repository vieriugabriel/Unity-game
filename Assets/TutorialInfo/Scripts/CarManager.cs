using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Reflection;
using UnityEngine;

[System.Serializable]
public class Cars
{
    public string carName;
    public GameObject carObject;
    public int cost;
    public bool isUnlocked=false;
}
public class CarManager : MonoBehaviour
{
    public static CarManager instance;
    public List<Cars> cars;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        //SaveCarData();
        LoadCarData();
    }

    public void LoadCarData()
    {
        for (int i = 0; i < cars.Count; i++)
        {
            cars[i].isUnlocked = PlayerPrefs.GetInt("CarUnlocked" + i, i == 0 ? 1 : 0) == 1;
            if (cars[i].isUnlocked)
            {
                if (cars[i].carObject != null)
                {
                    Destroy(cars[i].carObject);
                }
            }
        }
    }

    public void SaveCarData()
    {
        for (int i = 0; i < cars.Count; i++)
        {
            PlayerPrefs.SetInt("CarUnlocked" + i, cars[i].isUnlocked ? 1 : 0);
        }
        
    }
    private void Update()
    {
        for (int i = 0; i < cars.Count; i++)
            if(PlayerPrefs.GetInt("CarUnlocked" + i)==1)
            {
                Destroy(cars[i].carObject);
            }
    }
    public bool UnlockCar(int index)
    {
        int money = PlayerPrefs.GetInt("Coins", 0);
        if (cars[index].cost <= money)
        {
            PlayerPrefs.SetInt("Coins",money-cars[index].cost);
            ScoreManager.instance.UpdateMenuScore();
            cars[index].isUnlocked = true;
            SaveCarData();
            if (cars[index].carObject != null)
            {
                Destroy(cars[index].carObject);
            }
            return true;
        }
      return false;
        
    }
}
