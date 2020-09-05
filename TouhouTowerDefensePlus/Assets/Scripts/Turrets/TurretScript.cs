using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurretScript : MonoBehaviour
{
    public int turretID = 0;

    //塔八个方向的贴图
    //[HideInInspector ]
    public Sprite s0, s1, s2, s3, s4, s5, s6, s7;
    private Sprite[] images;

    public GameObject attackPrefab;
    [HideInInspector]
    public int direction = 4;

    [HideInInspector]
    public int price = 10;


    //类型。分为子弹、爆炸、辅助、激光
    public const int BULLET = 1, EXPLOSION = 2, ASSIST = 3, LASER = 4;
    public int type;



    //子弹伤害
    [HideInInspector]
    public float bulletDamage;
    //子弹速度
    public float bulletSpeed;
    //子弹类型
    public int bulletType;
    //开火间隔
    [HideInInspector]
    public float fireRate;
    //攻击半径，单位：格
    [HideInInspector]
    public float range;
    //攻击特效
    public int bulletEffect = 0;
    [HideInInspector]
    public float bulletEffectTime = 0;

    //爆炸伤害
    [HideInInspector]
    public float explosionDamage;
    //爆炸是否为aoe
    public bool explosionAOE;
    //爆炸半径
    [HideInInspector]
    public float explosionRadius;
    //爆炸特效
    public int explosionEffect;
    public float explosionEffectTime;


    //等级系统
    [HideInInspector]
    public int level = 0, maxLevel;
    //private Text levelText;

    private TurretInfo.TURRET info;
    [HideInInspector]
    public EnemyScript target;
    protected List<EnemyScript> enemyList;

    public float shootCountdown = 0f;

    private StageController stage;


    void Start()
    {
        images = new Sprite[8];
        images[0] = s0; images[1] = s1; images[2] = s2; images[3] = s3; images[4] = s4; images[5] = s5; images[6] = s6; images[7] = s7;

        enemyList = GameObject.Find("Map").GetComponent<EnemyController>().enemyList;

        shootCountdown = fireRate / 2;

        stage = GameObject.Find("Map").GetComponent<StageController>();

        //加载塔的信息
        info = TurretInfo.GetTurretInfo(this.turretID);
        //Debug.Log("info is loaded");
        maxLevel = info.maxLevel;

        price = TurretInfo.GetTurretInfo(this.turretID).price[0];
        //Debug.Log("name: " + info.name);
        SetLevelData(0);

        //Debug.Log(TextEffectManager.instance);
        //GameObject g = TextEffectManager.instance.ShowTurretLevel(this);
        
    }

    public void SetEnemyList(List<EnemyScript > list)
    {
        enemyList = list;
    }

    // Update is called once per frame
    void Update()
    {
        if (!stage.playing) return;


        if (type == ASSIST ) {
            //辅助性炮塔：特殊功能


        }
        //如果攻击类型是子弹/爆炸/激光，寻敌、攻击
        else
        {
            //攻击性炮塔：寻找敌人，发射子弹
            if (target == null)
            {
                //TurnTo(4);
                FindTarget();
            }
            else
            {
                if (InFireRange(target.transform))
                {
                    TurnTo(target.transform);

                    if (shootCountdown <= 0)
                    {
                        Shoot(target);
                        shootCountdown = fireRate;
                    }
                }
                else
                {
                    target = null;
                    FindTarget();
                    //target = null;
                    //TurnTo(4);
                }

            }

        }


        if (shootCountdown > 0)
        {
            shootCountdown -= Time.deltaTime;
            if (shootCountdown < 0) shootCountdown = 0;
        }

    }

    public EnemyScript FindTarget()
    {
        //Debug.Log("Find target...");
        if(enemyList  == null || enemyList .Count == 0)
        {
            return null;
        }
        for(int i = 0; i < enemyList .Count; i++)
        {
            if(InFireRange(enemyList[i].transform))
            {
                target = enemyList[i];
                return target;
            }
        }

        return null;
    }

    public bool InFireRange(Transform t)
    {
        float distance = Vector3.Distance(t.position, this.transform.position);


        return distance <= range+0.1f ;
    }

    public void Shoot()
    {
        shootCountdown = fireRate;
    }

    public void SetPosition(Vector3 pos)
    {
        transform.position = pos;
    }

    public Vector2 GetPosition()
    {
        return transform.position;
    }

    public void LevelUp()
    {
        if (level < maxLevel)
        {
            level++;
            SetLevelData(level);
        }

        //levelText.text = (level >= maxLevel) ? "★" : level + "";

    }
    
    public void SetLevelData(int i)
    {
        int level;
        if (i > maxLevel) level = maxLevel ;
        else level = i;

        if (info.bulletDamage != null && info.bulletDamage .Length >= level)
        {
            this.bulletDamage = info.bulletDamage[level];
            this.bulletEffectTime = info.bulletEffectTime[level];
        }

        if (info.range != null) this.range = info.range[level];
        else this.range = 0;
        //Debug.Log("range = " + info.range[0]);
        if (info.fireRate != null) this.fireRate = info.fireRate[level];
        else this.fireRate = 65536;
        if (info.explosionDamage != null&& info.explosionDamage.Length >= level)
        {
            this.explosionDamage = info.explosionDamage[level];
            this.explosionEffectTime = info.explosionEffectTime[level];
            this.explosionRadius = info.explosionRadius[level];

            this.explosionAOE = info.explosionAOE;
        }
    }

    public int GetSaleGold()
    {
        float rate = 0.5f;
        if (!stage.playing || PlayerInfo .instance .assist == Utils.ASSIST_RINNOSUKE ) rate = 0.75f;

        int total = 0;
        for(int i = 0; i <= level; i++)
        {
            total += info.price[i];
        }

        return  (int)(total * rate);
    }

    public int GetUpdateGold()
    {
        return this.info.price [level+1];
    }


    public void TurnTo(int i)
    {
        direction = i;
        gameObject.GetComponent<SpriteRenderer>().sprite = images[i];
    }

    public void TurnTo(Transform t)
    {
        Vector3 p1 = transform.position;
        Vector3 p2 = t.position;

        Vector3 pos1 = new Vector3(p1.x, 0, p1.y);
        Vector3 pos2 = new Vector3(p2.x, 0, p2.y);

        

        float angle = Vector3.Angle(Vector3.forward , pos2-pos1);
        if (p1.x > p2.x)
        {
            angle = 360-angle;
        }
        //float angle = Utils.GetAngle360(transform.position, t.position);
        //Debug.Log("angle "+ angle);

        int dir;

        if(angle >= 337.5 || angle < 22.5)
        {
            dir = 0;
        }
        else if (angle<67.5)
        {
            dir = 1;
        }
        else if (angle < 112.5)
        {
            dir = 2;
        }
        else if (angle < 157.5)
        {
            dir = 3;
        }
        else if (angle < 202.5)
        {
            dir = 4;
        }
        else if (angle < 247.5)
        {
            dir = 5;
        }
        else if (angle < 292.5)
        {
            dir = 6;
        }
        else
        {
            dir = 7;
        }

        TurnTo(dir);
    }

    /// <summary>
    /// 发射子弹的防御塔在发射时调用
    /// </summary>
    /// <param name="es"></param>
    /// <returns></returns>
    public virtual TurretAttack Shoot(EnemyScript es)
    {
        GameObject attack = Instantiate(attackPrefab);

        if (type == BULLET)
        {
            attack.transform.position = this.transform.position;

            BulletScript bs = attack.GetComponent<BulletScript>();

            bs.SetBullet(this);

            bs.flying = true;
            return bs;
        }
        else if(type == EXPLOSION)
        {
            attack.transform.position = target.transform.position;
            ExplosionScript explode = attack.GetComponent<ExplosionScript>();

            explode.SetExplosion(this);
            explode.Explode();
        }
        else if(type == LASER)
        {
            LaserScript ls = attack.GetComponent<LaserScript>();
            ls.SetLaser(this);
            ls.Emit();
        }

        return null;
        
    }
}
