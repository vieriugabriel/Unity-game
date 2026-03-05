using Firebase.Database;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using System;
using UnityEditor;
public class DatabaseManager : MonoBehaviour
{
    public static DatabaseManager instance;
    private string UserID;
    private DatabaseReference dbReference;
    User user;
    private void Awake()
    {
        if (instance == null)
        { 
            instance = this; 
            DontDestroyOnLoad(gameObject);
        }
        dbReference = FirebaseDatabase.DefaultInstance.RootReference;
    }
    private void Start()
    {
        InitializeUser();
    }

    // Initialize the user object if it is not already initialized
    private void InitializeUser()
    {
        if (user == null)
        {
            user = new User("Button", new int[4], 0, 0f, 0, new int[2], 0f, 0, 1f);
        }
    }
    public void CreateUser(string UserID)
    {
        
        if (dbReference == null)
        {
            Debug.LogError("dbReference is not initialized.");
            return;
        }
        user = new User("Button", new int[4], 0, 0f, 0, new int[2], 0f, 0, 1f);
        string json = JsonUtility.ToJson(user);

        dbReference.Child("Users").Child(UserID).SetRawJsonValueAsync(json);

        Init();
    }
    public void SaveData()
    {
        if (user == null)
        {
            Debug.LogError("User is not initialized.");
            return;
        }
        UserID = PlayerPrefs.GetString("User");
        Save(user);
        string json = JsonUtility.ToJson(user);

        dbReference.Child("Users").Child(UserID).SetRawJsonValueAsync(json);
        
    }
    public void LoadData()
    {
        StartCoroutine(LoadDataEnum());
    }
    IEnumerator LoadDataEnum()
    {
        var data = dbReference.Child("Users").Child(PlayerPrefs.GetString("User")).GetValueAsync();
        yield return new WaitUntil(predicate: ()=> data.IsCompleted);

        DataSnapshot snapshot =data.Result;
        string json = snapshot.GetRawJsonValue();

        if (json != null)
        {
            print("data found");
            user = JsonUtility.FromJson<User>(json);
            Load(user);
        }
        else
            print("data not found");
    }
    private void Load(User user)
    {
        if (user == null)
        {
            Debug.LogError("User is null in Load method");
            return;
        }

        PlayerPrefs.SetString("ControlMethod", user.controlMethod);

        if (user.carUnlocked == null || user.carUnlocked.Length < 4)
        {
            Debug.LogError("carUnlocked array is not properly initialized.");
            user.carUnlocked = new int[4];
        }

        for (int i = 0; i < 4; i++)
        {
            PlayerPrefs.SetInt("CarUnlocked" + i, user.carUnlocked[i]);
        }

        PlayerPrefs.SetInt("SelectedCarIndex", user.selectedCarIndex);
        PlayerPrefs.SetFloat("ScrollPos", user.scrollPos);
        PlayerPrefs.SetInt("MapIndex", user.mapIndex);

        if (user.mapUnlocked == null || user.mapUnlocked.Length < 2)
        {
            Debug.LogError("mapUnlocked array is not properly initialized.");
            user.mapUnlocked = new int[2];
        }

        for (int i = 0; i < 2; i++)
        {
            PlayerPrefs.SetInt("MapUnlocked" + i, user.mapUnlocked[i]);
        }

        PlayerPrefs.SetFloat("MapScrollPos", user.mapScrollPos);
        PlayerPrefs.SetInt("Coins", user.coins);
        PlayerPrefs.SetFloat("sound", user.sound);
        PlayerPrefs.Save();
    }

    private void Save(User user)
    {
        user.controlMethod = PlayerPrefs.GetString("ControlMethod");

        if (user.carUnlocked == null || user.carUnlocked.Length < 4)
        {
            user.carUnlocked = new int[4];
        }

        for (int i = 0; i < 4; i++)
        {
            user.carUnlocked[i] = PlayerPrefs.GetInt("CarUnlocked" + i);
        }

        user.selectedCarIndex = PlayerPrefs.GetInt("SelectedCarIndex");
        user.scrollPos = PlayerPrefs.GetFloat("ScrollPos");
        user.mapIndex = PlayerPrefs.GetInt("MapIndex");

        if (user.mapUnlocked == null || user.mapUnlocked.Length < 2)
        {
            user.mapUnlocked = new int[2]; 
        }

        for (int i = 0; i < 2; i++)
        {
            user.mapUnlocked[i] = PlayerPrefs.GetInt("MapUnlocked" + i);
        }

        user.mapScrollPos = PlayerPrefs.GetFloat("MapScrollPos");
        user.coins = PlayerPrefs.GetInt("Coins");
        user.sound = PlayerPrefs.GetFloat("sound");
    }
    private void Init()
    {
        PlayerPrefs.SetString("ControlMethod", "Button");
        for (int i = 0; i < 4; i++)
        {
            PlayerPrefs.SetInt("CarUnlocked" + i, 0);
        }
        PlayerPrefs.SetInt("SelectedCarIndex", 0);
        PlayerPrefs.SetFloat("ScrollPos", 0);
        PlayerPrefs.SetInt("MapIndex", 0);
        for (int i = 0; i < 2; i++)
        {
            PlayerPrefs.SetInt("MapUnlocked" + i, 0);
        }
        PlayerPrefs.SetFloat("MapScrollPos", 0);
        PlayerPrefs.SetInt("Coins", 0);
        PlayerPrefs.SetFloat("sound", 1);
        PlayerPrefs.Save();
    }
}
