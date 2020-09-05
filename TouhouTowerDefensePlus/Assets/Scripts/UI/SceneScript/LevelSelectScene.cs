using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class LevelSelectScene : MonoBehaviour
{
    public int level = 0;
    public GameObject lv0, lv1, lv2,lv3,lv4,lv5;
    private GameObject[] levelButtons;

    public Text mapName, mapInfo;

    public GameObject pin;

    // Start is called before the first frame update
    void Start()
    {
        levelButtons = new GameObject[10];
        levelButtons[0] = lv0; levelButtons[1] = lv1; levelButtons[2] = lv2;
        levelButtons[3] = lv3; levelButtons[4] = lv4; levelButtons[5] = lv5;
        ReadPlayerInfo();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SelectLevel(int i)
    {
        level = i;
        pin.GetComponent<RectTransform>().anchoredPosition = levelButtons[i].GetComponent<RectTransform>().anchoredPosition + new Vector2(0,60);
        ShowMapInfo(i);
    }

    private void ShowMapInfo(int i)
    {
        mapName.text = Utils.GetMapName(i);
        mapInfo.text = Utils.GetMapInfo(i);
    }

    public void NextScene()
    {
        WritePlayerInfo();
        DontDestroyOnLoad(PlayerInfo.instance);
        SceneManager.LoadScene("RoleSelect");

    }

    public void Quit()
    {
        Destroy(PlayerInfo.instance.gameObject);
        SceneManager.LoadScene("MainMenu");
    }

    private void ReadPlayerInfo()
    {
        if(PlayerInfo.instance != null)
        {
            SelectLevel(PlayerInfo.instance.level);

            GameObject.Find("EndlessToggle").GetComponent<Toggle>().isOn = PlayerInfo.instance.endless;
        }
    }

    private void WritePlayerInfo()
    {
        PlayerInfo.instance.level = this.level;
        PlayerInfo.instance.endless = GameObject.Find("EndlessToggle").GetComponent<Toggle>().isOn;
    }
}
