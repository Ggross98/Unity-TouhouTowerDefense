using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillBar : MonoBehaviour
{
    public TurretScript turret;

    public GameObject bar_in, bar_out;

    void Update()
    {
        float percent = turret.shootCountdown/turret.fireRate ;
        

        {
            this.transform.localScale = new Vector3(0.3f, 0.25f, 1f);
        }

        bar_in.transform.localScale = new Vector3(percent, 1, 1);
    }
}
