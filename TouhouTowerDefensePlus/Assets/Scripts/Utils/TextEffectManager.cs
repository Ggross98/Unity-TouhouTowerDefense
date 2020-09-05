using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 在游戏过程中显示文字的工具类
/// 包括：防御塔的等级，获得金币的提示
/// </summary>
public class TextEffectManager : MonoBehaviour
{
    public static TextEffectManager instance;

    public GameObject textPrefab;

    private List<GameObject> pool = new List<GameObject>();

    public void ShowTextAtPosition(string msg, Color color, int size, Vector3 wPos)
    {

    }
    public void ShowTextAtPosition(string msg, Color color, int size, Vector3 wPos, float duration)
    {
        GameObject go = GetActiveObject();
        Text text = go.GetComponent<Text>();
        text.text = msg;
        text.color = color;
        text.fontSize = size;

        Utils.SetUIPosition(go.GetComponent<RectTransform>(), wPos);


        StartCoroutine(RecycleInTime(go, duration));
    }

    private IEnumerator RecycleInTime(GameObject go, float time)
    {
        yield return new WaitForSeconds(time);

        Recycle(go);

        yield return null;
    }


    public GameObject ShowTurretLevel(TurretScript ts)
    {
        GameObject go = GetActiveObject();

        Text text = go.GetComponent<Text>();
        if (ts.level == ts.maxLevel) text.text = "★";
        else text.text = ts.level + "";

        text.color = new Color(1,215f/255f,0,1);
        text.fontSize = 10;

        Utils.SetUIPosition(go.GetComponent<RectTransform>(), ts.transform.position);

        return go;
    }


    private GameObject GetActiveObject()
    {
        //如果没有可用对象，生成
        if(pool.Count == 0)
        {
            GameObject go = Instantiate(textPrefab, this.transform );
            go.SetActive(true);
            go.GetComponent<Text>().text = "";
            //Debug.Log("生成对象");
            return go;

        }
        //如果有可用对象，将其从池中拿出并激活
        else
        {
            GameObject go = pool[0];
            pool.RemoveAt(0);
            go.SetActive(true);
            return go;
        }
    }


    public void Recycle(GameObject go)
    {
        if (go.GetComponent<Text>() == null) return;
        else
        {
            go.SetActive(false);
            pool.Add(go);
        }
    }

    void Awake()
    {
        //textPrefab = Resources.Load<GameObject>("InGameTextPrefab");
        instance = this;

    }
}
