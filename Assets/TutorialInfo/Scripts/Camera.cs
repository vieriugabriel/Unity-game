using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    public Transform target;
    public Transform[] RightBorder;
    public Vector3 offset;
    private float LeftBorder=-32f;
    public float x;
    public float y;
    public float z;
    void Start()
    {
        offset = transform.position - target.position;
        offset.x = 5;
        x = Camera.main.orthographicSize * Screen.width / Screen.height;
        LeftBorder += x;
    }

    void FixedUpdate()
    {
        
        Vector3 targetPosition = target.position + offset;
        float clampedX = Mathf.Clamp(targetPosition.x, LeftBorder, RightBorder[PlayerPrefs.GetInt("MapIndex")].position.x-5);
        transform.position = new Vector3(clampedX, targetPosition.y, targetPosition.z);
    }
    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }
}
