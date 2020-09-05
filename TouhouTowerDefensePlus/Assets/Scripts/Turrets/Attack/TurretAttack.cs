using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 防御塔的攻击方式
/// 包括子弹、爆炸和激光
/// </summary>
public class TurretAttack : MonoBehaviour
{
    //基础伤害
    public float damage;
    
    //攻击属性
    public int type;
    public const int MAGICAL = 11, PHYSICAL = 12, NONE = 13;
    //攻击特效和效果时间
    public int effect = 0;
    public float effectTime = 0;
    public const int EFFECT_FROZEN = 21, EFFECT_BURNED = 22, EFFECT_STUNED = 23;
    //攻击目标
    public EnemyScript target;
    //攻击形式：子弹，激光或爆炸
    public int form;
    public const int BULLET = 1, EXPLOSION = 2, LASER = 3;
    //发射这个子弹的塔
    [HideInInspector]
    public TurretScript turret;
    //触发时音效
    public AudioClip clip;
    
}
