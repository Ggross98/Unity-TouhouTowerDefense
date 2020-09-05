using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils
{
    //ground: 能够行走和摆放
    //road:   只能行走
    //platform:   只能摆放
    public const int GROUND = 0, ROAD = 1, PLATFORM = 2, BARRIER = -1, TURRET = -2;

    public const int UP = 0, RIGHT = 1, DOWN = 2, LEFT = 3;


    public const int LEVEL_MAX = 0;


    public static Utils instance;

    public Utils()
    {
        instance = this;
    }


    public static float LengthLocalToUI(float l)
    {
        float scale = 1920 / Screen.width;

        return l * scale;


    }

    public static Vector2 GetUIPosition(Vector3 wPos)
    {

        Vector3 screenPoint = RectTransformUtility.WorldToScreenPoint(Camera.main, wPos);

        float scale = 1920f / Screen.width;

        Vector2 point = new Vector2(scale * screenPoint.x - 1920 / 2, scale * screenPoint.y - 1080 / 2);

        return point;
    }


    public static float GetAngle360(Vector3 from_, Vector3 to_)
    {
        //两点的x、y值
        float x = -from_.x + to_.x;
        float y = -from_.y + to_.y;

        //斜边长度
        float hypotenuse = Mathf.Sqrt(Mathf.Pow(x, 2f) + Mathf.Pow(y, 2f));

        //求出弧度
        float cos = x / hypotenuse;
        float radian = Mathf.Acos(cos);

        //用弧度算出角度    
        float angle = 180 / (Mathf.PI / radian);

        
        if (y < 0)
        {
            angle = 360-angle;
        }
        else if ((y == 0) && (x < 0))
        {
            angle = 180;
        }


        return angle;
    }

    public static string GetMapName(int i)
    {
        string name = "name";
        switch (i)
        {
            case 0:
                name = "人间之里";
                break;
            case 1:
                name = "雾之湖";
                break;
            case 2:
                name = "旧地狱";
                break;
            case 3:
                name = "无缘冢";
                break;
        }
        return name;
    }

    public static string GetMapInfo(int i)
    {
        string name = "info";
        switch (i)
        {
            case 0:
                name = "人类居住的村庄。地形主要是平原，没有什么大妖怪出没。适合新手游玩。";
                break;
            case 1:
                name = "总是雾气缭绕的湖泊，琪露诺就居住在这里。湖面不能行走，注意利用木桥。";
                break;
            case 2:
                name = "废弃的地狱，现在是鬼的乐园。这里有大大小小的岩浆池，小心烫伤！";
                break;
            case 3:
                name = "一望无际的沙漠，似乎大结界在这里很稀薄。对人类和妖怪都非常危险。";
                break;
        }
        return name;
    }

    /// <summary>
    /// 设置UI物体的坐标为某个世界坐标
    /// </summary>
    /// <param name="rect">UI物体的RectTransform</param>
    /// <param name="wPos">世界坐标</param>
    public static void SetUIPosition(RectTransform rect, Vector3 wPos)
    {
        //Debug.Log(Screen.width + "," + Screen.height);
        //updateUI.transform.position = wPos;

        Vector3 screenPoint = RectTransformUtility.WorldToScreenPoint(Camera.main, wPos);
        //updateUI.GetComponent<RectTransform>().anchoredPosition = screenPoint;
        //Debug.Log(screenPoint);
        float scale = 1920f / Screen.width;
        Vector2 point = new Vector2(scale * screenPoint.x - 1920 / 2, scale * screenPoint.y - 1080 / 2);
        rect.anchoredPosition = point;
        
    }

    public const int ASSIST_REIMU = 50, ASSIST_AKYUU = 51, ASSIST_RINNOSUKE = 52, ASSIST_HINA = 53, ASSIST_LETTY = 54, ASSIST_BYAKUREN = 55;

    public class AssistRole
    {
        public int id;
        public string name, intro;
        public Sprite image;
        public AssistRole(int i, string n, string intro, Sprite s)
        {
            this.id = i;
            this.name = n;
            this.intro = intro;
            this.image = s;


        }
    }

    public static AssistRole REIMU = new AssistRole(
        
        ASSIST_REIMU ,
        "博丽灵梦",
        "乐园的美妙巫女。虽然灵梦懒散而贪财，但她实力强大，退治妖怪毫不留情.。作为支援角色时，增加提前释放波次的奖励金币100%。",
        Resources.Load<Sprite>("Head/reimu")
        
        
        );
    public static AssistRole AKYUU = new AssistRole(

        ASSIST_AKYUU,
        "稗田阿求",
        "第九代御阿礼之子，拥有过目不忘程度的能力，记录着幻想乡的历史。作为支援角色时，阿求会静静的看着你指挥战斗，不提供额外帮助。",
        Resources.Load<Sprite>("Head/akyuu")


        );

    public static AssistRole RINNOSUKE = new AssistRole(

        ASSIST_RINNOSUKE,
        "森近霖之助",
        "香霖堂道具店的店主，是人类与妖怪的混血。霖之助是精明的商人，他作为支援角色时，你出售防御塔会获得75%的退款。",
        Resources.Load<Sprite>("Head/rinnosuke")


        );

    public static AssistRole HINA = new AssistRole(

        ASSIST_HINA,
        "键山雏",
        "吸收厄运的人偶，同时是被原作者遗忘的可怜角色。作为支援角色时，处于诅咒状态的敌人受到全部伤害加倍。",
        Resources.Load<Sprite>("Head/hina")


        );

    public static AssistRole LETTY = new AssistRole(

        ASSIST_LETTY,
        "蕾蒂",
        "雪女的一种，只能在冬天遇见。她操纵着寒冷的力量，作为支援角色时，处于冰冻状态的敌人被击杀时，对周围敌人造成范围伤害。",
        Resources.Load<Sprite>("Head/letty")


        );

    public static AssistRole BYAKUREN= new AssistRole(

        ASSIST_BYAKUREN,
        "圣白莲",
        "被封印的大魔法师。性格稳重大方，非常的可靠，同时拥有着可怕的力量。每当生命值下降，她会帮你瞬间消灭一部分离基地太近的敌人。",
        Resources.Load<Sprite>("Head/byakuren")


        );

    public static AssistRole  GetAssistRoleInfo(int assist)
    {
        AssistRole ar = null;
        switch (assist)
        {
            case ASSIST_REIMU:
                ar = REIMU;
                break;
            case ASSIST_AKYUU:
                ar = AKYUU;
                break;
            case ASSIST_RINNOSUKE:
                ar = RINNOSUKE;
                break;
            case ASSIST_LETTY:
                ar = LETTY;
                break;
            case ASSIST_BYAKUREN:
                ar = BYAKUREN;
                break;
        }

        return ar;
    }

}
