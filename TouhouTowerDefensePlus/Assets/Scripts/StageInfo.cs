using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageInfo
{
    public int startGold= 3000, startLife = 10;

    public int waveCount = 0;

    private List<Wave> waves;

    private bool endless = false;


    public void AddWave(Wave wave)
    {
        waves.Add(wave);
        //Debug.Log(waves.Count);
        //waveCount++;
    }

    public Wave GetWave(int i)
    {
        if(!endless)
        {
            return waves[i];
        }
        else
        {
            if (i < 20) return waves[i];
            return CreateRandomWave(i);
        }
        
    }

    /// <summary>
    /// 为无尽模式生成随机关卡
    /// </summary>
    /// <param name="levelIndex"></param>
    /// <returns></returns>
    private Wave CreateRandomWave(int levelIndex)
    {
        
        if (levelIndex > 29 && levelIndex % 10 == 0)
        {
            return CreateBossWave(levelIndex );
        }

        Wave w = null;

        //第二十一波到第五十波提升敌人数量，之后只提高属性。最高提升为三倍
        float countMultiplier;
        if (levelIndex > 50) countMultiplier = 3;
        else countMultiplier = 1f + ((float)(levelIndex-20) / 30f * 2f);

        //二十波之后缩短间隔时间，100波到最短（0.3倍）
        float intervalMultiplier;
        if (levelIndex < 20) intervalMultiplier = 1;
        else if (levelIndex > 100) intervalMultiplier = 0.3f;
        else
        {
            intervalMultiplier = 1 - ((float)(levelIndex - 20) / 80f) * 0.3f;
        }

        //随机抽取一种敌人排列组合。根据当前波次，以一定概率抽取简单/困难波

        
        float f_hard;
        if (levelIndex < 20) f_hard = 20;
        else if (levelIndex > 50) f_hard = 90;
        else f_hard = (float)levelIndex * 7f / 3f - 80f / 3f;

        bool hard = Random.Range(1, 101) < f_hard;

        EnemyCombination ec;
        if (hard)
        {
            int i = Random.Range(0, enemyCombination_hard.Count);
            ec = enemyCombination_hard[i];
        }
        else
        {
            int i = Random.Range(0, enemyCombination.Count);
            ec = enemyCombination[i];
        }
        

        int gold = 0;
        if (levelIndex < 20) gold = 500 - 20 * levelIndex;
        else gold = 100;

        float time = ec.interval * ec.baseCount * countMultiplier;
        if (time < 10) time = 10;
        time += countMultiplier * 5;

        //boss波前时间较长
        if (levelIndex % 10 == 9) time += 10;

        int count =(int)( (float)ec.baseCount * countMultiplier) ;

        float interval = ec.interval;


        //将基础数量、间隔乘以倍数
        //w = new Wave(/*ec.id*/new int[] { EnemyInfo.ENEMY_CIRNO, 1 } ,/*(int)(ec.baseCount * countMultiplier)*/5 , time , ec.interval * ec.baseCount* countMultiplier ,gold);
        w = new Wave(ec.id,count, interval,time,gold) ;
        waves.Add(w);
        return w;
    }

    private Wave CreateBossWave(int levelIndex)
    {
        float interval = 60 + 10*(levelIndex / 10 - 3);
        float time = interval;

        int ecIndex = Random.Range(0, enemyCombination_boss.Count);
        EnemyCombination ec = enemyCombination_boss[ecIndex];

        int gold = 300 * (levelIndex / 10 - 2);
        if (gold < 300) gold = 300;

        Wave w;
        w = new Wave(ec.id, 1, interval, time, gold);
        return w;
    }

    private static List<EnemyCombination> enemyCombination = new List<EnemyCombination>() {

        //单一敌人
        new EnemyCombination (40, new int[]{EnemyInfo .ENEMY_MAOYU,1 },0.4f),
        
        new EnemyCombination (20, new int[]{EnemyInfo .ENEMY_KAGUYA,1 },1.2f),

        new EnemyCombination (10, new int[]{EnemyInfo .ENEMY_YUUGI,1 },2f),
        new EnemyCombination (20, new int[]{EnemyInfo .ENEMY_RIN,1 },0.8f),

        new EnemyCombination (35, new int[]{EnemyInfo .ENEMY_CIRNO,1 },0.6f),
        new EnemyCombination (30, new int[]{EnemyInfo .ENEMY_RUMIA,1 },0.6f),
        new EnemyCombination (25, new int[]{EnemyInfo .ENEMY_MEILIN,1 },0.6f),
        new EnemyCombination (25, new int[]{EnemyInfo .ENEMY_PATCHOULI,1 },1f),
        new EnemyCombination (25, new int[]{EnemyInfo .ENEMY_SAKUYA,1 },0.6f),
        new EnemyCombination (10, new int[]{EnemyInfo .ENEMY_REMILIA,1 },2f),
        new EnemyCombination (10, new int[]{EnemyInfo .ENEMY_FLANDRE,1 },2f),

        new EnemyCombination (30, new int[]{EnemyInfo .ENEMY_CHEN,1 },0.6f),
        new EnemyCombination (25, new int[]{EnemyInfo .ENEMY_ALICE,1 },1f),
        new EnemyCombination (25, new int[]{EnemyInfo .ENEMY_YOUMU,1 },1f),
        new EnemyCombination (10, new int[]{EnemyInfo .ENEMY_YUYUKO,1 },2f),

        new EnemyCombination (25, new int[]{EnemyInfo .ENEMY_AYA,1 },1f),
        new EnemyCombination (22, new int[]{EnemyInfo .ENEMY_SANAE,1 },1f),
        new EnemyCombination (10, new int[]{EnemyInfo .ENEMY_SUWAKO,1 },2f),
        new EnemyCombination (10, new int[]{EnemyInfo .ENEMY_KANAKO,1 },2f),



        //多种敌人的简单组合
        new EnemyCombination (25, new int[]{EnemyInfo .ENEMY_MEILIN,1, EnemyInfo .ENEMY_SAKUYA,1  },1f),
        new EnemyCombination (35, new int[]{EnemyInfo .ENEMY_CIRNO,2, EnemyInfo .ENEMY_RUMIA,2  },0.8f),
        //new EnemyCombination (20, new int[]{EnemyInfo .ENEMY_PATCHOULI,1, EnemyInfo .ENEMY_YUUGI,1  },2f),
        new EnemyCombination (10, new int[]{EnemyInfo .ENEMY_REMILIA,1,EnemyInfo .ENEMY_FLANDRE,1 },1.5f),
        //new EnemyCombination (15, new int[]{EnemyInfo .ENEMY_YUUGI,1,EnemyInfo .ENEMY_AYA,5 },1f),
        new EnemyCombination (15, new int[]{EnemyInfo .ENEMY_PATCHOULI,1,EnemyInfo .ENEMY_REMILIA,2 },1f),

        //new EnemyCombination (25, new int[]{EnemyInfo .ENEMY_RUMIA,5,EnemyInfo .ENEMY_RIN,1 },1f),
        //new EnemyCombination (20, new int[]{EnemyInfo .ENEMY_MEILIN,4,EnemyInfo .ENEMY_RIN,1 },1.5f),
        //new EnemyCombination (20, new int[]{EnemyInfo .ENEMY_PATCHOULI,3,EnemyInfo .ENEMY_RIN,1 },1.5f),

        new EnemyCombination (20, new int[]{ EnemyInfo.ENEMY_SAKUYA, 1, EnemyInfo.ENEMY_MEILIN, 1, EnemyInfo.ENEMY_PATCHOULI, 1, EnemyInfo .ENEMY_REMILIA,1,EnemyInfo .ENEMY_FLANDRE,1 },1.2f),
        new EnemyCombination (20, new int[]{ EnemyInfo.ENEMY_SANAE, 1, EnemyInfo.ENEMY_AYA, 1, EnemyInfo.ENEMY_SUWAKO, 1, EnemyInfo .ENEMY_KANAKO,1},1.2f),
        new EnemyCombination (20, new int[]{ EnemyInfo.ENEMY_CHEN, 1, EnemyInfo.ENEMY_ALICE, 1, EnemyInfo.ENEMY_YOUMU, 1, EnemyInfo .ENEMY_YUYUKO,1},1.2f),

        new EnemyCombination (15, new int[]{EnemyInfo .ENEMY_YOUMU,1, EnemyInfo .ENEMY_YUYUKO,1  },1f),
        new EnemyCombination (10, new int[]{EnemyInfo .ENEMY_SUWAKO,1, EnemyInfo .ENEMY_KANAKO,1  },2f),
        

    };

    private static List<EnemyCombination> enemyCombination_hard = new List<EnemyCombination>()
    {
        new EnemyCombination (30, new int[]{EnemyInfo .ENEMY_AYA,1 },0.6f),
        new EnemyCombination (15, new int[]{EnemyInfo .ENEMY_FLANDRE,1 },0.6f),

        new EnemyCombination (16, new int[]{EnemyInfo .ENEMY_YOUMU,3, EnemyInfo .ENEMY_AYA,5  },1.2f),
        new EnemyCombination (15, new int[]{EnemyInfo .ENEMY_SUWAKO,1, EnemyInfo .ENEMY_KANAKO,1  },1f),
        new EnemyCombination (20, new int[]{EnemyInfo .ENEMY_YOUMU,3, EnemyInfo .ENEMY_YUYUKO,1  },1f),
        

        //new EnemyCombination (18, new int[]{EnemyInfo .ENEMY_SANAE,3,EnemyInfo .ENEMY_KAGUYA,1 },1.5f),

        //治疗类
        new EnemyCombination (12, new int[]{EnemyInfo .ENEMY_SANAE,2,EnemyInfo .ENEMY_YUUGI,1 },1.5f),
        new EnemyCombination (12, new int[]{EnemyInfo .ENEMY_SANAE,2,EnemyInfo .ENEMY_FLANDRE,2 },0.8f),
        new EnemyCombination (15, new int[]{EnemyInfo .ENEMY_SANAE,2,EnemyInfo .ENEMY_SUWAKO,1 },0.8f),
        new EnemyCombination (15, new int[]{EnemyInfo .ENEMY_SANAE,2,EnemyInfo .ENEMY_KANAKO,1 },0.8f),
        
        new EnemyCombination (20, new int[]{EnemyInfo .ENEMY_YUUGI,10,EnemyInfo .ENEMY_AYA,10 },0.8f),
        new EnemyCombination (30, new int[]{EnemyInfo .ENEMY_MEILIN,2, EnemyInfo .ENEMY_SAKUYA,2  },0.6f),
        new EnemyCombination (12, new int[]{EnemyInfo .ENEMY_FLANDRE,1, EnemyInfo .ENEMY_REMILIA,1  },0.8f),
        new EnemyCombination (25, new int[]{EnemyInfo .ENEMY_ALICE,2, EnemyInfo .ENEMY_SANAE,5  },0.8f),
        new EnemyCombination (15, new int[]{EnemyInfo .ENEMY_REMILIA,3, EnemyInfo .ENEMY_RIN,1  },0.8f),
        new EnemyCombination (12, new int[]{EnemyInfo .ENEMY_YUUGI,4, EnemyInfo .ENEMY_RIN,1  },1f),
        new EnemyCombination (20, new int[]{EnemyInfo .ENEMY_SANAE,1, EnemyInfo .ENEMY_RIN,1  },0.8f),
        new EnemyCombination (16, new int[]{EnemyInfo .ENEMY_ALICE,3, EnemyInfo .ENEMY_FLANDRE,5  },1.2f),
        new EnemyCombination (20, new int[]{EnemyInfo .ENEMY_ALICE,1, EnemyInfo .ENEMY_PATCHOULI,1  },1.2f),

        new EnemyCombination (20, new int[]{EnemyInfo .ENEMY_SANAE,1, EnemyInfo .ENEMY_RIN,1 , EnemyInfo .ENEMY_ALICE,1  },0.8f),
        new EnemyCombination (15, new int[]{EnemyInfo .ENEMY_KAGUYA,3, EnemyInfo .ENEMY_RIN,1 , EnemyInfo .ENEMY_SANAE,1  },1f),


    };

    private static List<EnemyCombination> enemyCombination_boss = new List<EnemyCombination>()
    {
        new EnemyCombination (1, new int[]{EnemyInfo .ENEMY_SHIKIEKI,1 },45f),
        new EnemyCombination (1, new int[]{EnemyInfo .ENEMY_SUIKA,1 },45f),
        new EnemyCombination (1, new int[]{EnemyInfo .ENEMY_YUKARI,1 },45f),
        new EnemyCombination (1, new int[]{EnemyInfo .ENEMY_EIRIN,1 },45f),
        new EnemyCombination (1, new int[]{EnemyInfo .ENEMY_YUUKA,1 },45f),
    };

    /// <summary>
    /// 内部类：敌人的排列组合
    /// </summary>
    private class EnemyCombination
    {
        public int baseCount;
        public int[] id;
        public float interval;

        public EnemyCombination (int c, int[] i, float t)
        {
            this.baseCount = c;
            this.id = i;
            this.interval = t;
        }
    }

    /// <summary>
    /// 加载对应关卡的数据
    /// </summary>
    /// <param name="level"></param>
    public StageInfo (int level)
    {
        waves = new List<Wave>();

        switch (level)
        {
            case 0://人间之里
                startGold = 3000;
                AddWave(new Wave(EnemyInfo.ENEMY_MAOYU, 10, 1.2f, 20f, 500));
                AddWave(new Wave(EnemyInfo.ENEMY_MAOYU, 15, 1f, 25f, 500));
                AddWave(new Wave(EnemyInfo.ENEMY_CIRNO, 10, 1f, 15f, 500));
                AddWave(new Wave(EnemyInfo.ENEMY_RUMIA, 10, 0.8f, 15f, 500));
                AddWave(new Wave(EnemyInfo.ENEMY_CHEN, 12, 0.8f, 20f, 500));

                AddWave(new Wave(EnemyInfo.ENEMY_MAOYU, 20, 0.6f, 20f, 500));
                AddWave(new Wave(EnemyInfo.ENEMY_MEILIN, 8, 1.5f, 20f, 500));
                AddWave(new Wave(EnemyInfo.ENEMY_SAKUYA, 8, 1.5f, 20f, 500));
                AddWave(new Wave(EnemyInfo.ENEMY_CIRNO, 20, 0.6f, 20f, 250));
                AddWave(new Wave(EnemyInfo.ENEMY_RUMIA, 20, 0.8f, 20f, 250));

                AddWave(new Wave(EnemyInfo.ENEMY_ALICE, 15, 1.2f, 30f, 100));
                AddWave(new Wave(EnemyInfo.ENEMY_CHEN, 40, 0.4f, 25f, 100));
                AddWave(new Wave(EnemyInfo.ENEMY_SANAE, 15, 1.5f, 30f, 100));
                AddWave(new Wave(EnemyInfo.ENEMY_AYA, 20, 1f, 30f, 100));
                AddWave(new Wave(EnemyInfo.ENEMY_REMILIA, 12, 2f, 35f, 100));

                AddWave(new Wave(new int[] { EnemyInfo.ENEMY_MEILIN, 1, EnemyInfo.ENEMY_SAKUYA, 1 }, 30, 0.8f, 35f, 100));
                AddWave(new Wave(new int[] { EnemyInfo.ENEMY_CIRNO, 1, EnemyInfo.ENEMY_RUMIA, 1 }, 60, 0.4f, 35f, 100));
                AddWave(new Wave(new int[] { EnemyInfo.ENEMY_PATCHOULI, 5, EnemyInfo.ENEMY_SANAE, 2 }, 25, 1.5f, 45f, 100));
                AddWave(new Wave(new int[] { EnemyInfo.ENEMY_REMILIA, 5, EnemyInfo.ENEMY_AYA , 5 }, 20, 1f, 40f, 100));
                AddWave(new Wave(EnemyInfo.ENEMY_FLANDRE, 20, 1.5f, 9999f, 0));
                break;
            case 1://雾之湖
                startGold = 4000;
                //AddWave(new Wave(EnemyInfo.ENEMY_MAOYU, 10, 1.2f, 20f, 500));
                //AddWave(new Wave(EnemyInfo.ENEMY_MAOYU, 15, 1f, 20f, 500));
                AddWave(new Wave(EnemyInfo.ENEMY_CIRNO, 10, 1.5f, 25f, 500));
                AddWave(new Wave(EnemyInfo.ENEMY_RUMIA, 10, 1.5f, 25f, 500));
                AddWave(new Wave(EnemyInfo.ENEMY_CHEN, 10, 1f, 20f, 500));
                AddWave(new Wave(EnemyInfo.ENEMY_MEILIN, 6, 2f, 20f, 500));
                AddWave(new Wave(EnemyInfo.ENEMY_SAKUYA, 6, 2f, 20f, 500));

                AddWave(new Wave(EnemyInfo.ENEMY_CIRNO, 15, 0.8f, 20f, 250));
                AddWave(new Wave(EnemyInfo.ENEMY_RUMIA, 12, 1f, 20f, 250));
                AddWave(new Wave(EnemyInfo.ENEMY_ALICE, 10, 1f, 20f, 250));
                AddWave(new Wave(EnemyInfo.ENEMY_AYA, 12, 1f, 20f, 250));
                AddWave(new Wave(EnemyInfo.ENEMY_SANAE, 12, 1f, 20f, 250));

                AddWave(new Wave(EnemyInfo.ENEMY_KAGUYA, 5, 2f, 30f, 100));
                AddWave(new Wave(EnemyInfo.ENEMY_PATCHOULI, 10, 1f, 25f, 100));
                AddWave(new Wave(new int[] { EnemyInfo.ENEMY_MEILIN, 1, EnemyInfo.ENEMY_SAKUYA, 1 }, 18, 0.8f, 30f, 100));
                AddWave(new Wave(new int[] { EnemyInfo.ENEMY_CIRNO, 1, EnemyInfo.ENEMY_RUMIA, 1 }, 30, 0.8f, 34f, 100));
                AddWave(new Wave(new int[] { EnemyInfo.ENEMY_SANAE, 2, EnemyInfo.ENEMY_CHEN, 5 }, 28, 0.8f, 30f, 100));

                AddWave(new Wave(EnemyInfo.ENEMY_YUUGI, 5, 2f, 25f, 100));
                AddWave(new Wave(EnemyInfo.ENEMY_REMILIA, 12, 1.5f, 25f, 100));
                AddWave(new Wave(EnemyInfo.ENEMY_FLANDRE, 10, 1.5f, 20f, 100));
                AddWave(new Wave(new int[] { EnemyInfo.ENEMY_PATCHOULI, 1, EnemyInfo.ENEMY_YUUGI, 1 }, 15, 2f, 50f, 100));
                AddWave(new Wave(new int[] { EnemyInfo.ENEMY_ALICE, 2, EnemyInfo.ENEMY_RIN, 1 }, 20, 1.2f, 9999f, 0));

                break;
            case 2://旧地狱
                startGold = 3500;
                AddWave(new Wave(EnemyInfo.ENEMY_MAOYU, 10, 1.2f, 20f, 500));
                AddWave(new Wave(new int[] { EnemyInfo.ENEMY_CIRNO, 1, EnemyInfo.ENEMY_RUMIA, 1 }, 10, 1.5f, 25f, 100));
                AddWave(new Wave(EnemyInfo.ENEMY_MAOYU, 15, 1f, 25f, 500));
                AddWave(new Wave(EnemyInfo.ENEMY_CHEN, 12, 0.8f, 20f, 500));
                AddWave(new Wave(EnemyInfo.ENEMY_RIN, 9, 1f, 20f, 500));

                AddWave(new Wave(EnemyInfo.ENEMY_MAOYU, 25, 0.8f, 25f, 500));
                AddWave(new Wave(EnemyInfo.ENEMY_MEILIN, 8, 1.5f, 20f, 500));
                AddWave(new Wave(EnemyInfo.ENEMY_SAKUYA, 8, 1.5f, 20f, 250));
                AddWave(new Wave(new int[] { EnemyInfo.ENEMY_RUMIA, 1, EnemyInfo.ENEMY_RIN, 1 }, 10, 1.2f, 30f, 250));
                AddWave(new Wave(EnemyInfo.ENEMY_YUUGI, 2, 4f, 20f, 250));

                AddWave(new Wave(EnemyInfo.ENEMY_ALICE, 10, 1.2f, 25f, 100));
                AddWave(new Wave(EnemyInfo.ENEMY_CHEN, 20, 0.8f, 25f, 100));
                AddWave(new Wave(EnemyInfo.ENEMY_SANAE, 12, 1.5f, 30f, 100));
                AddWave(new Wave(EnemyInfo.ENEMY_AYA, 15, 1f, 25f, 100));
                AddWave(new Wave(EnemyInfo.ENEMY_REMILIA, 8, 2f, 25f, 100));

                AddWave(new Wave(new int[] { EnemyInfo.ENEMY_SANAE, 2, EnemyInfo.ENEMY_RIN, 1 }, 16,1.2f, 40f, 100));
                AddWave(new Wave(new int[] { EnemyInfo.ENEMY_CIRNO, 1, EnemyInfo.ENEMY_RUMIA, 1 }, 40, 0.5f, 30f, 100));
                AddWave(new Wave(new int[] { EnemyInfo.ENEMY_PATCHOULI, 5, EnemyInfo.ENEMY_YUUGI, 2 }, 15, 2f, 45f, 100));
                AddWave(new Wave(new int[] { EnemyInfo.ENEMY_REMILIA,1, EnemyInfo.ENEMY_FLANDRE, 1 }, 12, 2f, 45f, 100));
                AddWave(new Wave(new int[] { EnemyInfo.ENEMY_ALICE, 2, EnemyInfo.ENEMY_SANAE, 2, EnemyInfo.ENEMY_RIN, 1 }, 30, 1.5f, 999f, 0));
                break;
            case 3://无缘冢
                startGold = 3000;
                AddWave(new Wave(EnemyInfo.ENEMY_MAOYU, 10, 1.2f, 20f, 500));
                AddWave(new Wave(EnemyInfo.ENEMY_MAOYU, 15, 1f, 25f, 500));
                AddWave(new Wave(EnemyInfo.ENEMY_CIRNO, 10, 1f, 15f, 500));
                AddWave(new Wave(EnemyInfo.ENEMY_RUMIA, 10, 0.8f, 15f, 500));
                AddWave(new Wave(EnemyInfo.ENEMY_CHEN, 12, 0.8f, 20f, 500));

                AddWave(new Wave(EnemyInfo.ENEMY_AYA, 10, 1f, 25f, 500));
                AddWave(new Wave(EnemyInfo.ENEMY_MEILIN, 12, 1.5f, 25f, 500));
                AddWave(new Wave(EnemyInfo.ENEMY_SAKUYA, 12, 1.5f, 25f, 250));
                AddWave(new Wave(EnemyInfo.ENEMY_SANAE, 12, 1f, 20f, 250));
                AddWave(new Wave(EnemyInfo.ENEMY_KAGUYA, 5, 3f, 25f, 250));

                AddWave(new Wave(EnemyInfo.ENEMY_ALICE, 15, 1.2f, 30f, 100));
                AddWave(new Wave(EnemyInfo.ENEMY_RIN, 20, 1.2f, 35f, 100));
                AddWave(new Wave(EnemyInfo.ENEMY_PATCHOULI, 18, 1.5f, 35f, 100));
                AddWave(new Wave(EnemyInfo.ENEMY_YUUGI, 10, 3f, 45f, 100));
                AddWave(new Wave(EnemyInfo.ENEMY_REMILIA, 12, 2f, 35f, 100));

                AddWave(new Wave(new int[] { EnemyInfo.ENEMY_MEILIN, 1, EnemyInfo.ENEMY_SAKUYA, 1 }, 25, 0.8f, 35f, 100));
                AddWave(new Wave(new int[] { EnemyInfo.ENEMY_KAGUYA, 1, EnemyInfo.ENEMY_SANAE, 2 }, 20, 1f, 40f, 100));
                AddWave(new Wave(new int[] { EnemyInfo.ENEMY_ALICE, 5, EnemyInfo.ENEMY_YUUGI, 2 }, 20, 1.5f, 45f, 100));
                AddWave(new Wave(new int[] { EnemyInfo.ENEMY_RIN, 1, EnemyInfo.ENEMY_AYA, 5 }, 25, 1f, 40f, 100));
                AddWave(new Wave(new int[] { EnemyInfo.ENEMY_KAGUYA, 2, EnemyInfo.ENEMY_SANAE, 2, EnemyInfo.ENEMY_ALICE, 1 }, 30, 1f, 999f, 0));

                break;
        }
        
        waveCount = waves.Count ;
        //Debug.Log("wave count = " + waveCount + "," + waves.Count);
    }

    /// <summary>
    /// 无尽模式的数据
    /// </summary>
    public StageInfo()
    {
        endless = true;
        startGold = 5000;
        startLife = 20;

        waves = new List<Wave>();
        //前二十波固定
        AddWave(new Wave(EnemyInfo.ENEMY_MAOYU, 10, 1.2f, 25f, 500));
        AddWave(new Wave(EnemyInfo.ENEMY_CIRNO, 8, 2f, 25f, 500));
        AddWave(new Wave(EnemyInfo.ENEMY_RUMIA, 8, 2f, 20f, 500));
        AddWave(new Wave(EnemyInfo.ENEMY_CHEN, 10, 1.5f, 25f, 500));
        AddWave(new Wave(EnemyInfo.ENEMY_MEILIN, 8, 1.5f, 20f, 500));
        AddWave(new Wave(EnemyInfo.ENEMY_SAKUYA, 8, 1.5f, 20f, 500));
        AddWave(new Wave(EnemyInfo.ENEMY_AYA, 8, 1.5f, 20f, 500));
        AddWave(new Wave(EnemyInfo.ENEMY_SANAE, 10, 1.5f, 25f, 500));
        AddWave(new Wave(EnemyInfo.ENEMY_ALICE, 10, 2f, 30f, 500));
        AddWave(new Wave(EnemyInfo.ENEMY_KAGUYA, 8, 2f, 30f, 500));

        AddWave(new Wave(EnemyInfo.ENEMY_YOUMU, 12, 1f, 25f, 200));
        AddWave(new Wave(EnemyInfo.ENEMY_RIN, 12, 1f, 25f, 200));
        AddWave(new Wave(EnemyInfo.ENEMY_YUYUKO, 5, 2f, 20f, 200));
        AddWave(new Wave(EnemyInfo.ENEMY_SUWAKO, 6, 2f, 20f, 200));
        AddWave(new Wave(EnemyInfo.ENEMY_KANAKO, 7, 2f, 25f, 200));
        AddWave(new Wave(EnemyInfo.ENEMY_YUUGI, 8, 2f, 25f, 150));
        AddWave(new Wave(EnemyInfo.ENEMY_REMILIA, 8, 2f, 25f, 150));
        AddWave(new Wave(EnemyInfo.ENEMY_FLANDRE, 9, 2f, 25f, 150));
        AddWave(new Wave(EnemyInfo.ENEMY_ALICE, 16, 1f, 30f, 150));
        AddWave(new Wave(EnemyInfo.ENEMY_YUUKA, 1, 2f, 45f, 150));
    }

    public static StageInfo GetStageInfo(int level)
    {
        StageInfo info=null;



        return info;
    }


}
