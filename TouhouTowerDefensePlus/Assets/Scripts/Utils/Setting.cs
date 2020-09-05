using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Setting : MonoBehaviour
{
    public GameObject resolution;
    private Dropdown r;

    public Toggle fullscreen;
    private bool fc;

    public GameObject volume_background, volume_sound;
    private Slider slider_bgm, slider_se;
    public Text text_bgm, text_se;
    
    void Awake()
    {
        
        slider_bgm = volume_background.GetComponentInChildren<Slider>();
        slider_se = volume_sound.GetComponentInChildren<Slider>();

        gameObject.SetActive(false);
    }

    void Update()
    {
        text_bgm.text = slider_bgm.value + "";
        text_se.text = slider_se.value + "";
    }

    void OnEnable()
    {
        float v_bgm = AudioManager.instance.musicVolume;
        float v_se = AudioManager.instance.soundVolume;
        SetSlider(volume_background, (int)(v_bgm * 100));
        SetSlider(volume_sound, (int)(v_se * 100));
    }

    private void SetSlider(GameObject obj, int v)
    {
        if (obj.GetComponentInChildren<Slider>() == null) return;
        obj.GetComponentInChildren<Slider>().value = v;
        //obj.GetComponentInChildren<Text>().text = v + "";
    }

    public void HidePanel()
    {
        this.gameObject.SetActive(false);
    }

    public void ShowSettingPanel()
    {

        this.gameObject.SetActive(true);

        this.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
    }

    public void Apply()
    {
        float v_bgm = slider_bgm .value ;
        float v_se = slider_se .value ;
        AudioManager.instance.soundVolume = v_se  / 100f;
        AudioManager.instance.musicVolume  = v_bgm / 100f;
        AudioManager.instance.SetVolume();

        HidePanel();
    }

    public void Cancel()
    {
        SetSlider(volume_background, 50);
        SetSlider(volume_sound,50);
    }
    
}
