using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 存储玩家游戏信息的类
/// 1.玩家选择的关卡
/// 2.玩家选择的防御塔和支援角色
/// 
/// </summary>
public class PlayerInfo : MonoBehaviour
{
    public int level = 0;

    public bool endless = false;

    public int assist = Utils.ASSIST_AKYUU;

    //public const int ASSIST_REIMU = 1000, ASSIST_CIRNO = 1001;

    public List<int> selectableTurrets =  null;

    public static PlayerInfo instance;

    void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

}
