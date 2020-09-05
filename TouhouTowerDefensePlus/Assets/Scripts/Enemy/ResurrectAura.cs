using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResurrectAura : EnemySkill
{
    public float range = 1;
    public float hpRatio = 0.1f;

    public override void UseSkill()
    {
        ParticleEffectManager.ShowResurrectAuraEffect(transform.position,transform );
        foreach(EnemyScript es in enemyList)
        {
            if(!es.resurrect && es.ID != this.enemy .ID )
            {
                if (Vector3.Distance(es.transform.position, this.transform.position) < range)
                {
                    
                    es.resurrect = true;
                    es.resurrectRatio = this.hpRatio;
                }
                
                
            }
        }
    }
}
