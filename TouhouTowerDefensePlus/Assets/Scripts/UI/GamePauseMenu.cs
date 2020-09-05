using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePauseMenu : MonoBehaviour
{
    StageController stage;

    [SerializeField ]
    Slider v_bgm, v_se;
    [SerializeField]
    Text text_bgm, text_se;
    [SerializeField]
    Button exit, restart, close;

    void Awake()
    {
        stage = GameObject.Find("Map").GetComponent<StageController>();

        exit.onClick.AddListener(

            delegate {
                Apply();
                stage.Exit();

            }

            );

        restart.onClick.AddListener(

           delegate {
               stage.Restart();
               Apply();
           }

           );

        close.onClick.AddListener(

           delegate {
               //stage.Resume();
               stage.ShowPauseMenu(false);
               //stage.Resume();
               Apply();
           }

           );

        //v_bgm = GameObject.Find("volume_bgm").GetComponent<Slider>();
        //v_se = GameObject.Find("volume_sound").GetComponent<Slider>();
        //text_bgm = GameObject.Find("text_bgm").GetComponent<Text>();
        //text_se = GameObject.Find("text_sound").GetComponent<Text>();

        gameObject.SetActive(false);
    }

    void Update()
    {
        if(!isActiveAndEnabled)
        {
            return;
        }
        text_bgm.text = v_bgm.value + "";
        text_se.text = v_se.value + "";
        
    }

    void OnEnable()
    {
        float bgm = AudioManager.instance.musicVolume;
        float se = AudioManager.instance.soundVolume;
        SetSlider(v_bgm , (int)(bgm * 100));
        SetSlider(v_se, (int)(se * 100));
    }

    void SetSlider(Slider obj, int v)
    {
        obj.value = v;
        //obj.GetComponentInChildren<Text>().text = v + "";
    }

    public void Apply()
    {
        float bgm = v_bgm.value;
        float se = v_se.value;
        AudioManager.instance.soundVolume = se / 100f;
        AudioManager.instance.musicVolume = bgm / 100f;
        AudioManager.instance.SetVolume();
    }
}
