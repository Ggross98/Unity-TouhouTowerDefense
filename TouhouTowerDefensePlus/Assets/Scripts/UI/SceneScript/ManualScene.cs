using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class ManualScene : MonoBehaviour
{
    //两个选择界面：防御塔，敌人
    public GameObject turret, enemy;
    public SelectButton title_turret, title_enemy;

    //0为防御塔，1为敌人
    public int currentManual = 0;

    //左边的按钮
    List<TurretSelectButton> buttonList_turret;
    List<EnemySelectButton> buttonList_enemy;

    public GameObject buttonPrefab_turret, buttonPrefab_enemy;
    public Transform layout_enemy, layout_turret;
    //选中了第几个
    public int selected_enemy = 0, selected_turret = 0;

    //右边显示信息
    public Image show_image;
    public Text show_name, show_intro, show_msg1, show_msg2;

    
    void Awake()
    {
        title_turret.SetSelected(true);
        title_turret.button.onClick.AddListener(
            delegate {
                title_enemy.SetSelected(false);
                title_turret.SetSelected(true);

                currentManual = 0;
                Switch(0);

            }
            
            );
        title_enemy.SetSelected(false);
        title_enemy.button.onClick.AddListener(
            delegate {
                title_enemy.SetSelected(true);
                title_turret.SetSelected(false);

                currentManual = 1;
                Switch(1);

            }

        );

        CreateButtons();
        Switch(0);
    }


    /// <summary>
    /// 退出图鉴场景
    /// </summary>
    public void Quit()
    {
        SceneManager.LoadScene("MainMenu");
    }


    /// <summary>
    /// 创建按钮对象
    /// </summary>
    public void CreateButtons()
    {
        buttonList_turret = new List<TurretSelectButton>();
        AddTurretButton(TurretInfo.TURRET_MARISA);
        AddTurretButton(TurretInfo.TURRET_CIRNO);
        AddTurretButton(TurretInfo.TURRET_SAKUYA);
        AddTurretButton(TurretInfo.TURRET_TEI);
        AddTurretButton(TurretInfo.TURRET_MINORIKO);
        AddTurretButton(TurretInfo.TURRET_YOUMU);
        AddTurretButton(TurretInfo.TURRET_MOKOU);
        AddTurretButton(TurretInfo.TURRET_UTSUHO);
        AddTurretButton(TurretInfo.TURRET_REMILIA);
        AddTurretButton(TurretInfo.TURRET_YUUKA);
        AddTurretButton(TurretInfo.TURRET_TENSHI);
        AddTurretButton(TurretInfo.TURRET_YUYUKO);
        AddTurretButton(TurretInfo.TURRET_RAN);
        SelectTurret(buttonList_turret[0]);

        buttonList_enemy = new List<EnemySelectButton>();
        
        AddEnemyButton(EnemyInfo.ENEMY_MAOYU);
        //红
        AddEnemyButton(EnemyInfo.ENEMY_CIRNO);
        AddEnemyButton(EnemyInfo.ENEMY_RUMIA);
        AddEnemyButton(EnemyInfo.ENEMY_MEILIN);
        AddEnemyButton(EnemyInfo.ENEMY_PATCHOULI);
        AddEnemyButton(EnemyInfo.ENEMY_SAKUYA);
        AddEnemyButton(EnemyInfo.ENEMY_REMILIA);
        AddEnemyButton(EnemyInfo.ENEMY_FLANDRE);
        //妖
        AddEnemyButton(EnemyInfo.ENEMY_CHEN);
        AddEnemyButton(EnemyInfo.ENEMY_ALICE);
        AddEnemyButton(EnemyInfo.ENEMY_SHANGHAI);
        AddEnemyButton(EnemyInfo.ENEMY_YOUMU);
        AddEnemyButton(EnemyInfo.ENEMY_YUYUKO);
        AddEnemyButton(EnemyInfo.ENEMY_YUKARI);
        //永
        AddEnemyButton(EnemyInfo.ENEMY_KAGUYA);
        AddEnemyButton(EnemyInfo.ENEMY_EIRIN);
        //风
        AddEnemyButton(EnemyInfo.ENEMY_AYA);
        AddEnemyButton(EnemyInfo.ENEMY_SANAE);
        AddEnemyButton(EnemyInfo.ENEMY_KANAKO);
        AddEnemyButton(EnemyInfo.ENEMY_SUWAKO);
        //地
        AddEnemyButton(EnemyInfo.ENEMY_RIN);
        AddEnemyButton(EnemyInfo.ENEMY_YUUGI);
        //花
        AddEnemyButton(EnemyInfo.ENEMY_YUUKA);
        AddEnemyButton(EnemyInfo.ENEMY_SHIKIEKI);
        //萃
        AddEnemyButton(EnemyInfo.ENEMY_SUIKA);
        AddEnemyButton(EnemyInfo.ENEMY_SUIKA_SMALL);


    }

    public GameObject  AddTurretButton(int turretID)
    {
        GameObject button = Instantiate(buttonPrefab_turret,layout_turret );

        TurretSelectButton tsb = button.GetComponent<TurretSelectButton>();

        tsb.SetID(turretID);
        

        tsb.button.onClick.AddListener(
            delegate { SelectTurret(tsb);  }
            );

        tsb.index = buttonList_turret.Count;
        tsb.SetSelected(selected_turret == tsb.index);
        buttonList_turret.Add(tsb);

        return button;
    }

    public GameObject AddEnemyButton(int enemyID)
    {
        GameObject button = Instantiate(buttonPrefab_enemy, layout_enemy);

        EnemySelectButton tsb = button.GetComponent<EnemySelectButton>();

        tsb.SetID(enemyID);


        tsb.button.onClick.AddListener(
            delegate { SelectEnemy(tsb); }
            );

        tsb.index = buttonList_enemy.Count;
        tsb.SetSelected(selected_enemy == tsb.index);
        buttonList_enemy.Add(tsb);

        return button;
    }


    public void ShowTurretInfo(int id)
    {
        show_image.sprite = TurretInfo.GetTurretInfo(id).buttonImage;
        show_name.text = TurretInfo.GetTurretInfo(id).name;
        show_intro.text = TurretInfo.GetTurretInfo(id).intro;
        show_msg1.text = "价格："+TurretInfo.GetTurretInfo(id).price[0];
        show_msg2.text = "";
    }

    public void ShowEnemyInfo(int id)
    {
        EnemyInfo.ENEMY info = EnemyInfo.GetEnemyInfo(id);
        show_image.sprite = info.image;
        show_name.text = info.name;
        show_intro.text = info.intro;

        string hp;
        if (info.hp > 1500) hp = "非常高";
        else if (info.hp > 700) hp = "高";
        else if (info.hp > 300) hp = "普通";
        else hp = "低";

        string speed;
        if (info.cellTime > 0.8) speed = "非常慢";
        else if (info.cellTime > 0.61) speed = "较慢";
        else if (info.cellTime > 0.46) speed = "较快";
        else speed = "非常快";

        string abilities="";
        if (info.physicalResist > 0) abilities += "物理抗性" + info.physicalResist+"; ";
        if (info.magicalResist > 0) abilities += "魔法抗性" + info.magicalResist + "; ";

        if (info.frozenResist ) abilities += "免疫冰冻" + "; ";
        if (info.burnResist) abilities += "免疫烧伤" +  "; ";
        //if (info.physicalResist > 0) abilities += "物理抗性" + info.physicalResist + "; ";



        show_msg1.text = "生命：" + hp + " 速度：" + speed + "\n赏金："+info.reward ;
        show_msg2.text = "特殊能力："+abilities;
    }

    public void SelectTurret(TurretSelectButton tsb)
    {
        /*
        enemy.SetActive(false);
        turret.SetActive(true);*/

        if (currentManual != 0) return;

        buttonList_turret[selected_turret].SetSelected(false);
        tsb.SetSelected(true);
        selected_turret = tsb.index;

        ShowTurretInfo(tsb.turretID);
        
    }

    public void SelectEnemy(EnemySelectButton esb)
    {
        if (currentManual != 1) return;

        buttonList_enemy[selected_enemy].SetSelected(false);
        esb.SetSelected(true);
        selected_enemy = esb.index;

        ShowEnemyInfo(esb.enemyID);
    }

    public void Switch(int i)
    {
        if(i == 0)
        {
            enemy.SetActive(false);
            turret.SetActive(true);
            SelectTurret (buttonList_turret [selected_turret ]);
        }
        else
        {
            turret.SetActive(false);
            enemy.SetActive(true);
            SelectEnemy(buttonList_enemy[selected_enemy]);
        }
    }
}
