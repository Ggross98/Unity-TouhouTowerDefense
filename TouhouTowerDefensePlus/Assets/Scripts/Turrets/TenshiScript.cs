using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TenshiScript : TurretScript
{
    public GameObject ps;
    public AudioClip clip;
    
    public override TurretAttack Shoot(EnemyScript es)
    {
        //Debug.Log("tenshi attack");

        GameObject particle = Instantiate(ps, transform );
        particle.transform.position = this.transform.position +  new Vector3 (0,-1f);

        if(clip != null)
        {
            AudioManager.instance.PlaySound(clip);
        }

        foreach(EnemyScript enemy in enemyList)
        {
            if(Vector3 .Distance (enemy.transform .position ,transform .position )< this.range)
            {
                enemy.HitBy(this.explosionDamage, this.bulletType, this.explosionEffect, this.explosionEffectTime);
            }
        }

        return null;
    }
}
