using UnityEngine;
using System.Collections;

public class XLine : MonoBehaviour {
	public GameObject Line;
	public GameObject FXef;//激光击中物体的粒子效果
	// Use this for initialization
	// Update is called once per frame
	void Update () {
		RaycastHit hit;
		Vector3 Sc;// 变换大小
		Sc.x=0.5f;
		Sc.z=0.5f;
		//发射射线，通过获取射线碰撞后返回的距离来变换激光模型的y轴上的值
        if (Physics.Raycast(transform.position, this.transform.forward, out hit)){
			Debug.DrawLine(this.transform.position,hit.point);
			Sc.y=hit.distance;
			FXef.transform.position=hit.point;//让激光击中物体的粒子效果的空间位置与射线碰撞的点的空间位置保持一致；
			FXef.SetActive(true);
		}
		//当激光没有碰撞到物体时，让射线的长度保持为500m，并设置击中效果为不显示
		else{
			Sc.y=500;
		    FXef.SetActive(false);
		}
			
		Line.transform.localScale=Sc;
            
	}
}
