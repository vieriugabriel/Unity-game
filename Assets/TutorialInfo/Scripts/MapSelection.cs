using System.Collections.Generic;
using UnityEngine;

public class MapSelection : MonoBehaviour
{
    public List<GameObject> map;

    private void Start()
    {

        int selectedMapIndex = PlayerPrefs.GetInt("MapIndex", 0);

        for (int i = 0; i < map.Count; i++)
        {
            map[i].SetActive(false);
        }

        if (selectedMapIndex >= 0 && selectedMapIndex < map.Count)
        {
            map[selectedMapIndex].SetActive(true);
        }
        else
        {
            Debug.LogError("Indexul hartii selectate este invalid!");
        }
    }
}
