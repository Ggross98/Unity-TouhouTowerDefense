using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 创建炮塔、管理炮塔
/// </summary>
public class TurretController : MonoBehaviour
{

    private MapController mapController;

    private StageController stageController;

    private TurretSelectUIController ui;
    

    //炮塔预制体
    public GameObject turret_marisa, turret_cirno, turret_tei, turret_minoriko, turret_mokou, 
        turret_yukari, turret_utsuho, turret_yuuka, turret_yuyuko, turret_youmu, turret_remilia, 
        turret_tenshi, turret_sakuya, turret_ran;
    //预制体数组
    public GameObject[] selectablePrefabs;


    //public GameObject buttonPrefab;

    //从上个场景加载：可选炮塔的编号
    public List<int> selectableTurrets;

    public int turretCount = 2;
    public int selectedTurret = -1;
    

    //UI中的按钮数组
    //public TurretSelectButton[] buttons;


    //场景中已经生成的防御塔
    public List<TurretScript> list_turret= new List<TurretScript> ();
    public List<MinorikoScript> list_minoriko = new List<MinorikoScript> ();

    //public Sprite image_marisa, image_cirno;
    //private Sprite[] images;

    //选中塔时，选择鼠标位置
    public GameObject mouseMark, rangeMark;

    
    void Awake()
    {

        GameObject map = GameObject.Find("Map");
        mapController = map.GetComponent<MapController>();
        stageController = map.GetComponent<StageController>();

        ui = GameObject.Find("TurretSelectUI").GetComponent<TurretSelectUIController>();


        //selectablePrefabs = new GameObject[turretCount ];


        //加载选中的防御塔信息
        /*GameObject playerSetting = GameObject.Find("PlayerSetting");
        if(playerSetting != null)
        {
            PlayerInfo info = playerSetting.GetComponent<PlayerInfo>();
            if(info.selectableTurrets != null)
            {
                selectableTurrets = info.selectableTurrets;
            }
        }*/
        if(PlayerInfo .instance != null)
        {
            selectableTurrets = PlayerInfo.instance.selectableTurrets;

        }
        if(selectableTurrets == null || selectableTurrets .Count <=0)
        {
            selectableTurrets = new List<int>();
            selectableTurrets.Add(TurretInfo.TURRET_MARISA);
            selectableTurrets.Add(TurretInfo.TURRET_CIRNO);
            selectableTurrets.Add(TurretInfo.TURRET_UTSUHO);
            selectableTurrets.Add(TurretInfo.TURRET_MOKOU);
            selectableTurrets.Add(TurretInfo.TURRET_YOUMU);
            selectableTurrets.Add(TurretInfo.TURRET_MINORIKO);
        }
        
        
        
        //根据所选防御塔，加载ui信息
        LoadPrefabs();

        ui.LoadSelectableTurrets(selectableTurrets);
        //ui.LoadPrefabs(selectablePrefabs);
        ui.CreateUIObjects();
    }

    public void LoadPrefabs()
    {
        //selectablePrefabs[0] = turret_marisa;
        //selectablePrefabs[1] = turret_cirno;

        turretCount = selectableTurrets.Count;
        selectablePrefabs = new GameObject[turretCount];

        for(int i = 0; i < turretCount; i++)
        {
            switch(selectableTurrets[i])
            {
                case TurretInfo.TURRET_MARISA:
                    selectablePrefabs[i] = turret_marisa;
                    break;
                case TurretInfo.TURRET_CIRNO:
                    selectablePrefabs[i] = turret_cirno;
                    break;
                case TurretInfo.TURRET_TEI:
                    selectablePrefabs[i] = turret_tei;
                    break;
                case TurretInfo.TURRET_MINORIKO:
                    selectablePrefabs[i] = turret_minoriko;
                    break;
                case TurretInfo.TURRET_YOUMU:
                    selectablePrefabs[i] = turret_youmu;
                    break;
                case TurretInfo.TURRET_MOKOU:
                    selectablePrefabs[i] = turret_mokou; break;
                case TurretInfo.TURRET_UTSUHO:
                    selectablePrefabs[i] = turret_utsuho; break;
                case TurretInfo.TURRET_REMILIA:
                    selectablePrefabs[i] = turret_remilia; break;
                case TurretInfo.TURRET_YUUKA:
                    selectablePrefabs[i] = turret_yuuka; break;
                case TurretInfo.TURRET_YUYUKO:
                    selectablePrefabs[i] = turret_yuyuko; break;
                case TurretInfo.TURRET_YUKARI:
                    selectablePrefabs[i] = turret_yukari; break;
                case TurretInfo.TURRET_SAKUYA:
                    selectablePrefabs[i] = turret_sakuya; break;
                case TurretInfo.TURRET_TENSHI:
                    selectablePrefabs[i] = turret_tenshi; break;
                case TurretInfo.TURRET_RAN:
                    selectablePrefabs[i] = turret_ran; break;

            }
        }


        /*
        images = new Sprite[turretCount];
        images[0] = image_marisa ;
        images[1] = image_cirno;
        */
    }


    

