using System.Collections.Generic;
using UnityEngine;

public class CarSelection : MonoBehaviour
{
    public List<GameObject> cars;
    public MainCamera cam;
    public DisplayDistance carPos;
    private void Start()
    {
       
        int selectedCarIndex = PlayerPrefs.GetInt("SelectedCarIndex", 0);

        for (int i = 0; i < cars.Count; i++)
        {
            cars[i].SetActive(false);
        }

        if (selectedCarIndex >= 0 && selectedCarIndex < cars.Count)
        {
            cars[selectedCarIndex].SetActive(true);
            cam.SetTarget(cars[selectedCarIndex].transform);
            carPos.SetTarget(cars[selectedCarIndex].transform);
        }
        else
        {
            Debug.LogError("Indexul mașinii selectate este invalid!");
        }
    }
}
