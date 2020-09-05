using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurretUpdateUIController : MonoBehaviour
{
    GameObject updateUI;

    Button sale, update;
    Text saleText, updateText, levelText;

    TurretScript selected;
    TurretController controller;

    GameObject fireRange;

    //public bool showing = false;

    void Awake()
    {
        updateUI = GameObject.Find("TurretUpdateUI");

        controller = GameObject.Find("TurretSelectUI").GetComponent<TurretController>();

        GameObject s = GameObject.Find("Sale");
        sale = s.GetComponent<Button>();
        saleText = s.GetComponentInChildren<Text>();

        sale.onClick.AddListener(delegate {

            if(selected != null)
            {
                controller.SaleTurret(selected);
                selected = null;
            }
            

        });

        GameObject u = GameObject.Find("Update");
        update = u.GetComponent<Button>();
        updateText = u.GetComponentInChildren<Text>();

        update.onClick.AddListener(delegate {
            //Debug.Log("will update a turret");
            if (selected != null)
            
            {
                
                controller.UpdateTurret(selected);
                ShowTurretInfo(selected);
                //selected = null;
            }

        });

        levelText = GameObject.Find("Level").GetComponentInChildren<Text>();

        HideTurretInfo();

        selected = null;


        //fireRange = GameObject.Find("FireRange");
    }

 


    public void ShowTurretInfo(TurretScript ts)
    {
        //showing = true;

        selected = ts;
        SetLocalPosition(ts.GetPosition());
        //updateUI.GetComponent<RectTransform>().anchoredPosition = ts.GetPosition ();

        if(ts.level >= ts.maxLevel)
        {
            update.interactable = false;
            updateText.text = "--";
        }
        else
        {
            update.interactable = true;
            //Debug.Log("enemy's price is " + ts.price);
            updateText.text = "$" + ts.GetUpdateGold ();
        }

        sale.interactable = true;
        saleText.text = "$" + ts.GetSaleGold();

        levelText.text = (ts.level >= ts.maxLevel) ? "Max" : ts.level + "";

        //GameObject.Find("DrawToolkit").GetComponent<DrawToolkit>().DrawCircle(ts.transform .position , ts.range );

        RectTransform rt = GameObject.Find("Range").GetComponent<RectTransform>();
        rt.anchoredPosition = GetUIPosition(ts.transform.position);


        float range = Utils.LengthLocalToUI(ts.range *2* GameObject.Find("Map").GetComponent<MapController>().cellLocalSize);


        rt.sizeDelta = new Vector2(range,range);
    }

    public void HideTurretInfo()
    {
        //showing = false;
        selected = null;
        SetLocalPosition(new Vector3(2000, 2000, 0));


        RectTransform rt = GameObject.Find("Range").GetComponent<RectTransform>();
        rt.anchoredPosition = new Vector2 (-1000,-1000);

        //updateUI.GetComponent<RectTransform>().anchoredPosition = new Vector2(3000, 3000);
    }

    /// <summary>
    /// 把世界坐标设置为UI坐标
    /// </summary>
    /// <param name="wPos"></param>
    public void SetLocalPosition(Vector3 wPos)
    {
        //Debug.Log(Screen.width + "," + Screen.height);
        updateUI.transform.position = wPos;
        
        Vector3 screenPoint = RectTransformUtility.WorldToScreenPoint(Camera.main , wPos);
        //updateUI.GetComponent<RectTransform>().anchoredPosition = screenPoint;
        //Debug.Log(screenPoint);
        float scale = 1920f / Screen.width;
        Vector2 point = new Vector2(scale *screenPoint.x - 1920/ 2, scale *screenPoint.y - 1080 / 2);
        updateUI.GetComponent<RectTransform>().anchoredPosition = point;

        //Vector3 screenPoint = wPos;
        /*
        RectTransform rt = GameObject.Find("UICanvas").GetComponent<RectTransform>();
        Vector3 localPoint;
        RectTransformUtility.ScreenPointToWorldPointInRectangle(rt, screenPoint, Camera.main, out localPoint);
        updateUI.GetComponent<RectTransform>().anchoredPosition = localPoint;
        */
        //Debug.Log("turret's position is" + lPos);
        //Debug.Log("ui's position is" + wPos);
        //updateUI.GetComponent<RectTransform>().anchoredPosition = wPos;
    }

    public static Vector2 GetUIPosition(Vector3 wPos)
    {
        
        Vector3 screenPoint = RectTransformUtility.WorldToScreenPoint(Camera.main, wPos);
   
        float scale = 1920f / Screen.width;

        Vector2 point = new Vector2(scale * screenPoint.x - 1920 / 2, scale * screenPoint.y - 1080 / 2);

        return point;
    }

    public bool Selected()
    {
        return selected != null;
    }
    
}
