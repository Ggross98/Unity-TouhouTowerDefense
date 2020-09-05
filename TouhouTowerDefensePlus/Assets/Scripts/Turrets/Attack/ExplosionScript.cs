using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionScript : TurretAttack
{
    //爆炸是否是aoe
    public bool aoe = false;
    //爆炸半径。如果是单体则此属性无效
    public float radius = 1;
    //粒子效果
    //public ParticleSystem ps;
    //存在时间：爆炸无论是否击中目标，会在播放完粒子效果后销毁
    public float lifetime = 5f;


    // Start is called before the first frame update
    void Start()
    {
        form = EXPLOSION;
    }

    // Update is called once per frame
    void Update()
    {
        if(target != null)
        {
            transform.position = target.transform.position;
        }
    }

    public void Explode()
    {
        Destroy(this.gameObject, lifetime);

        if (clip != null)
        {
            AudioManager.instance.PlaySound(clip);
        }

        ParticleSystem ps = GetComponentInChildren<ParticleSystem>();
        if (ps != null) ps.Play();

        if (aoe)
        {
            List < EnemyScript >  enemyList = GameObject.Find("Map").GetComponent<EnemyController>().enemyList;
            for(int i = 0; i < enemyList.Count ; i++)
            {
                if(Mathf .Abs(Vector2.Distance(enemyList[i].transform.position, transform.position)) < radius)
                {
                    enemyList[i].HitBy(this);
                }
            }
        }
        else
        {
            if(target != null)
            {
                target.HitBy(this);
            }
        }
    }


    public void SetExplosion(float d, bool aoe, float r, int effect, float effectTime, EnemyScript es)
    {
        damage = d;
        this.aoe = aoe;
        radius = r;

        this.effect = effect;
        this.effectTime = effectTime;

        target = es;
        if(target !=null)
            transform.position = target.transform.position;
    }

    public void SetExplosion(TurretScript ts)
    {
        this.damage = ts.explosionDamage;
        this.aoe = ts.explosionAOE;
        this.radius = ts.explosionRadius;

        this.effect = ts.explosionEffect;
        this.effectTime = ts.explosionEffectTime;
        this.turret = ts;
        this.target = ts.target;
        if (target != null)
            transform.position = target.transform.position;
    }

    public void SetExplosion(BulletExplosion be)
    {
        TurretScript ts = be.turret;

        this.damage = be.eDamage ;
        this.aoe = ts.explosionAOE;
        this.radius = be.eRadius ;

        this.effect = be.eEffect ;
        this.effectTime = be.eEffectTime ;
        this.turret = ts;

        this.target = be.target;
        if (target != null)
            transform.position = target.transform.position;
        else
        {
            transform.position = be.transform.position;
        }
    }
}
