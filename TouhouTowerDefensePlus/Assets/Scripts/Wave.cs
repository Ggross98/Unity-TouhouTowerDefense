using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave
{
    //结束回合后获得的金钱奖励
    public int waveGold = 10;
    //此波的时间限制
    public float waveTime = 10f;
    //怪物id
    //public int enemyID = 200;
    public List<int> enemyIdList = new List<int>();
    //出怪时间间隔
    public float enemyInterval = 0.5f;
    //怪物数量
    public int enemyCount = 5;

    /// <summary>
    /// 生成一波混合敌人的方法，一波敌人由相同的几小波组成。
    /// </summary>
    /// <param name="list">每小波敌人的数据。格式：第一种敌人id，第一种敌人数量，第二种敌人id...以此类推</param>
    /// <param name="interval">两个怪物生成的时间间隔</param>
    /// <param name="time">此波的限制时间</param>
    /// <param name="gold">此波的奖励金币</param>
    public Wave(int[] list, int count, float interval, float time, int gold)
    {
        
        for (int i = 0; i < list.Length; i += 2)
        {
            for (int j = 0; j < list[i + 1]; j++)
            {
                enemyIdList.Add(list[i]);
            }
        }
        enemyCount = count;

        waveGold = gold;
        waveTime = time;
        enemyInterval = interval;
    }

    /// <summary>
    /// 生成单一敌人波次的方法
    /// </summary>
    /// <param name="id"></param>
    /// <param name="count"></param>
    /// <param name="interval"></param>
    /// <param name="time"></param>
    /// <param name="gold"></param>
    public Wave(int id, int count, float interval, float time, int gold)
    {

        for(int i = 0; i < count; i++)
        {
            enemyIdList.Add(id);
        }
        enemyCount = count;

        waveGold = gold;
        waveTime = time;
        enemyInterval = interval;
    }

    
    /// <summary>
    /// 获得这一波次中第Index个敌人
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public int GetEnemyAt(int index)
    {
        if(enemyIdList == null || enemyIdList .Count == 0)
        {
            return -1;
        }
        else
        {
            return enemyIdList[index % enemyIdList.Count];
        }
        
    }
    
}
