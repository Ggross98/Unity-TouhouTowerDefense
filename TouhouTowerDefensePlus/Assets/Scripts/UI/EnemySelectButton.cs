using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySelectButton : SelectButton
{
    public int enemyID = 100;

    void Awake()
    {
        button = GetComponent<Button>();
        image = button.GetComponent<Image>();
        text = button.GetComponent<Text>();

        SetSelected(false);
    }

    public void SetID(int id)
    {
        this.enemyID = id;
        this.image.sprite = EnemyInfo.GetEnemyInfo(id).image;
        if (id == 0) this.image.color = Color.clear;
        else this.image.color = Color.white;
    }
}
