using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{

    private MapController mapController;

    public Vector3Int enemyStart, home;
    public Vector3 enemyStart_wPos, home_wPos;

    public GameObject mark_enemystart, mark_home;

    public List<EnemyScript> enemyList;

    private GameObject maoyu, cirno, rumia, meilin, patchouli, sakuya, remilia, flandre;
    private GameObject sanae, alice, shanghai, rin, chen, kaguya, aya, yuugi, kanako, suwako;
    private GameObject shikieki, yukari, yuyuko, youmu, suika, suika_small, yuuka, eirin;

    private Color enemyGoldColor;

    private int wave = 0, rank = 0;

    private TurretController turretController;

    // Start is called before the first frame update
    void Awake()
    {
        mark_enemystart = GameObject.Find("mark_enemystart");
        mark_home = GameObject.Find("mark_home");

        turretController = GameObject.Find("TurretSelectUI").GetComponent<TurretController>();

        mapController = gameObject.GetComponent<MapController>();
        enemyStart = mapController.WorldToCell(mark_enemystart.transform.position);
        home = mapController.WorldToCell(mark_home.transform.position);
        enemyStart_wPos = mark_enemystart.transform.position;
        home_wPos = mark_home.transform.position;

        enemyList = new List<EnemyScript>();

        if (PlayerInfo.instance.level == 2) enemyGoldColor = Color.white;
        else enemyGoldColor = Color.black;

        maoyu = Resources.Load<GameObject >("Prefabs/enemies/enemy_maoyu");
        cirno = Resources.Load<GameObject>("Prefabs/enemies/enemy_cirno");
        rumia = Resources.Load<GameObject>("Prefabs/enemies/enemy_rumia");
        meilin = Resources.Load<GameObject>("Prefabs/enemies/enemy_meilin");
        sakuya = Resources.Load<GameObject>("Prefabs/enemies/enemy_sakuya");
        patchouli = Resources.Load<GameObject>("Prefabs/enemies/enemy_patchouli");
        remilia = Resources.Load<GameObject>("Prefabs/enemies/enemy_remilia");
        flandre = Resources.Load<GameObject>("Prefabs/enemies/enemy_flandre");
        alice = Resources.Load<GameObject>("Prefabs/enemies/enemy_alice");
        shanghai = Resources.Load<GameObject>("Prefabs/enemies/enemy_shanghai");
        chen = Resources.Load<GameObject>("Prefabs/enemies/enemy_chen");
        rin = Resources.Load<GameObject>("Prefabs/enemies/enemy_rin");
        sanae = Resources.Load<GameObject>("Prefabs/enemies/enemy_sanae");
        aya = Resources.Load<GameObject>("Prefabs/enemies/enemy_aya");
        kaguya = Resources.Load<GameObject>("Prefabs/enemies/enemy_kaguya");
        yuugi = Resources.Load<GameObject>("Prefabs/enemies/enemy_yuugi");

        kanako = Resources.Load<GameObject>("Prefabs/enemies/enemy_kanako");
        suwako = Resources.Load<GameObject>("Prefabs/enemies/enemy_suwako");
        yuuka = Resources.Load<GameObject>("Prefabs/enemies/enemy_yuuka");
        suika = Resources.Load<GameObject>("Prefabs/enemies/enemy_suika");
        suika_small  = Resources.Load<GameObject>("Prefabs/enemies/enemy_suika_small");
        youmu = Resources.Load<GameObject>("Prefabs/enemies/enemy_youmu");
        yuyuko = Resources.Load<GameObject>("Prefabs/enemies/enemy_yuyuko");

        eirin = Resources.Load<GameObject>("Prefabs/enemies/enemy_eirin");
        yukari = Resources.Load<GameObject>("Prefabs/enemies/enemy_yukari");
        shikieki = Resources.Load<GameObject>("Prefabs/enemies/enemy_shikieki");
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if(Input.GetMouseButtonUp(0))
        {
            CreateEnemy();
        }*/

        for(int i =0;i<enemyList.Count; i++)
        {
            if (enemyList[i].hp <= 0)
                DeleteEnemy(enemyList[i]);
        }
    }

    public void DeleteEnemy(EnemyScript es)
    {
        //金币奖励
        mapController.gameObject.GetComponent<StageController>().gold += es.reward;
        TextEffectManager.instance.ShowTextAtPosition("+" + es.reward, enemyGoldColor  , 25, es.transform.position+new Vector3 (0,0.5f), 0.3f);

        int id = es.ID;
        Vector3 pos = es.transform.position;
        //有复活标记并且是被打死的才能复活
        bool rs = es.resurrect && es.hp <=0 && !es.cursed ;

        //秋穰子收获

        for(int i =0;i<turretController.list_minoriko.Count; i++)
        {
            MinorikoScript ms = turretController.list_minoriko[i];
            if(ms.InFireRange (es.transform))
            {
                float rate;
                if (ms.level == 0) rate = 0.6f;
                else rate = 1f;
                ms.Gain(rate*(float)es.reward );
            }
        }

        //结算蕾蒂的冰爆效果
        if(PlayerInfo .instance .assist == Utils.ASSIST_LETTY)
        {
            if(es.HasDebuff (21))
            {
                foreach (EnemyScript enemy in enemyList)
                {
                    if(enemy!=es && Vector3.Distance(es.transform.position, enemy.transform.position) <= 2)
                    {
                        enemy.HitBy(es.maxHp / 10, 0,21,1);
                    }
                }
            }
        }
        
        enemyList.Remove(es);
        Destroy(es.gameObject);

        //如果能复活
        if (rs)
        {
            
            EnemyScript newes = CreateEnemy(id, pos).GetComponent <EnemyScript >();
            newes.resurrect = false;
            newes.SetHP(es.maxHp *es.resurrectRatio , es.maxHp * es.resurrectRatio);

            ParticleEffectManager.ShowResurrectEffect(pos + new Vector3(0, -0.5f), newes.transform);
        }

        

    }

    public void EnemyAtHome(EnemyScript es)
    {
        mapController.gameObject.GetComponent<StageController>().EnemyAtHome ();

        if (PlayerInfo.instance.assist == Utils.ASSIST_BYAKUREN)
        {
            foreach(EnemyScript enemy in enemyList)
            {
                if(enemy!= es && Vector3 .Distance (es.transform.position ,enemy.transform .position) <= 3)
                {
                    DeleteEnemy(enemy);
                }
            }
        }

        DeleteEnemy(es);

    }

    public int EnemyCount()
    {
        return enemyList.Count;
    }

    public void SetWaveAndRank(int w, int r)
    {
        this.wave = w;
        this.rank = r;
    }
    /*
    public GameObject CreateEnemy()
    {
        GameObject newEnemy = Instantiate(cirno);
        //newEnemy.GetComponent<EnemyScript>().SetPosition(enemyStart_wPos);
        EnemyScript enemyScript = newEnemy.GetComponent<EnemyScript>();
        enemyScript.mapController = this.mapController;
        enemyScript.SetPosition(enemyStart_wPos);
        enemyScript.cellTime = 0.5f;

        enemyList.Add(enemyScript);

        //Debug.Log("enemy's position is in Cell "+mapController.WorldToCell(newEnemy.transform.position));

        return newEnemy ;
    }*/

    public GameObject CreateEnemy(int enemyID, Vector3 pos)
    {
        GameObject newEnemy = null;
        switch(enemyID)
        {
            case EnemyInfo.ENEMY_CIRNO:
                newEnemy = Instantiate(cirno);
                break;
            case EnemyInfo .ENEMY_RUMIA :
                newEnemy = Instantiate(rumia);
                break;
            case EnemyInfo.ENEMY_MAOYU:
                newEnemy = Instantiate(maoyu);
                break;
            case EnemyInfo.ENEMY_MEILIN:
                newEnemy = Instantiate(meilin);
                break;
            case EnemyInfo.ENEMY_SAKUYA:
                newEnemy = Instantiate(sakuya);
                break;
            case EnemyInfo.ENEMY_PATCHOULI:
                newEnemy = Instantiate(patchouli);
                break;
            case EnemyInfo.ENEMY_REMILIA:
                newEnemy = Instantiate(remilia);
                break;
            case EnemyInfo.ENEMY_FLANDRE:
                newEnemy = Instantiate(flandre);
                break;
            case EnemyInfo.ENEMY_ALICE:
                newEnemy = Instantiate(alice);
                break;
            case EnemyInfo.ENEMY_SHANGHAI:
                newEnemy = Instantiate(shanghai);
                break;
            case EnemyInfo.ENEMY_SANAE:
                newEnemy = Instantiate(sanae);
                break;
            case EnemyInfo.ENEMY_AYA:
                newEnemy = Instantiate(aya);
                break;
            case EnemyInfo.ENEMY_RIN:
                newEnemy = Instantiate(rin);
                break;
            case EnemyInfo.ENEMY_CHEN:
                newEnemy = Instantiate(chen);
                break;
            case EnemyInfo.ENEMY_KAGUYA:
                newEnemy = Instantiate(kaguya);
                break;
            case EnemyInfo.ENEMY_YUUGI:
                newEnemy = Instantiate(yuugi);
                break;

            case EnemyInfo.ENEMY_SUIKA:
                newEnemy = Instantiate(suika);
                break;
            case EnemyInfo.ENEMY_SUIKA_SMALL:
                newEnemy = Instantiate(suika_small );
                break;
            case EnemyInfo.ENEMY_SUWAKO:
                newEnemy = Instantiate(suwako);
                break;
            case EnemyInfo.ENEMY_KANAKO:
                newEnemy = Instantiate(kanako);
                break;
            case EnemyInfo.ENEMY_EIRIN:
                newEnemy = Instantiate(eirin);
                break;
            case EnemyInfo.ENEMY_YUKARI:
                newEnemy = Instantiate(yukari);
                break;
            case EnemyInfo.ENEMY_YUUKA:
                newEnemy = Instantiate(yuuka);
                break;
            case EnemyInfo.ENEMY_YOUMU:
                newEnemy = Instantiate(youmu);
                break;
            case EnemyInfo.ENEMY_YUYUKO:
                newEnemy = Instantiate(yuyuko);
                break;
            case EnemyInfo.ENEMY_SHIKIEKI:
                newEnemy = Instantiate(shikieki );
                break;
        }

        EnemyScript enemyScript = newEnemy.GetComponent<EnemyScript>();
        enemyScript.mapController = this.mapController;
        enemyScript.ID = enemyID;
        //enemyScript.SetPosition(enemyStart_wPos);
        enemyScript.SetPosition(pos);

        //加载基础属性
        EnemyInfo.ENEMY info = EnemyInfo.GetEnemyInfo(enemyID );
        enemyScript.SetHP(info.hp, info.hp);
        enemyScript.reward = info.reward;
        enemyScript.cellTime = info.cellTime;
        enemyScript.physicalResist = info.physicalResist;
        enemyScript.magicalResist = info.magicalResist;
        enemyScript.stunedResist = info.stuntResist;
        enemyScript.burnedResist = info.burnResist;
        enemyScript.frozenResist = info.frozenResist;
        enemyScript.poisonedResist = info.poisonResist;


        //根据关卡提高怪物属性
        float x;
        if (wave < 20) x = 1;
        else x = 1+ (float)((wave - 20)) * 0.1f;
        //x = 4f;
        if (x > 10) x = 10;
        //根据rank提高属性
        if (rank > 0)
        {
            x += 0.2f * rank;

            float y = Mathf.Pow(0.9f, rank);
            enemyScript.cellTime *= y;
        }


        float newhp = enemyScript.maxHp * x;
        if (wave > 10) newhp += (wave-10) * 10;
        enemyScript.SetHP(newhp, newhp );

        //enemyScript.cellTime = 0.5f;


        enemyList.Add(enemyScript);

        return newEnemy;
    }

    public GameObject CreateEnemy(int enemyID)
    {
        return CreateEnemy(enemyID, enemyStart_wPos);
    }
}
