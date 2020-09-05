using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpUIController : MonoBehaviour
{
    public Button headFrame;


    public Image timeProgressbar;

    public Text timeText, waveText, lifeText, goldText;

    void Awake()
    {
        headFrame = GameObject.Find("HeadFrameButton").GetComponent<Button>();

        timeText = GameObject.Find("LeftTimeText").GetComponent <Text>();
        timeProgressbar = GameObject.Find("LeftTime").GetComponent<Image>();

        waveText = GameObject.Find("WaveText").GetComponent<Text>();
        lifeText = GameObject.Find("LifeText").GetComponent<Text>();
        goldText = GameObject.Find("GoldText").GetComponent<Text>();

    }

    public void ShowLeftTime(float left, float total)
    {
        timeText.text = "time " + (int)left;
        timeProgressbar.fillAmount = left / total;
    }

    public void ShowWave(int current, int total)
    {
        waveText.text = (current + 1) + "/" + total;
    }

    public void ShowWave(int current)
    {
        waveText.text = (current + 1) + "";
    }

    public void ShowLife(int life)
    {
        lifeText.text = life + "";
    }

    public void ShowGold(int gold)
    {
        goldText.text = gold + "";
    }
}
