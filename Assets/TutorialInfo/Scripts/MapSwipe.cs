using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MapSwipe : MonoBehaviour
{
    public GameObject scrollbar;
    public float scroll_pos ;
    public float []pos;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("MapScrollPos"))
        {
            scroll_pos = PlayerPrefs.GetFloat("MapScrollPos");
            scrollbar.GetComponent<Scrollbar>().value = scroll_pos;
        }
        else
        {
            scroll_pos = 0.0f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        pos = new float[transform.childCount];

        float distance = 1f / (pos.Length - 1f);
        for (int i = 0; i < pos.Length; i++)
        {
            pos[i] = distance * i;
        }
        if (Input.GetMouseButton(0))
        {
            scroll_pos = scrollbar.GetComponent<Scrollbar>().value;
        }
        else
        {
            for (int i = 0; i < pos.Length; i++)
            {
                if (scroll_pos < pos[i] + (distance / 2) && scroll_pos > pos[i] - (distance / 2))
                {
                    scrollbar.GetComponent<Scrollbar>().value = Mathf.Lerp(scrollbar.GetComponent<Scrollbar>().value, pos[i], 0.1f);
                }
            }
        }

        for (int i = 0; i < pos.Length; i++)
        {
            if (scroll_pos < pos[i] + (distance / 2) && scroll_pos > pos[i] - (distance / 2))
            {
                transform.GetChild(i).localScale = Vector2.Lerp(transform.GetChild(i).localScale, new Vector2(1f, 1f), 0.1f);
                MainMenu.instance.mapIndex = i;
                
                for (int j = 0; j < pos.Length; j++)
                {
                    if (j != i)
                    {
                        transform.GetChild(j).localScale = Vector2.Lerp(transform.GetChild(j).localScale, new Vector2(0.8f, 0.8f), 0.1f);
                    }
                }
            }
        }
    }
    void OnDisable()
    {
        PlayerPrefs.SetFloat("MapScrollPos", scroll_pos);
        PlayerPrefs.Save();
    }
}
