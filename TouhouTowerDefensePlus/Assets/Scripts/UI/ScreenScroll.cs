using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenScroll : MonoBehaviour
{
    public float speed;
    //public float xDir = -1;
    private RectTransform rt;

    void Start()
    {
        rt = GetComponent<RectTransform>();
    }

    void Update()
    {
        rt.anchoredPosition -= new Vector2(speed, 0);
        if(rt.anchoredPosition == new Vector2(-1920, 0))
        {
            rt.anchoredPosition = new Vector2(1920, 0);
        }
    }
}
