using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurretSelectButton : SelectButton
{
    
    public int turretID = 100;
    
    void Awake()
    {
        button = GetComponent<Button>();
        image = button.GetComponent<Image>();
        text = button.GetComponent<Text>();

        SetSelected(false);
    }

    public void SetID(int id)
    {
        this.turretID = id;
        this.image.sprite = TurretInfo.GetSelectButtonImage(id);
        if (id == 0) this.image.color = Color.clear;
        else this.image.color = Color.white;
    }

}
