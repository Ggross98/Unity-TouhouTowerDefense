using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 子弹：从防御塔中心发射，跟踪目标敌人
/// 若飞行过程中敌人消失，销毁自身
/// 击中敌人后，造成伤害，销毁自身
/// </summary>
public class BulletScript : TurretAttack
{
    /*
    public float damage;
    public float speed;
    public int type;
    public const int MAGICAL=11, PHYSICAL=12, NONE = 13;
    public int effect = 0;
    public float effectTime = 0;
    public const int EFFECT_FROZEN = 21;

    public EnemyScript target;

    public bool flying = false;*/


    //飞行速度
    public float speed;
    //是否处于飞行状态，用于子弹飞行检测
    public bool flying = false;
    //击中特效
    public ParticleSystem ps;

    void Start()
    {
        form = BULLET;
    }


    void FixedUpdate()
    {
        if (!flying) return;

        if(target == null)
        {
            Destroy(gameObject);
        }
        else
        {
            if(Vector3.Distance (transform .position , target.transform .position )<= speed*Time.deltaTime )
            {
                if(clip != null)
                {
                    AudioManager.instance.PlaySound(clip);
                }
                if (ps != null)
                {
                    ParticleEffectManager.CreateParticleEffect(ps, this.transform.position, Camera .main.transform );
                }
                target.HitBy(this);
            }
            else
            {
                //this.transform.position += new Vector3(1, 0, 0) * speed * Time.deltaTime;
                this.transform.position += (target.transform.position - this.transform.position).normalized * speed*Time.deltaTime ;
            }
        }
    }

    public void SetBullet(float d, float s, int t, int effect, float effectTime, EnemyScript es) 
    {
        damage = d;
        speed = s;
        type = t;

        this.effect = effect;
        this.effectTime = effectTime;

        target = es;
    }

    public virtual void SetBullet(TurretScript ts)
    {
        this.damage = ts.bulletDamage;
        this.effect = ts.bulletEffect;
        this.effectTime = ts.bulletEffectTime;
        this.type = ts.bulletType;

        this.target = ts.target;
        this.turret = ts;
        
    }


}
