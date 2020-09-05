using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GainGold : MonoBehaviour
{
    public TurretScript ts;

    public int gold = 20;

    void Update()
    {
        if(ts.shootCountdown <= 0)
        {
            this.gold = (int)ts.bulletDamage;
            if (GameObject.Find("Map").GetComponent<StageController>().gold < 5000)
                Gain(this.gold);
            else Gain(0);
        }
    }

    public void Gain(int i)
    {
        GameObject.Find("Map").GetComponent<StageController>().gold += i;
        ts.shootCountdown = ts.fireRate;

        TextEffectManager.instance.ShowTextAtPosition("+" + gold, Color.yellow, 20, this.transform.position + new Vector3(0, 0.5f), 0.4f);
    }


}
