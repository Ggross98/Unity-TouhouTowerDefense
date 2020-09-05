using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 所有敌人技能的父类
/// </summary>
public class EnemySkill : MonoBehaviour
{

    public float skillInterval = 5;
    public float countdown;

    protected EnemyScript enemy;
    protected List<EnemyScript> enemyList;

    void Awake()
    {
        enemy = this.gameObject.GetComponent<EnemyScript>();

        enemyList = GameObject.Find("Map").GetComponent<EnemyController>().enemyList;
    }

    void Update()
    {
        if (countdown > 0) countdown -= Time.deltaTime;
        else
        {
            UseSkill();
            countdown = skillInterval;
        }
    }

    public virtual void UseSkill()
    {

    }

}
