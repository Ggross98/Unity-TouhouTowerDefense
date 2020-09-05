using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectButton : MonoBehaviour
{

    public Button button;
    public Image image;
    public Text text;
    //public int turretID = 100;

    public int index;
    public bool selected;


    void Awake()
    {
        button = GetComponent<Button>();
        image = button.GetComponent<Image>();
        text = button.GetComponent<Text>();

        SetSelected(false);
    }



    public void SetSelected(bool i)
    {
        selected = i;
        if (i)
        {

            image.color = Color.white;
        }
        else
        {
            image.color = Color.gray;
        }
    }


}
