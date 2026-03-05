using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class DisplayDistance : MonoBehaviour
{
    public TextMeshProUGUI distanceText;
    public Transform carPos;
    public Transform[] finish;
    private Vector2 startPos;
    private Vector2 endPos;

    private void Start()
    {
        startPos = carPos.position;

        endPos = finish[PlayerPrefs.GetInt("MapIndex")].position;
    }
    private void Update()
    {
        Vector2 distance = (Vector2)carPos.position - startPos;
        distance.y = 0f;
        Vector2 vector2 = endPos - startPos;
        if(distance.x<0f) 
            distance.x = 0f;

        distanceText.text = distance.x.ToString("F0") + "m/"+ vector2.x.ToString("F0")+"m";
        if(distance.x>= vector2.x)
            GameManager.instance.Finish();
    }
    public void SetTarget(Transform newTarget)
    {
        carPos = newTarget;
    }

}
