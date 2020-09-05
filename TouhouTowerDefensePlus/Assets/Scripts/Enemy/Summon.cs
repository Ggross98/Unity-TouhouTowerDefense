using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Summon : EnemySkill 
{
    public int summonID;

    public override void UseSkill()
    {
        if(enemyList .Count <200)
            GameObject.Find("Map").GetComponent<EnemyController>().CreateEnemy(summonID , transform .position );
    }
}
