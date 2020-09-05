using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurretSelectUIController : MonoBehaviour
{
    //UI按钮预制体
    public GameObject buttonPrefab;

    //UI中的按钮数组
    public TurretSelectButton[] buttons;

    private List<int> selectableTurrets;

    //按钮的图片
    public Sprite image_marisa, image_cirno,image_tei, image_minoriko;

    //炮塔总数、选中炮塔的Index
    private int turretCount;
    public int selectedTurret = -1;
    
    private TurretUpdateUIController updateUI;


    void Awake()
    {
        updateUI = GameObject.Find("TurretUpdateUI").GetComponent<TurretUpdateUIController>();
    }

    public void LoadSelectableTurrets(List<int> list)
    {
        selectableTurrets = list;

        turretCount = list.Count;

        //images = new Sprite[turretCount];

        /*
        for(int i = 0; i < turretCount; i++)
        {
            switch (list[i])
            {
                case TurretInfo.TURRET_MARISA :
                    images[i] = image_marisa;
                    break;
                case TurretInfo.TURRET_CIRNO:
                    images[i] = image_cirno;
                    break;
                case TurretInfo.TURRET_TEI:
                    images[i] = image_tei;
                    break;
                case TurretInfo.TURRET_MINORIKO:
                    images[i] = image_minoriko;
                    break;
            }
        }*/

        //prefabs = new GameObject[turretCount];
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(1))
        {
            CancelSelect();
            updateUI.HideTurretInfo();
        }

        if(Input.GetKeyDown (KeyCode.Alpha1)&& turretCount >= 1)
        {
            updateUI.HideTurretInfo();
            SelectTurret(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && turretCount >= 2)
        {
            updateUI.HideTurretInfo();
            SelectTurret(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && turretCount >= 3)
        {
            updateUI.HideTurretInfo();
            SelectTurret(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4) && turretCount >= 4)
        {
            updateUI.HideTurretInfo();
            SelectTurret(3);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5) && turretCount >= 5)
        {
            updateUI.HideTurretInfo();
            SelectTurret(4);
        }
        if (Input.GetKeyDown(KeyCode.Alpha6) && turretCount >= 6)
        {
            updateUI.HideTurretInfo();
            SelectTurret(5);
        }
        if (Input.GetKeyDown(KeyCode.Alpha7) && turretCount >= 7)
        {
            updateUI.HideTurretInfo();
            SelectTurret(6);
        }
        if (Input.GetKeyDown(KeyCode.Alpha8) && turretCount >= 8)
        {
            updateUI.HideTurretInfo();
            SelectTurret(7);
        }
    }






    public void CreateUIObjects()
    {
        HorizontalLayoutGroup layout = GetComponent<HorizontalLayoutGroup>();
        layout.spacing = turretCount;
        RectTransform rt = GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2((turretCount) * 150 + 100, 150);

        buttons = new TurretSelectButton[turretCount];


        for (int i = 0; i < turretCount; i++)
        {
            GameObject newButton = Instantiate(buttonPrefab, this.gameObject.transform);
            TurretSelectButton tsb = newButton.GetComponent<TurretSelectButton>();
            //tsb.image.sprite = images[i];
            tsb.SetID(selectableTurrets[i]);
            tsb.index = i;

            Text price = newButton.transform.Find("Price").GetComponent<Text>();
            Text keycode = newButton.transform.Find("Keycode").GetComponent<Text>();

            price.text = ("$" + TurretInfo.GetTurretInfo (tsb.turretID).price [0] );
            keycode.text = i + 1 + "";

            tsb.button.onClick.AddListener(

                delegate {
                    updateUI.HideTurretInfo();
                    SelectTurret(tsb);
                }

                );

            tsb.SetSelected(false);

            buttons[i] = tsb;
        }

    }

    public void SelectTurret(int index)
    {
        if (index < 0 || index >= turretCount) return;
        selectedTurret = index;
        for (int i = 0; i < turretCount; i++)
        {
            if (i == index) buttons[i].SetSelected(true);
            else buttons[i].SetSelected(false);
        }

    }

    public void SelectTurret(TurretSelectButton tsb)
    {
        SelectTurret(tsb.index);


    }

    public void CancelSelect()
    {
        selectedTurret = -1;
        for (int i = 0; i < turretCount; i++)
        {
            buttons[i].SetSelected(false);
        }
    }
}
