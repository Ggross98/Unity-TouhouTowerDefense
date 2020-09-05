using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RoleSelectScene : MonoBehaviour
{
    public GameObject turret_up, turret_down;
    public GameObject buttonPrefab;


    private List<int> turretList;
    private List<int> selectableTurretList;
    private int selected = 0;

    private List<TurretSelectButton> upButtons;
    private TurretSelectButton[] downButtons; 

    public static int turretCount = 8;
    //private int[] selectedTurretArray = new int[turretCount];

    // Start is called before the first frame update
    void Start()
    {
        turretList = new List<int>{ TurretInfo.TURRET_MARISA,  TurretInfo.TURRET_CIRNO, TurretInfo.TURRET_TEI, TurretInfo.TURRET_MINORIKO,
            TurretInfo.TURRET_SAKUYA,  TurretInfo.TURRET_MOKOU, TurretInfo.TURRET_UTSUHO, TurretInfo.TURRET_YOUMU,
            TurretInfo.TURRET_REMILIA,  TurretInfo.TURRET_YUUKA, TurretInfo.TURRET_TENSHI, TurretInfo.TURRET_YUYUKO,
            TurretInfo.TURRET_RAN
            };

        

        CreateTurretSelectUI();

        ReadPlayerInfo();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ReadPlayerInfo()
    {
        if(PlayerInfo .instance != null&&PlayerInfo .instance .selectableTurrets != null)
        {
            selectableTurretList  = PlayerInfo.instance.selectableTurrets;
            //如果list长度不够，补全
            /*
            for(int i = selectableTurretList.Count; i < turretCount; i++)
            {
                selectableTurretList.Add(0);
            }*/
            Refresh();
        }
        if(selectableTurretList == null)
        {
            selectableTurretList = new List<int>();
        }

        assist_role = new List<int>();
        assist_role.Add(Utils.ASSIST_REIMU);
        assist_role.Add(Utils.ASSIST_AKYUU);
        assist_role.Add(Utils.ASSIST_RINNOSUKE);
        assist_role.Add(Utils.ASSIST_LETTY);
        assist_role.Add(Utils.ASSIST_BYAKUREN);

        if (PlayerInfo .instance != null)
        {
            SelectAssistRoleWithID(51);
        }
        else
        {
            SelectAssistRole(1);
        }
    }

    private void WritePlayerInfo()
    {
        List<int> l = PlayerInfo.instance.selectableTurrets;
        l = new List<int>();
        for(int i = 0; i < selectableTurretList.Count; i++)
        {
            //Debug.Log(selectableTurretList[i]);
            l.Add(selectableTurretList[i]);
        }

        PlayerInfo.instance.assist = selectedAssistRole;
    }

    /// <summary>
    /// 根据选中的list来更新UI
    /// </summary>
    private void Refresh()
    {
        List<int> l = new List<int>(selectableTurretList);
        //上半部分
        for(int i =0;i<l.Count; i++)
        {
            for(int j= 0; j < upButtons .Count; j++)
            {
                if(l[i] == upButtons[j].turretID)
                {
                    upButtons[j].SetSelected(true);
                    //l.RemoveAt(i);
                }
            }
        }
        //下半部分
        for(int i = 0; i < l.Count; i++)
        {
            downButtons[i].SetID(l[i]);
        }
        for(int i = l.Count; i < turretCount; i++)
        {
            downButtons[i].SetID(0);
        }
    }

    public void StartGame()
    {
        WritePlayerInfo();
        //DontDestroyOnLoad(PlayerInfo.instance);

        SceneManager.LoadScene("Loading" );

        AudioManager.instance.StopMusic();
    }

    public void Exit()
    {
        
        SceneManager.LoadScene("LevelSelect");
    }

    private void CreateTurretSelectUI()
    {
        //上半部分的按钮：单机选择，再次单击取消选择
        upButtons = new List<TurretSelectButton>();
        for(int i =0;i<turretList .Count ; i++)
        {
            TurretSelectButton tsb = CreateSelectButton(turret_up.transform, turretList[i]);
            tsb.button.onClick.AddListener(
                
                delegate {
                    //Debug.Log("按下");
                    if(!tsb.selected)
                    {
                        Select(tsb);
                    }
                    else {
                        CancelSelect(tsb);
                    }

                }
                
                );
            tsb.SetSelected(false);

            upButtons.Add(tsb);
        }



        //下半部分的按钮
        downButtons = new TurretSelectButton[turretCount];
        for (int j = 0; j < turretCount; j++)
        {
            TurretSelectButton tsb = CreateSelectButton(turret_down.transform, 0);
            tsb.index = j;
            tsb.button.interactable = true;
            tsb.SetSelected(true);
            tsb.SetID(0);
            downButtons[j] = tsb;

            tsb.button.onClick.AddListener(

                delegate {
                    CancelSelectFromDownButton(tsb);
                }

                );
        }
        
    }


    private void Select(TurretSelectButton tsb)
    {
        //如已经选中，不能再选
        for(int i = 0; i < selected; i++)
        {
            if(downButtons [i].turretID == tsb.turretID)
            {
                return;
            }
        }
        //已经选了最大数量，不能选
        if (selected >= turretCount) return;

        //Debug.Log("选中");
        //在下方显示图片
        downButtons[selected++].SetID(tsb.turretID);
        selectableTurretList.Add(tsb.turretID);
        tsb.SetSelected(true);

    }

    public void CancelSelect()
    {
        for(int i = turretCount - 1; i >= 0; i--)
        {
            CancelSelectFromDownButton(downButtons[i]);
        }
    }

    private void CancelSelect(TurretSelectButton tsb)
    {
        for(int i = 0; i < selected; i++)
        {
            if(downButtons [i].turretID == tsb.turretID)
            {
                for(int j = i; j < selected-1; j++)
                {
                    downButtons[j].SetID(downButtons [j+1].turretID );
                }
                downButtons[selected-1].SetID(0);

                tsb.SetSelected(false);
                //downButtons[i].SetID(0);
                selectableTurretList.Remove(tsb.turretID);
                selected--;
                return;
            }
        }
    }

    private void CancelSelectFromDownButton(TurretSelectButton tsb)
    {
        if(tsb.turretID != 0)
        {
            for(int i = 0; i < upButtons .Count; i++)
            {
                if(upButtons [i].turretID == tsb.turretID)
                {
                    CancelSelect(upButtons[i]);
                    return;
                }
            }
        }
    }

    private TurretSelectButton  CreateSelectButton(Transform parent, int id)
    {
        GameObject obj = Instantiate(buttonPrefab, parent);

        TurretSelectButton tsb = obj.GetComponent<TurretSelectButton>();
        tsb.SetID(id);

        return tsb;
        

    }




    #region 支援角色选择
    public Image assist_image;
    public int selectedAssistRole = 0;
    public Text assist_name, assist_intro;

    private List<int> assist_role = new List<int>();
    private int assist_index;

    public Button assist_next, assist_last;

    public void SelectAssistRole(int i)
    {
        if (i >= 0 && i < assist_role.Count)
        {
            assist_index = i;
            selectedAssistRole = assist_role[i];

            Utils.AssistRole ar = Utils.GetAssistRoleInfo(assist_role[i]);
            if(ar!= null)
            {
                assist_image.sprite = ar.image;
                assist_name.text = ar.name;
                assist_intro.text = ar.intro;
            }
            

        }
    }

    public void SelectAssistRoleWithID(int id)
    {
        for(int i =0;i<assist_role.Count; i++)
        {
            if(assist_role [i] == id)
            {
                SelectAssistRole(i);
                return;
            }
        }
    }

    public void NextAssistRole()
    {
        int index = assist_index + 1;
        if(index>assist_role .Count-1)
        {
            index = 0;
        }
        SelectAssistRole(index);
    }

    public void LastAssistRole()
    {
        int index = assist_index - 1;
        if (index <0)
        {
            index = assist_role .Count -1;
        }
        SelectAssistRole(index);
    }


    #endregion
}