    // Update is called once per frame
    void Update()
    {

        if (stageController.pausing) return;


        Vector3 lPos = Input.mousePosition;
        Vector3Int cPos = mapController.WorldToCell(Camera.main.ScreenToWorldPoint(lPos));
        Vector3 newPos = mapController.CellToWorld(cPos);

        //如果所选地点不在场景内，不管
        if (mapController.CellOutOfBound(cPos))
        {
            mouseMark.GetComponent<RectTransform>().anchoredPosition = new Vector2(5000, 5000);
            return;
        }
        //如果在场景内
        else
        {
            //获取所选塔
            selectedTurret = ui.selectedTurret;
            //如果已经选取了塔
            if (selectedTurret != -1)
            {
                
                mouseMark .GetComponent <RectTransform >().anchoredPosition = Utils.GetUIPosition(newPos);
                float r = TurretInfo.GetTurretInfo(selectableTurrets[selectedTurret]).range[0];
                float range = Utils.LengthLocalToUI( r* 2 * GameObject.Find("Map").GetComponent<MapController>().cellLocalSize);

                rangeMark.GetComponent<RectTransform>().sizeDelta  = new Vector2(range, range);

                //能够造塔
                if(CanCreateTurret (newPos))
                {
                    mouseMark.GetComponent<Image>().color = Color.green;
                    //单击左键建造
                    if (Input.GetMouseButtonDown(0))
                    {
                        CreateTurret(newPos);

                        Vector3Int aPos = mapController.CellToArray(cPos);
                        mapController.mapArray[aPos.x, aPos.y] = Utils.TURRET;    //地图中此位置标记为TURRET

                        //敌人改变默认路径
                        GameObject.Find("Map").GetComponent<PathFinder>().RefreshDefaultPath();
                    }


                }
                //不能造塔
                else
                {
                    mouseMark.GetComponent<Image>().color = Color.red;

                    if(Input.GetMouseButtonDown(0)) AudioManager.instance.PlaySound("se_invalid");
                }

            }
            //没选中塔
            else
            {
                mouseMark.GetComponent<RectTransform>().anchoredPosition = new Vector2(5000, 5000);
                if (Input.GetMouseButtonDown(0))
                {
                    Vector3 wPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    TurretScript ts = GetTurret(wPos);

                    if (ts != null && !GameObject.Find("TurretUpdateUI").GetComponent<TurretUpdateUIController>().Selected())
                    {
                        GameObject.Find("TurretUpdateUI").GetComponent<TurretUpdateUIController>().ShowTurretInfo(ts);
                    }
                }
            }
            
            

        }


        /*
        //单机左键：摆放新炮塔或查看已摆放炮塔升级界面
        if (Input.GetMouseButtonDown(0))
        {
            selectedTurret = ui.selectedTurret;

            //若已经选中炮塔：放置炮塔
            if(selectedTurret != -1)
            {
                //获得鼠标点击位置
                Vector3 lPos = Input.mousePosition;
                Vector3Int cPos = mapController.WorldToCell(Camera.main.ScreenToWorldPoint(lPos));
                Vector3 newPos = mapController.CellToWorld(cPos);

                //如果所选地点不在场景内，不管
                if (mapController.CellOutOfBound(cPos)) return;


                //如果能放置，则根据选取的角色放置炮塔
                if (CanCreateTurret(newPos))
                {
                    CreateTurret(newPos);

                    Vector3Int aPos = mapController.CellToArray(cPos);
                    mapController.mapArray[aPos.x, aPos.y] = Utils.TURRET;    //地图中此位置标记为TURRET

                    //敌人改变默认路径
                    GameObject.Find("Map").GetComponent<PathFinder>().RefreshDefaultPath();
                    //Debug.Log("place a turret");
                }
                else
                {
                    //AudioSource.PlayClipAtPoint(AudioManager.se_invalid,new Vector3(0,0,0));
                    //AudioManager.instance.PlaySound("se_invalid");
                }

                return;
            }
            //否则查看升级界面
            else
            {
                

            }
            
   
        }*/

        //右键取消选择
        //if(Input.GetMouseButtonDown(1))
        //{
        //    CancelSelect();
        //}
    }

