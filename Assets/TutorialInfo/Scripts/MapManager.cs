using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

[System.Serializable]
public class Map
{
    public string mapName;
    public GameObject mapObject;
    public int cost;
    public bool isUnlocked = false;
}
public class MapManager : MonoBehaviour
{
    public static MapManager instance;
    public List<Map> map;

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
        LoadMapData();
    }

    public void LoadMapData()
    {
        for (int i = 0; i < map.Count; i++)
        {
            map[i].isUnlocked = PlayerPrefs.GetInt("MapUnlocked" + i, i == 0 ? 1 : 0) == 1;
            if (map[i].isUnlocked)
            {
                if (map[i].mapObject != null)
                {
                    Destroy(map[i].mapObject);
                }
            }
        }
    }

    public void SaveMapData()
    {
        for (int i = 0; i < map.Count; i++)
        {
            PlayerPrefs.SetInt("MapUnlocked" + i, map[i].isUnlocked ? 1 : 0);
        }

    }

    public bool UnlockMap(int index)
    {
        int money = PlayerPrefs.GetInt("Coins", 0);
        if (map[index].cost <= money)
        {
            PlayerPrefs.SetInt("Coins", money - map[index].cost);
            ScoreManager.instance.UpdateMenuScore();
            map[index].isUnlocked = true;
            SaveMapData();
            if (map[index].mapObject != null)
            {
                Destroy(map[index].mapObject);
            }
            return true;
        }
        return false;

    }
}
