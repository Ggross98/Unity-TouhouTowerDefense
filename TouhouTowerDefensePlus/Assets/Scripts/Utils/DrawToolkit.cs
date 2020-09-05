using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawToolkit : MonoBehaviour
{
    Vector3 v;                   //圆心，Vector2是2D,当然也可以换Vector3
    float R;					//半径
    int positionCount;			//完成一个圆的总点数，
    float angle;				//转角，三个点形成的两段线之间的夹角
    Quaternion q;               //Quaternion四元数

    public GameObject renderPrefab;
    List<LineRenderer> lineList;          //LineRenderer组件

    LineRenderer line;

    void Start()
    {
        v = new Vector3(0,0,-0.1f);
        R = 6;
        positionCount = 180;
        angle = 360f / (positionCount - 1);
        line = GetComponent<LineRenderer>();
        line.positionCount = positionCount;

        

        //lineList = new List<LineRenderer>();
    }

    void Update()
    {
        //DrawCircle();
    }

    public void DrawCircle(Vector3 center, float r)
    {
        v = center;
        R = r;

        for (int i = 0; i < positionCount; i++)
        {
            if (i != 0)
            {
                q = Quaternion.Euler(q.eulerAngles.x, q.eulerAngles.y, q.eulerAngles.z + angle);
            }
            Vector3 forwardPosition = v + q * Vector3.down * R;
            line.SetPosition(i, forwardPosition);
        }
    }

    public void DrawLine(Vector3 pos1, Vector3 pos2)
    {
        GameObject obj = Instantiate(renderPrefab);
        LineRenderer line = obj.GetComponent<LineRenderer>();
        lineList.Add(line);

        line.SetPosition(0, pos1);
        line.SetPosition(1, pos2);

        line.startWidth = 0.02f;
        line.endWidth = 0.02f;


    }

    public void DeleteGraphics()
    {
        /*for(int i =0;i<lineList.Count; i++)
        {
            Destroy(lineList[i].gameObject);
            lineList.RemoveAt(i);
        }*/
        for (int i = 0; i < positionCount; i++)
        {
            
            line.SetPosition(i, new Vector3 (-1,-1));
        }
    }
}
