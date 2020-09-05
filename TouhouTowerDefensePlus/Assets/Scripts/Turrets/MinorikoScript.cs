using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 秋穰子的脚本
/// 范围内有敌人被击杀时获得金钱
/// </summary>
public class MinorikoScript : TurretScript
{
    public void Gain(float i)
    {
        GameObject.Find("Map").GetComponent<StageController>().gold += (int)i;
        //ts.shootCountdown = ts.fireRate;


        TextEffectManager.instance.ShowTextAtPosition("+" + i, Color.yellow, 20, this.transform.position + new Vector3(0, 0.5f), 0.4f);
    }

    public void Gain()
    {
        Gain(this.bulletDamage);
    }
}
