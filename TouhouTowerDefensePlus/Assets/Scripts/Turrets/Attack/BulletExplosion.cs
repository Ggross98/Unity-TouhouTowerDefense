using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 击中敌人后爆炸的子弹
/// </summary>
public class BulletExplosion : BulletScript
{
    public GameObject explosionPrefab;

    public float eDamage, eRadius;
    public int eEffect;
    public float eEffectTime;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void FixedUpdate()
    {
        if (!flying) return;

        if (target == null)
        {
            CreateExplosion();
            Destroy(gameObject);
        }
        else
        {
            if (Vector3.Distance(transform.position, target.transform.position) <= speed * Time.deltaTime)
            {

                CreateExplosion();
                target.HitBy(this);

                

            }
            else
            {
                //this.transform.position += new Vector3(1, 0, 0) * speed * Time.deltaTime;
                this.transform.position += (target.transform.position - this.transform.position).normalized * speed * Time.deltaTime;
            }
        }
    }

    private ExplosionScript CreateExplosion()
    {
        ExplosionScript es = Instantiate(explosionPrefab).GetComponent<ExplosionScript>();
        es.SetExplosion(this);
        es.Explode();


        return null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void SetBullet(TurretScript ts)
    {
        this.damage = ts.bulletDamage;
        this.effect = ts.bulletEffect;
        this.effectTime = ts.bulletEffectTime;
        this.type = ts.bulletType;

        this.eDamage = ts.explosionDamage;
        this.eEffect = ts.explosionEffect;
        this.eRadius = ts.explosionRadius;
        this.eEffectTime = ts.explosionEffectTime;

        this.target = ts.target;
        this.turret = ts;

    }
}
