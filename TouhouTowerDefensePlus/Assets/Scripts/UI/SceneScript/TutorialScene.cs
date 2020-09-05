using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutorialScene : MonoBehaviour
{
    public Image image;

    private List<Sprite> imageList;

    private int index = 0;

    void Awake()
    {
        imageList = new List<Sprite>();

        imageList.Add(Resources.Load<Sprite>("Tutorial/教程1"));
        imageList.Add(Resources.Load<Sprite>("Tutorial/教程2"));
        imageList.Add(Resources.Load<Sprite>("Tutorial/教程3"));
        imageList.Add(Resources.Load<Sprite>("Tutorial/教程4"));

        Debug.Log(imageList.Count);
        image.sprite  = imageList[0];
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            NextImage();
        }else if(Input.GetMouseButtonDown(1))
        {
            LastImage();
        }
    }

    public void NextImage() {

        index++;
        if (index >= imageList.Count) index = 0;
        image.sprite  = imageList[index];

    }

    public void LastImage() {
        index--;
        if (index < 0) index = imageList .Count -1;
        image.sprite  = imageList[index];

    }

    public void Quit()
    {
        SceneManager.LoadScene("MainMenu"); 
    }

}
