using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHeal : EnemySkill
{
    public bool aoe = false;
    public float range = 0;

    //按定值治疗，按自身的生命值上限治疗，按目标的生命值上限治疗
    public float heal_value = 0, heal_own_ratio = 0, heal_target_ratio = 0;
    

    public override void UseSkill()
    {
        if(enemyList != null)
        {
            ParticleEffectManager.ShowHealEffect(this.transform.position + new Vector3(0, 0.5f), this.transform );

            if (aoe)
            {
                foreach(EnemyScript ec in enemyList)
                {
                    if(Vector3.Distance (ec.transform .position ,this.transform .position) <= range)
                    {
                        Heal(ec);
                    }
                }
            }
            else
            {
                Heal(enemy);
            }
            
        }
    }

    private float Heal(EnemyScript target)
    {
        if (target.cursed) return 0;

        float heal = heal_value;

        heal += this.enemy.maxHp * heal_own_ratio;
        heal += target.maxHp * heal_target_ratio;

        target.AddHP(heal);

        return heal;
    }
}
