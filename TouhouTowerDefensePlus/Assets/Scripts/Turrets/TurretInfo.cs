using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurretInfo
{
    public const int TURRET_MARISA = 100, TURRET_CIRNO = 101, TURRET_TEI = 102, TURRET_MINORIKO = 103;
    public const int TURRET_YOUMU = 104, TURRET_MOKOU = 105, TURRET_UTSUHO = 106, TURRET_REMILIA = 107;
    public const int TURRET_YUUKA = 108, TURRET_YUYUKO = 109, TURRET_YUKARI = 110, TURRET_SAKUYA = 111;
    public const int TURRET_TENSHI = 112, TURRET_RAN = 113;

    public class TURRET {

        public int id;
        public string name;
        public string intro;
        public Sprite buttonImage;
        public int maxLevel;
        public int[] price;
        public float[] bulletDamage;
        public float[] bulletEffectTime;
        public float[] range;
        public float[] fireRate;

        public float[] explosionDamage;
        public float[] explosionEffectTime;
        public float[] explosionRadius;
        public bool explosionAOE;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="img"></param>
        /// <param name="maxLevel"></param>
        /// <param name="p">初始及升级价格</param>
        /// <param name="d">初始及升级后伤害</param>
        /// <param name="et">初始及升级后效果时间</param>
        /// <param name="r">初始及升级后攻击范围</param>
        /// <param name="fr">初始及升级后攻击间隔</param>
        public TURRET(int id, string name, string intro, Sprite img, int maxLevel, int[] p, float[] d, float[] et, float[] r, float[] fr){

            this.id = id;
            this.name = name;
            buttonImage = img;
            price = p;
            bulletDamage = d;
            bulletEffectTime = et;
            range = r;
            fireRate = fr;
            this.intro = intro;

            this.maxLevel = maxLevel;

            float resolutionRevise = 1920 / Screen.width;
            if (range != null)
            {
                for (int i = 0; i < range.Length; i++) range[i] *= resolutionRevise;
            }

        }

        public TURRET(int id, string name, string intro, Sprite img, int maxLevel, int[] p, float[] d, float[] et, float[] r, float[] fr, float[] ed, float[] ee, float[] er,bool aoe)
        {

            this.id = id;
            this.name = name;
            buttonImage = img;
            price = p;

            this.maxLevel = maxLevel;

            bulletDamage = d;
            bulletEffectTime = et;
            range = r;
            fireRate = fr;

            explosionDamage = ed;
            explosionEffectTime = ee;
            explosionRadius = er;
            explosionAOE = aoe;

            this.intro = intro;

            float resolutionRevise = 1920 / Screen.width;

            if (range != null)
            {
                for (int i = 0; i < range.Length; i++) range[i] *= resolutionRevise;
            }

            if (explosionRadius != null)
            {
                for (int i = 0; i < explosionRadius.Length; i++) explosionRadius[i] *= resolutionRevise;
            }
        }

    }

    public static TURRET MARISA = new TURRET(

        TURRET_MARISA,
        "魔理沙",
        "普通的魔法使，提供基础魔法输出。",
        Resources.Load<Sprite>("SelectButton/marisa"),
        3,
        new int[] { 600, 300, 300, 600 },
        new float[] {50,60,60,80},
        new float[] {0,0,0,0},
        new float[] {2,2,2,2.5f},
        new float[] {1,1,0.8f,0.8f}
        
        
    );

    public static TURRET CIRNO = new TURRET(

        TURRET_CIRNO,
        "琪露诺",
        "雾之湖上的笨蛋妖精。攻击可以使敌人冰冻，移速减少60%。",
        Resources.Load<Sprite>("SelectButton/cirno"),
        3,
        new int[] { 500, 250, 250, 750 },
        new float[] { 10, 10, 10, 10 },
        new float[] { 2, 2.5f, 3, 3.5f },
        new float[] { 1.5f, 1.5f, 1.5f, 1.5f },
        new float[] { 0.8f,0.8f, 0.6f, 0.5f }


    );

    public static TURRET TEI = new TURRET(

        TURRET_TEI,
        "因幡帝",
        "腹黑的幸运兔子。用很少的金币就可以让她上场，但她什么也不会做。",
        Resources.Load<Sprite>("SelectButton/tei"),
        0,
        new int[] { 50},
        null,
        null,
        new float[] {0f},
        null


    );

    public static TURRET MINORIKO = new TURRET(

        TURRET_MINORIKO,
        "秋穰子",
        "缺乏存在感的丰收之神。当攻击范围内有敌人被击败时收获金币！",
        Resources.Load<Sprite>("SelectButton/minoriko"),
        1,
        new int[] { 300,600},
        new float[] { 30, 60},
        new float[] { 0,0 },
        new float[] { 3.5f,4f },
        new float[] { 10,10}


    );

    public static TURRET RAN = new TURRET(

        TURRET_MINORIKO,
        "八云蓝",
        "八云紫的式神，九尾狐妖。拥有非凡的计算能力，能够提高周围队友的射速。（未完成）",
        Resources.Load<Sprite>("SelectButton/ran"),
        1,
        new int[] { 800, 1000 },
        new float[] { 1.2f, 1.4f },
        new float[] { 0, 0 },
        new float[] { 0, 0 },
        new float[] { 1, 2 }


    );

    public static TURRET SAKUYA = new TURRET(

        TURRET_SAKUYA,
        "咲夜",
        "完美潇洒的女仆。提供基础物理输出。",
        Resources.Load<Sprite>("SelectButton/sakuya"),
        3,
        new int[] { 600, 300, 300, 600 },
        new float[] { 50, 60, 60, 80 },
        new float[] { 0, 0, 0, 0 },
        new float[] { 2, 2, 2, 2.5f },
        new float[] { 1, 1, 0.8f, 0.8f }


    );

    public static TURRET YOUMU = new TURRET(

        TURRET_YOUMU,
        "妖梦",
        "白玉楼的园艺师。用剑术向接近的敌人造成高额物理伤害。",
        Resources.Load<Sprite>("SelectButton/youmu"),
        3,
        new int[] { 1200, 600, 1000, 1500 },
        null,
        null,
        new float[] { 1.2f, 1.2f, 1.5f, 1.5f },
        new float[] { 0.8f, 0.7f, 0.5f, 0.4f },

        new float[] { 80, 100, 100, 120 },
        new float[] { 0 , 0, 0,0 },
        new float[] { 0,0,0,0 },
        false

    );

    public static TURRET TENSHI = new TURRET(

        TURRET_TENSHI,
        "天子",
        "居于天界的不良天人，携带着要石，能够引发地震，对大范围的敌人造成物理伤害和短暂眩晕。",
        Resources.Load<Sprite>("SelectButton/tenshi"),
        3,
        new int[] { 1800, 800, 1000, 1500 },
        null,
        null,
        new float[] { 2f, 2f, 2.5f, 3f },
        new float[] { 5f, 4.5f, 4.5f, 4f },

        new float[] { 80, 100, 120, 150 },
        new float[] { 0.2f, 0.2f, 0.25f, 0.3f },
        new float[] { 2f, 2f, 2.5f, 3f },
        true

    );

    public static TURRET MOKOU = new TURRET(

        TURRET_MOKOU,
        "妹红",
        "不老不死的火鸟。快速点燃接近的敌人。",
        Resources.Load<Sprite>("SelectButton/mokou"),
        3,
        new int[] { 1200, 600, 800, 1200},
        new float[] { 0,0,0,0},
        new float[] { 0,0,0 ,0},
        new float[] { 1.5f, 1.5f, 2f,2f},
        new float[] { 0.8f, 0.7f, 0.7f,0.6f},

        new float[] { 20,20,40,40 },
        new float[] { 4,5, 6,7 },
        new float[] { 0,0,0,0 },
        false

    );

    public static TURRET UTSUHO = new TURRET(

        TURRET_UTSUHO,
        "空",
        "掌握核聚变之力的地狱鸦。发射爆炸性子弹，对大范围的敌人造成毁灭性打击。",
        Resources.Load<Sprite>("SelectButton/utsuho"),
        3,
        new int[] { 1800, 800, 1000, 2000 },
        new float[] { 0,0,0,0 },
        new float[] { 0,0,0,0 },
        new float[] { 3.5f, 3.5f, 3.5f, 4f },
        new float[] { 3.5f, 3.5f, 3f, 3f },

        new float[] { 80,100,100,120},
        new float[] { 0,0,0,0},          
        new float[] { 0.5f,0.5f,0.5f,1f},
        true

    );

    public static TURRET YUUKA = new TURRET(

        TURRET_YUUKA,
        "幽香",
        "四季的鲜花之主。发射强力的直线激光。",
        Resources.Load<Sprite>("SelectButton/yuuka"),
        3,
        new int[] { 1200, 800,1500, 2000 },
        new float[] { 100, 120, 150, 180 },
        new float[] { 0, 0, 0, 0 },
        new float[] { 4f, 4f, 5f, 5f },
        new float[] { 3.5f,3.5f,3.2f,2.9f }


    );

    public static TURRET REMILIA = new TURRET(

        TURRET_REMILIA,
        "蕾米莉亚",
        "永远鲜红的幼月。投出冈格尼尔之枪，造成高额物理伤害并击晕敌人。",
        Resources.Load<Sprite>("SelectButton/remilia"),
        3,
        new int[] { 1200, 800,1000,2000 },
        new float[] { 100, 120, 120, 150 },
        new float[] { 0.3f, 0.4f, 0.4f, 0.6f },
        new float[] { 3f,3f,3.5f,3.5f },
        new float[] { 2f, 2f, 1.5f, 1.2f }
        
    );

    public static TURRET YUYUKO = new TURRET(

        TURRET_YUYUKO,
        "幽幽子（未完成）",
        "操纵生死的亡灵公主。诅咒敌人，降低其生命上限，并使其无法被治疗和复活。",
        Resources.Load<Sprite>("SelectButton/yuyuko"),
        3,
        new int[] { 1200,1500,1000,1000 },
        new float[] { 100, 150, 150, 150 },
        new float[] { 0, 0, 0, 0 },
        new float[] { 2f, 2f, 2.5f, 3f },
        new float[] { 1.5f, 1.3f, 1f, 1f }


    );

    public static TURRET YUKARI = new TURRET(

        TURRET_YUYUKO,
        "八云紫（未完成）",
        "境界的妖怪贤者。还没想好有什么用。",
        Resources.Load<Sprite>("SelectButton/yukari"),
        3,
        new int[] { 1500 },
        new float[] { 100, 150, 150, 150 },
        new float[] { 0, 0, 0, 0 },
        new float[] { 3f, 3f, 4f, 4f },
        new float[] { 3f, 3f, 3f, 2.5f }


    );



    public static Sprite GetSelectButtonImage(int i)
    {
        TURRET t = GetTurretInfo(i);
        if (t == null) return null;
        return t.buttonImage;
    }

    public static TURRET GetTurretInfo(int i)
    {
        TURRET s = null;
        switch (i)
        {
            case TURRET_MARISA:
                s = MARISA;
                break;
            case TURRET_CIRNO:
                s = CIRNO;
                break;
            case TURRET_TEI:
                s = TEI;
                break;
            case TURRET_MINORIKO:
                s = MINORIKO ;
                break;
            case TURRET_YOUMU:
                s = YOUMU;
                break;
            case TURRET_MOKOU:
                s = MOKOU;
                break;
            case TURRET_UTSUHO:
                s = UTSUHO ;
                break;
            case TURRET_REMILIA:
                s = REMILIA;
                break;
            case TURRET_YUUKA:
                s = YUUKA;
                break;
            case TURRET_YUYUKO:
                s = YUYUKO;
                break;
            case TURRET_YUKARI:
                s = YUKARI;
                break;
            case TURRET_SAKUYA:
                s = SAKUYA;
                break;
            case TURRET_TENSHI:
                s = TENSHI;
                break;
            case TURRET_RAN:
                s = RAN;
                break;
        }
        return s;
    }




   

}