    //public void SelectTurret(int index)
    //{
    //    if (index < 0 || index >= turretCount) return;
    //    selectedTurret = index;
    //    for(int i = 0; i < turretCount; i++)
    //    {
    //        if (i == index) buttons[i].SetSelected(true);
    //        else buttons[i].SetSelected(false);
    //    }

    //}

    //public void SelectTurret(TurretSelectButton tsb)
    //{
    //    SelectTurret(tsb.index);
    //}

    //public void CancelSelect()
    //{
    //    selectedTurret = -1;
    //    for (int i = 0; i < turretCount; i++)
    //    {
    //        buttons[i].SetSelected(false);
    //    }
    //}

    public bool CanCreateTurret(Vector3 wPos)
    {
        Vector3Int cPos = mapController.WorldToCell(wPos);
        Vector3 aPos = mapController.CellToArray(cPos);

        if (stageController.gold < TurretInfo .GetTurretInfo (selectableTurrets[selectedTurret ]).price [0])
        {
            //AudioManager.instance.PlaySound("se_invalid");
            return false;
        }

        if (mapController.CellOutOfBound(cPos)) return false;

        int i = mapController.GetMapArray()[(int)aPos.x, (int)aPos.y];//地块的标记
        if (i == Utils.GROUND || i == Utils.PLATFORM )
        {
            if (!mapController.gameObject.GetComponent<PathFinder>().CanPlaceTurret(new Vector2Int((int)aPos.x, (int)aPos.y)))
            {
                Debug.Log("不能把路堵死");
                //AudioManager.instance.PlaySound("se_invalid");
                return false;
            }


            return true;
        }

        return false;
    }

    public TurretScript GetTurret(Vector3 wPos)
    {
        Vector3Int cPos = mapController.WorldToCell(wPos);
        Vector3 _wPos = mapController.CellToWorld(cPos);
        Vector2 p = new Vector2(_wPos.x, _wPos.y);

        for(int i =0;i<list_turret.Count; i++)
        {
            if(list_turret [i].GetPosition () == p)
            {
                return list_turret[i];
            }
        }

        return null;
    }

    public GameObject CreateTurret(Vector3 pos)
    {
        GameObject newTurret = null;

        int price = TurretInfo.GetTurretInfo(selectableTurrets[selectedTurret]).price[0];
        if (stageController .gold >= price)
        {
            newTurret = Instantiate(selectablePrefabs[selectedTurret ]);

            TurretScript ts = newTurret.GetComponent<TurretScript>();
            ts.SetPosition(pos);

            list_turret.Add(ts);

            if(ts.turretID == TurretInfo .TURRET_MINORIKO)
            {
                MinorikoScript ms = newTurret.GetComponent<MinorikoScript>();
                if (ms != null) list_minoriko.Add(ms);
            }

            stageController.gold -= price;

        

        
        }

        return newTurret;

    }

    public void UpdateTurret(TurretScript ts)
    {
        Debug.Log("升级防御塔！");

        if (ts.level == ts.maxLevel) return;

        if(stageController .gold >= ts.GetUpdateGold())
        {
            
            stageController.gold -= ts.GetUpdateGold();
            ts.LevelUp();

            AudioManager.instance.PlaySound("se_levelup");
        }
    }

    public void SaleTurret(TurretScript ts)
    {
        stageController.gold += ts.GetSaleGold();
        GameObject.Find("TurretUpdateUI").GetComponent<TurretUpdateUIController>().HideTurretInfo();

        Vector3Int aPos = mapController.WorldToArray(ts.GetPosition());

        mapController.mapArray[aPos.x, aPos.y] = Utils.GROUND;

        DeleteTurret(ts);

        AudioManager.instance.PlaySound("se_sell");


        //敌人改变默认路径
        GameObject.Find("Map").GetComponent<PathFinder>().RefreshDefaultPath();
    }




    public void DeleteTurret(TurretScript ts)
    {
        if(ts.turretID == TurretInfo .TURRET_MINORIKO)
        {
            MinorikoScript ms = ts.gameObject.GetComponent<MinorikoScript>();
            if(ms!= null && list_minoriko .Contains(ms))
            {
                list_minoriko.Remove(ms);
            }
        }


        if(list_turret .Contains(ts))
        {
            Destroy(ts.gameObject);
            list_turret.Remove(ts);
        }
    }
}
