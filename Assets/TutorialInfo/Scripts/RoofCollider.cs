using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoofCollider : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            if(PlayerPrefs.GetInt("SelectedCarIndex")==0)
                AudioManager.Instance.PlayAudio(2);
            else
                AudioManager.Instance.PlayAudio(3);
            GameManager.instance.GameOver();
        }
    }
}
