using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPBar : MonoBehaviour
{
    public EnemyScript enemy;

    public GameObject bar_in, bar_out;

    void Update()
    {
        if(enemy.cursed)
        {
            //Debug.Log("show cursed color");
            bar_in.GetComponent<SpriteRenderer>().color = Color.gray ;
            bar_out.GetComponent<SpriteRenderer>().color = Color.gray;
        }

        float percent = enemy.hp / enemy.maxHp;
        if(percent == 1)
        {
            this.transform.localScale = new Vector3(0, 0, 0);
        }
        else
        {
            this.transform.localScale = new Vector3(0.3f, 0.25f, 1f);
        }
        
        bar_in.transform.localScale = new Vector3(percent, 1,1);
    }

}
