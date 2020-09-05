using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInfo
{
    public const int ENEMY_MAOYU = 500, ENEMY_RUMIA = 501, ENEMY_CIRNO = 502, ENEMY_MEILIN = 503;
    public const int ENEMY_PATCHOULI = 504, ENEMY_SAKUYA = 505, ENEMY_REMILIA = 506, ENEMY_FLANDRE = 507;
    public const int ENEMY_ALICE = 508, ENEMY_SHANGHAI = 509, ENEMY_KAGUYA = 510, ENEMY_CHEN = 511;
    public const int ENEMY_SANAE = 512, ENEMY_AYA = 513, ENEMY_RIN = 514, ENEMY_YUUKA = 515;
    public const int ENEMY_YUUGI = 516, ENEMY_SHIKIEKI = 517, ENEMY_YUKARI = 518, ENEMY_EIRIN = 519, ENEMY_SUIKA = 520;
    public const int ENEMY_SUIKA_SMALL = 521, ENEMY_KANAKO = 522, ENEMY_HINA = 523, ENEMY_SUWAKO = 524;
    public const int ENEMY_YUYUKO = 525, ENEMY_YOUMU = 526;


    //public const int ENEMY_MAOYU = 500, ENEMY_RUMIA = 501, ENEMY_CIRNO = 502, ENEMY_MEILIN = 503;

    public static ENEMY MAOYU = new ENEMY
    (
        ENEMY_MAOYU,
        "毛玉",
        "看上去像是无害的毛球，实际上全身布满了锋利的针刺。喜欢群居，攻击性强，但单个出现时不堪一击。",
        150,
        0.6f,
        5
    );

    public static ENEMY CIRNO = new ENEMY
    (
        ENEMY_CIRNO,
        "琪露诺",
        "湖上的笨蛋冰精，自以为是幻想乡最强。",
        200,
        0.6f,
        10
    );

    public static ENEMY RUMIA = new ENEMY
    (
        ENEMY_RUMIA,
        "露米娅",
        "宵暗的妖怪，实力弱小。传说如果解开头上的蝴蝶结，露米娅就会进入强大的解放形态。",
        250,
        0.75f,
        10
    );

    public static ENEMY CHEN = new ENEMY
    (
        ENEMY_CHEN,
        "橙",
        "新生的猫妖，是八云蓝的式神。由于实力尚浅，还没有被赋予八云的姓氏。",
        150,
        0.45f,
        10
    );

    public static ENEMY MEILIN = new ENEMY
    (
        ENEMY_MEILIN,
        "红美铃",
        "红魔馆的门番，经常被人看见在工作室偷懒。擅长中华武术，对物理攻击有一定抗性。",
        750,
        0.75f,
        25,
        50,
        0,
        false,false,false,false
    );

    public static ENEMY SAKUYA = new ENEMY
    (
        ENEMY_SAKUYA,
        "咲夜",
        "完美潇洒的女仆，能力是操纵时间，传说是Dio的女儿。对魔法攻击有一定抗性。",
        600,
        0.6f,
        25,
        0,
        50,
        false, false, false, false
    );

    public static ENEMY AYA = new ENEMY
    (
        ENEMY_AYA,
        "文",
        "喜欢搞大新闻的天狗记者，飞行速度是幻想乡最快。",
        600,
        0.45f,
        30
    );

    public static ENEMY PATCHOULI = new ENEMY
    (
        ENEMY_PATCHOULI,
        "帕秋莉",
        "不动的大图书馆。精通七曜魔法，具有极高的魔法抗性，但对物理伤害较弱。",
        750,
        0.75f,
        25,
        -25,
        90,
        false, false, false, false
    );

    public static ENEMY REMILIA = new ENEMY
    (
        ENEMY_REMILIA,
        "蕾米莉亚",
        "永远鲜红的幼月，是一名强大的吸血鬼。具有物理抗性并且不会眩晕。",
        1400,
        0.6f,
        50,
        50,
        0,
        false, false, true, true
    );

    public static ENEMY FLANDRE = new ENEMY
    (
        ENEMY_FLANDRE,
        "芙兰朵露",
        "蕾米莉亚的妹妹，还不能控制自己那破坏性的力量。具有物理抗性并且不会眩晕。",
        1050,
        0.45f,
        50,
        50,
        0,
        false, false, true, true
    );

    public static ENEMY SANAE = new ENEMY
    (
        ENEMY_SANAE,
        "早苗",
        "守矢神社的现人神，据说幻想入之前是一名JK。每隔一段时间能治愈周围的所有敌人。",
        600,
        0.6f,
        30

    );

    public static ENEMY ALICE = new ENEMY
    (
        ENEMY_ALICE,
        "爱丽丝",
        "技艺精湛的人偶师。每隔一段时间会放出一只上海人形加入战斗。",
        750,
        0.75f,
        30
    );

    public static ENEMY SHANGHAI = new ENEMY
    (
        ENEMY_SHANGHAI,
        "上海人形",
        "爱丽丝制作的玩偶，具有一定独立行动能力。速度快但十分脆弱。",
        150,
        0.45f,
        0
    );

    public static ENEMY YUUGI = new ENEMY
    (
        ENEMY_YUUGI,
        "勇仪",
        "鬼族四天王之一的力之勇仪。对她使用物理攻击占不到任何便宜。",
        1750,
        0.75f,
        50,
        90, 0,
        false, false, false, false
    );

    public static ENEMY KAGUYA = new ENEMY
    (
        ENEMY_KAGUYA,
        "辉夜",
        "月球的公主，被放逐至此地。服下了蓬莱之药，可以在被击倒后复活。",
        750,
        0.75f,
        30
    );

    public static ENEMY RIN = new ENEMY
    (
        ENEMY_RIN,
        "火焰猫燐",
        "居住在地灵殿，以搬运尸体为乐。每过一段时间，为周围的敌人添加复活标记。拥有该标记的敌人在死后复活，生命上限变为原来的一半。",
        600,
        0.6f,
        30
    );

    public static ENEMY KANAKO = new ENEMY
    (
        ENEMY_KANAKO ,
        "神奈子",
        "守矢神社供奉的神之一，会为周围其他敌人提供守护。拥有魔法抗性。",
        1400,
        0.6f,
        50,
        0,50,
        false,false,false,false

    );

    public static ENEMY SUWAKO = new ENEMY
    (
        ENEMY_SUWAKO,
        "诹访子",
        "守矢神社供奉的神之一，会吸收周围其他敌人的生命。拥有魔法抗性",
        1400,
        0.6f,
        50,
        0, 50,
        false, false, false, false
    );

    public static ENEMY YUYUKO = new ENEMY
    (
        ENEMY_YUYUKO,
        "幽幽子",
        "操纵死亡的幽灵公主。不会被冰冻、烧伤或眩晕。",
        1750,
        0.75f,
        50,
        0,0,
        true,true,true,true
    );

    public static ENEMY YUUKA = new ENEMY
    (
        ENEMY_YUUKA,
        "幽香",
        "四季的鲜花之主。作为幻想乡有名的大妖怪，她拥有最纯粹的力量。（Boss角色）",
        12000,
        0.6f,
        300
    );

    public static ENEMY YOUMU = new ENEMY
    (
        ENEMY_YOUMU,
        "妖梦",
        "白玉楼的园艺师。性格率直，容易上当受骗。移动速度很快，并且不会被眩晕和冰冻。",
        450,
        0.45f,
        30,
        0,0,
        false,true,false,true
    );

    public static ENEMY SUIKA = new ENEMY
    (
        ENEMY_SUIKA,
        "萃香",
        "鬼族四天王之一，喜欢喝酒与开宴会。拥有操纵疏和密的能力，在被击倒后会放出若干个分身。（Boss角色）",
        10000,
        0.6f,
        300,
        90, 0,
        false,false,false,false
    );

    public static ENEMY SUIKA_SMALL = new ENEMY
    (
        ENEMY_SUIKA_SMALL,
        "萃香-小",
        "萃香利用操纵疏密的能力制造出的分身，相比本体十分脆弱，但移动非常快",
        500,
        0.45f,
        10
    );

    public static ENEMY YUKARI = new ENEMY
    (
        ENEMY_YUKARI,
        "八云紫",
        "幻想乡幕后黑手，大结界的创立者，大妖怪八云紫。能够在隙间中自由移动，瞬移到地图中其他位置。（Boss角色）",
        15000,
        0.9f,
        300,
        25,25,
        false,false,false,false

    );

    public static ENEMY SHIKIEKI = new ENEMY
    (
        ENEMY_SHIKIEKI,
        "四季映姬",
        "作为阎魔，担当幻想乡死者的裁决。非常喜欢说教，一说起来就是几个小时。（Boss角色）",
        10000,
        0.45f,
        300
    );

    public static ENEMY EIRIN = new ENEMY
    (
        ENEMY_EIRIN,
        "永琳",
        "永远亭的贤者，月之头脑八意永琳。似乎对一切威胁都游刃有余，免疫眩晕、烧伤和冰冻。",
        12000,
        0.6f,
        300,
        0, 0,
        true, true, true, true

    );

    public class ENEMY
    {
        public int id;
        public string name;
        public string intro;
        public Sprite image;
        public int reward;


        public float hp;
        public float cellTime;
        public float physicalResist, magicalResist;
        public bool burnResist, frozenResist, poisonResist, stuntResist;

        public ENEMY(int id, string name, string intro, float hp, float cellTime, int reward, float pr, float mr, bool burn, bool frozen, bool poison, bool stunt)
        {
            this.id = id;
            this.name = name;
            this.intro = intro;
            this.hp = hp;
            this.cellTime = cellTime;
            this.reward = reward;
            this.physicalResist = pr;
            this.magicalResist = mr;
            this.burnResist = burn;
            this.frozenResist = frozen;
            this.poisonResist = poison;
            this.stuntResist = stunt;

            this.image = LoadSprite(name);
        }


        public ENEMY(int id, string name, string intro, float hp, float cellTime, int reward)
        {
            this.id = id;
            this.name = name;
            this.intro = intro;
            this.hp = hp;
            this.cellTime = cellTime;
            this.reward = reward;
            this.physicalResist = 0;
            this.magicalResist = 0;
            this.burnResist = false ;
            this.frozenResist = false;
            this.poisonResist = false;
            this.stuntResist = false;

            this.image = LoadSprite(name);
        }


    }

    public static Sprite LoadSprite(string name)
    {

        Sprite s = Resources.Load<Sprite>("Role/4directions/" + name);

        return s;
    }

    public static ENEMY GetEnemyInfo(int id)
    {
        ENEMY e = null;

        switch (id)
        {
            case ENEMY_CIRNO:
                e = CIRNO;
                break;
            case ENEMY_RUMIA:
                e = RUMIA;
                break;
            case ENEMY_CHEN:
                e = CHEN;
                break;
            case ENEMY_MAOYU:
                e = MAOYU;
                break;
            case ENEMY_SAKUYA:
                e = SAKUYA;
                break;
            case ENEMY_MEILIN:
                e = MEILIN;
                break;
            case ENEMY_REMILIA:
                e = REMILIA;
                break;
            case ENEMY_FLANDRE:
                e = FLANDRE;
                break;
            case ENEMY_ALICE:
                e = ALICE;
                break;
            case ENEMY_SHANGHAI:
                e = SHANGHAI;
                break;
            case ENEMY_KAGUYA:
                e = KAGUYA;
                break;
            case ENEMY_RIN:
                e = RIN;
                break;
            case ENEMY_PATCHOULI:
                e = PATCHOULI ;
                break;
            case ENEMY_SANAE:
                e = SANAE ;
                break;
            case ENEMY_AYA:
                e = AYA;
                break;
            case ENEMY_YUUGI:
                e = YUUGI;
                break;
            case ENEMY_YUUKA:
                e = YUUKA;
                break;
            case ENEMY_SUIKA:
                e = SUIKA;
                break;
            case ENEMY_SUIKA_SMALL:
                e = SUIKA_SMALL;
                break;
            case ENEMY_YUYUKO:
                e = YUYUKO;
                break;
            case ENEMY_YOUMU:
                e = YOUMU;
                break;
            case ENEMY_YUKARI:
                e = YUKARI;
                break;
            case ENEMY_SHIKIEKI:
                e = SHIKIEKI ;
                break;
            case ENEMY_EIRIN:
                e = EIRIN;
                break;
            case ENEMY_KANAKO:
                e = KANAKO;
                break;
            case ENEMY_SUWAKO:
                e = SUWAKO;
                break;
                /*case ENEMY_CIRNO:
                    e = CIRNO;
                    break;
                case ENEMY_CIRNO:
                    e = CIRNO;
                    break;*/

        }

        return e;
    }
    

}
