using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{

    public static int UP = 0, RIGHT = 1, DOWN = 2, LEFT = 3;

    public int ID;
    public int reward = 5;
    public float hp = 200, maxHp=200;
    public float cellTime = 0.05f;
    //public float distance = 1f;
    private Animator anim;

    private int direction = 1;
    private int directionID;
    private bool moving = false;

    public float physicalResist = 0, magicalResist = 0;

    //地图控制器
    //public GameObject MapObject;
    public MapController mapController;

    public PathFinder pathFinder;

    private int[,] map;


    private Path path;

    //冷冻：此状态下减速60%，与烧伤互斥
    private float frozenTime = 0f;
    public bool frozenResist = false;

    //烧伤：此状态下持续掉血，与冷冻互斥
    private float burnedTime = 0f;
    private float burnCountdown = 0;
    public bool burnedResist = false;

    //中毒：此状态下持续掉血
    private float poisonedTime = 0f;
    public bool poisonedResist = false;
    private float poisonedCountdown = 0;

    //眩晕：此状态下无法行动，也无法使用技能
    private float stunedTime = 0f;
    public bool stunedResist = false;

    //复活：有此标记的怪物在死后复活
    public bool resurrect = false;
    public float resurrectRatio = 0.5f;

    //诅咒：有次标记的怪物无法受到治疗、不能复活
    public bool cursed = false;

    private bool flashing = false;

    void Awake()
    {
        //mapController = MapObject.GetComponent<MapController>();

        anim = GetComponent<Animator>();
        directionID = Animator.StringToHash("direction");
        TurnTo(1);

        pathFinder = GameObject.Find("Map").GetComponent<PathFinder>();
        path = pathFinder.defaultPath;

        gameObject.layer = 10;
    }

    // Update is called once per frame
    void Update()
    {
        Move();

        if(flashing)
        {
            GetComponent<SpriteRenderer>().color = Color.gray;
        }
        else
        {
            if(frozenTime > 0)
            {
                GetComponent<SpriteRenderer>().color = Color.blue;
            }
            else if(burnedTime > 0)
            {
                GetComponent<SpriteRenderer>().color = new Color(1,144f/255f,0,1);
            }
            else
            {
                GetComponent<SpriteRenderer>().color = Color.white;
            }
        }

        //冰冻
        if (frozenTime > 0) frozenTime -= Time.deltaTime;

        //眩晕
        if (stunedTime > 0) stunedTime -= Time.deltaTime;

        //烧伤：
        if(burnedTime > 0)
        {
            if (burnCountdown > 0) burnCountdown -= Time.deltaTime;
            else
            {
                int burn = (int)burnedTime;
                
                float damage = (1f - magicalResist / 100f) * burn * 10;
                hp -= damage;
                Debug.Log("burn" + damage);
                
                burnedTime -= 3;
                if (burnedTime < 0) burnedTime = 0;

                burnCountdown = 1;
            }

            
        }
    }

    public void SetPositionInCell(Vector3Int cPos)
    {
        SetPosition(mapController.CellToWorld(cPos));
    }


    public int GetX()
    {
        return mapController.WorldToArray(transform.position).x;
    }

    public int GetY()
    {
        return mapController.WorldToArray(transform.position).y;
    }

    //检测一个方向是否能够前进：仅当方向上下一个格子在地图数组中为1时可行
    
    //public bool CanMove(int newDirection)
    //{
    //    int x = GetX();
    //    int y = GetY();
    //    //Debug.Log("x" + x + "y" + y);

    //    switch(newDirection)
    //    {
    //        case 0:
    //            y++;
    //            break;
    //        case 1:
    //            x++;
    //            break;
    //        case 2:
    //            y--;
    //            break;
    //        case 3:
    //            x--;
    //            break;
    //    }

        
    //    if(x<0||y<0||x >=mapController.column || y >=mapController.row)
    //    {
    //        return false;
    //    }
    //    //Debug.Log(x + "," + y + ":" + (mapController.GetMapArray())[x, y]);
    //    if ((mapController.GetMapArray())[x, y] != Utils.GROUND  && (mapController.GetMapArray())[x, y] != Utils.ROAD ) return false;


    //    return true;
    //}


    //走完一个格子后进行的检测：走最短的路径
    //public int GetNewDirection()
    //{
    //    if (CanMove(direction)) return direction;
    //    return ChangeDirection();
    //}


    //改变至一个可以行动的方向，优先选择不掉头

    //protected int ChangeDirection()
        
    //{
    //    int[] newDirection= new int[2];
    //    int reverse = 0;

    //    //生成可选方向
    //    if(direction == 0)
    //    {
    //        reverse = 2;
    //        newDirection[0] = 1;
    //        newDirection[1] = 3;

    //    }
    //    else if (direction == 1)
    //    {
    //        reverse = 3;
    //        newDirection[0] = 2;
    //        newDirection[1] = 0;

    //    }
    //    else if (direction == 2)
    //    {
    //        reverse = 0;
    //        newDirection[0] = 3;
    //        newDirection[1] = 1;

    //    }
    //    else if (direction == 3)
    //    {
    //        reverse = 1;
    //        newDirection[0] = 0;
    //        newDirection[1] = 2;

    //    }

    //    if(CanMove (newDirection[0]))
    //    {
    //        return newDirection[0];
    //    }
    //    else if(CanMove(newDirection[1]))
    //    {
    //        return newDirection[1];
    //    }

        
    //    return reverse;

    //}


    //控制运动的方法
    protected void Move()
    {
        /*
        if(transform.position == mapController .CellToWorld (mapController .WorldToCell(transform .position )))
        {
            direction = GetNewDirection();
            TurnTo(direction);

        }
        Vector3 directionVector = GetDirectionVector();
        transform.position = transform.position + directionVector *distance*Time.deltaTime /cellTime;
        */
        if (!moving)
        {
            //if (path == null) path = pathFinder.FindPathToGoal (new Vector2Int(GetX(), GetY()));
            path = pathFinder.defaultPath;

            int index = path.OnPath(GetX(), GetY());
            
            if(index<0)
            {
                path = pathFinder.FindPathToGoal(new Vector2Int(GetX(), GetY()));
                TurnTo(path.GetDirection(0));
            }
            else
            {
                TurnTo(path.GetDirection (index));
            }


            //Debug.Log("direction: " + direction +)；
            //TurnTo(pathFinder.EnemyNextDirection(new Vector2Int (GetX(),GetY())));
            //TurnTo(GetNewDirection());
            StartCoroutine(MoveOneCell());
        }
    }

    //根据方向前进一格
    protected IEnumerator MoveOneCell()
    {
        
        moving = true;

        Vector3 pos1 = mapController.CellToWorld(mapController.WorldToCell(transform.position));
        SetPosition(pos1);
        Vector3 pos2 = mapController .CellToWorld (mapController .WorldToCell (transform .position )+GetDirectionVector ());
       
        Vector3 delta = (pos2 - pos1) / cellTime  * 0.05f;

        while (moving)
        {
            
            if(stunedTime > 0)
            {
                //不能动
            }
            else if (frozenTime > 0)
            {
                transform.position += delta * 0.4f;
            }
            else
            {
                transform.position += delta;
            }

            

            yield return new WaitForSeconds(0.05f);
            
            float dis = Vector2.Distance(transform.position, pos2);
            if (dis < delta.magnitude) {

                transform.position = pos2;
                break;
            }
        }

       
        moving = false;

        //animator.SetBool("_moving", false);

        yield return null;
    }


    public void SetPosition(Vector3 wPos)
    {
        transform.position = wPos;
    }

    public void TurnTo(int dir)
    {
        direction = dir;
        moving = false;
        anim.SetInteger(directionID , dir);
        
    }

    public int GetDirection()
    {
        return direction;
    }

    public Vector3Int GetDirectionVector()
    {
        switch(direction)
        {
            case 0:
                return new Vector3Int(0,1,0);
            case 1:
                return new Vector3Int(1,0,0);
            case 2:
                return new Vector3Int(0,-1,0);
            case 3:
                return new Vector3Int(-1,0,0);

        }

        return new Vector3Int();
    }


    public void HitBy(TurretAttack  bullet)
    {
        //AudioSource.PlayClipAtPoint(AudioManager.se_hit ,Camera .main.transform .position );
        /*
        float fDamage = bullet.damage;
        if (bullet.type == 11) fDamage *= 1 - magicalResist / 100f;
        if (bullet.type == 12) fDamage *= 1 - physicalResist / 100f;
        this.hp -= fDamage ;

        if (hp <= 0)
        {
            
            hp = 0;
        }

        if(bullet.effect != 0)
        {
            if(bullet.effect == 21)
            {
                Frozen(bullet.effectTime);
            }

            if(bullet.effect == 22)
            {
                Burn(bullet.effectTime);
            }

            if(bullet.effect == 23)
            {
                Stun(bullet.effectTime);
            }
        }
        */

        HitBy(bullet.damage, bullet.type, bullet.effect, bullet.effectTime);

        if(bullet.form == 1)
        {
            Destroy(bullet.gameObject);
        }
        
    }

    public void HitBy(float damage, int damageType, int effect, float effectTime)
    {

        float fDamage = damage;
        if (damageType == 11) fDamage *= 1 - magicalResist / 100f;
        if (damageType == 12) fDamage *= 1 - physicalResist / 100f;
        this.hp -= fDamage;

        if (hp <= 0)
        {

            hp = 0;
        }

        if (effect != 0)
        {
            if (effect == 21)
            {
                Frozen(effectTime);
            }

            if (effect == 22)
            {
                Burn(effectTime);
            }

            if (effect == 23)
            {
                Stun(effectTime);
            }

            if(effect == 25)
            {
                cursed = true;
            }
        }
        /*
        if (bullet.form == 1)
        {
            Destroy(bullet.gameObject);
        }*/

        StartCoroutine(Flash(0.05f));
    }

    public IEnumerator Flash(float time)
    {
        //Color oColor = GetComponent<SpriteRenderer>().color;
        flashing = true;
        yield return new WaitForSeconds(time);
        flashing = false;
        yield return null;
    }

    public void Frozen(float time)
    {
        if(!frozenResist)
        {
            burnedTime = 0;
            frozenTime = time;
        }
    }

    public void Burn(float time)
    {
        if(!burnedResist)
        {
            frozenTime = 0;
            burnedTime += time;//叠加最多20层。如果叠到了再被烧伤，触发一次伤害。
            if (burnedTime > 20) {

                burnedTime = 20;

                float damage = (1f - magicalResist / 100f) * 20 * 10;
                hp -= damage;
            }
            //burned += (int)time;
        }
        
    }

    public void Stun(float time)
    {
        if (!burnedResist && stunedTime <=0)
        {
            stunedTime = time;
        }
    }

    public void SetHP(float hp, float maxHP)
    {
        this.hp = hp;
        this.maxHp = maxHP;
    }

    public void AddHP(float h)
    {
        this.hp += h;
        if (hp > maxHp) hp = maxHp;
    }

    public bool HasDebuff(int i)
    {
        switch (i)
        {
            case 21:
                return frozenTime > 0;
            case 22:
                return burnedTime > 0;
            case 23:
                return stunedTime > 0;
            case 25:
                return cursed;
        }
        return false;
    }
}
