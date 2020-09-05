using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StageController : MonoBehaviour
{

    private EnemyController enemyController;
    public StageInfo stageInfo;
    private MapController mapController;

    //是否游戏中
    public bool playing = false;
    //游戏状态：游戏中，未开始，胜利，失败
    private int gameState = 0;
    public const int STATE_WIN=2, STATE_LOSE = 3;

    //是否暂停
    public bool pausing = false;

    //速度
    public int gameSpeed = 1;
    //rank：影响怪物的强度
    public int rank = 0;

    //每一波的倒计时
    private float waveCountdown = 0f;
    //当前是第几波
    private int currentWave = -1;
    private Wave currentWaveInfo;
    //最多同时释放多少波
    private static int MAX_WAVES = 4;
    private Queue<Wave> wave_waiting = new Queue<Wave> ();
    private List<Wave> wave_releasing = new List<Wave> ();

    //游戏中数据
    public int life;
    public int gold;
    //UI
    public UpUIController upUI;
    public GameObject pauseMenu, overMenu;
    public SelectButton pauseButton, speedButton;

    //是否为无尽模式
    public bool endless;

    // Start is called before the first frame update/
    void Start()
    {
        enemyController = gameObject.GetComponent<EnemyController>();
        mapController = gameObject.GetComponent<MapController>();

        upUI = GameObject.Find("UpUI").GetComponent<UpUIController>();

        if (stageInfo == null)
        {
            int level = 0;
            
            if(PlayerInfo.instance  != null)
            {
                level = PlayerInfo.instance.level;
                this.endless = PlayerInfo.instance.endless;
            }

            if (endless) this.stageInfo = new StageInfo();
            else this.stageInfo = new StageInfo(level);
        }

        if(PlayerInfo .instance != null)
        {
            GameObject.Find("HeadFrameButton").GetComponent<Image>().sprite = Utils.GetAssistRoleInfo(PlayerInfo.instance.assist).image ;
        }



        life = this.stageInfo.startLife;
        gold = this.stageInfo.startGold;

        upUI.headFrame.onClick.AddListener(

            delegate () {

                if (playing) StartNewWave();
                else StartGame();

            }

            );

        upUI.ShowGold(gold);
        upUI.ShowLife(life);

        pauseButton = GameObject.Find("PauseButton").GetComponent<SelectButton>();
        speedButton = GameObject.Find("SpeedButton").GetComponent<SelectButton>();

        pauseButton.button.onClick.AddListener(
            
            delegate {
                ClickPauseButton();
                
            }
            );
        pauseButton.SetSelected(false);
        speedButton.button.onClick.AddListener(

            delegate {
                ClickSpeedButton();
            }
            );
        speedButton.SetSelected(false);


        AudioManager.instance.PlayMusic("bgm01");

        //playing = true;
    }

    public void StartGame()
    {
        playing = true;
        pausing = false;
    }



    // Update is called once per frame
    void Update()
    {
        upUI.ShowGold(gold);
        upUI.ShowLife(life);

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!pausing)
            {
                //ClickPauseButton();
                Pause();
                ShowPauseMenu(true);
                pauseButton.SetSelected(true);
            }
            else
            {
                Resume();
                ShowPauseMenu(false);
                pauseButton.SetSelected(false);
            }
            return;
        }

        if(Input.GetKeyDown (KeyCode.Tab))
        {
            ClickSpeedButton();
        }

        if (playing)
        {

            #region UI控制
            //upUI.ShowGold(gold);
            //upUI.ShowLife(life);

            if (endless) upUI.ShowWave(currentWave);
            else upUI.ShowWave(currentWave, stageInfo.waveCount);
            //upUI.ShowLeftTime();
            if(currentWave >= 0)
            {
                upUI.ShowLeftTime(waveCountdown, currentWaveInfo .waveTime);
            }
            #endregion



            gameState = CheckWin();

            if (gameState == 1)
            {
                Win();
            }
            else if(gameState == -1)
            {
                Lose();
            }
            else
            {
                if(waveCountdown <=0) //倒计时结束
                {
                    if(currentWave == stageInfo.waveCount - 1)//是最后一波
                    {
                        if (endless) StartNewWave();
                    }
                    else //否则开始新的一波
                    {
                        StartNewWave();
                    }
                }
                else //继续倒计时
                {
                    waveCountdown -= Time.deltaTime;
                }


                //如果正在放的波次小于最大值，把等待队列中的波次释放
                if(wave_releasing .Count <MAX_WAVES)
                {
                    if(wave_waiting .Count > 0)
                    {
                        Wave w = wave_waiting.Dequeue();
                        wave_releasing.Add(w);

                        StartCoroutine(CreateEnemies(w));
                    }
                    
                }


            }
        }
    }

    public void ClickPauseButton()
    {
        Debug.Log("pausing : " + pausing);
        if (pauseButton .selected )
        {
            Resume();
            //ShowPauseMenu(false);
            pauseButton.SetSelected(false);
        }
        else
        {
            Pause();
            //ShowPauseMenu(true);
            pauseButton.SetSelected(true);
        }
    }

    public void ClickSpeedButton()
    {
        if(speedButton .selected)
        {
            speedButton.SetSelected(false);
            SetGameSpeed(1);
        }
        else
        {
            speedButton.SetSelected(true);
            SetGameSpeed(2);
        }

    }

    private void SetGameSpeed(int i)
    {
        gameSpeed = i;
        if (!pausing)
        {
            Time.timeScale = gameSpeed;
        }
    }

    public void Win() {

        playing = false;

        pauseMenu.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
        //GameObject.Find("MenuTitle").GetComponent<Text>().text = "游戏胜利！";
        /*
        pauseMenu.SetActive(true);
        GameObject.Find("MenuTitle").GetComponent<Text>().text = "游戏胜利！";*/
        ShowOverMenu(STATE_WIN);

    }

    public void Lose() {

        playing = false;
        Time.timeScale = 0;
        pauseMenu.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
        //GameObject.Find("MenuTitle").GetComponent<Text>().text = "游戏结束！你存活了"+currentWave +"轮";
        /*
        pauseMenu.SetActive(true);
        GameObject.Find("MenuTitle").GetComponent<Text>().text = "游戏结束！你存活了" + currentWave + "轮";*/
        ShowOverMenu(STATE_LOSE);
    }


    private void Pause()
    {
        pausing = true;
        Time.timeScale = 0;

        /*
        pauseMenu.GetComponent<RectTransform>().anchoredPosition = new Vector2(0,0);
        pauseMenu.SetActive(true);
        GameObject.Find("MenuTitle").GetComponent<Text>().text = "Pause" ;*/
    }

    


    public void ShowPauseMenu(bool b)
    {
        if (b)
        {
            pauseMenu.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
            pauseMenu.SetActive(true);
            //GameObject.Find("MenuTitle").GetComponent<Text>().text = "Pause";
            pauseButton.button.interactable = false;
        }
        else
        {
            pauseMenu.GetComponent<RectTransform>().anchoredPosition = new Vector2(3000, 3000);
            pauseMenu.SetActive(false);
            //GameObject.Find("MenuTitle").GetComponent<Text>().text = "Pause";
            pauseButton.button.interactable = true;
        }
    }

    private void ShowOverMenu(int state)
    {
        overMenu.SetActive(true);
        Text overinfo = GameObject.Find("OverInfo").GetComponent<Text>();
        switch (state)
        {
            case STATE_LOSE:
                overinfo.text = "你输了！存活了" + currentWave + "关";
                break;
            case STATE_WIN:
                overinfo.text = "你赢了！";
                break;
            default:
                break;
        }
        overMenu.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
        
    }

    public void Resume()
    {
        pausing = false;
        Time.timeScale = gameSpeed;
        /*
        pauseMenu.GetComponent<RectTransform>().anchoredPosition = new Vector2(3000, 3000);
        pauseMenu.SetActive(false);*/
    }

    public void Exit()
    {
        Time.timeScale = 1;
        if(PlayerInfo .instance != null)
            Destroy(PlayerInfo.instance.gameObject );
        if(AudioManager .instance != null)
        {
            Destroy(AudioManager.instance.gameObject);
        }
        SceneManager.LoadScene("MainMenu");
    }


    public void Restart() {

        //Time.timeScale = 1;
        SetGameSpeed(1);
        Resume();
        SceneManager.LoadScene("Game");

    }

    //开始新的波次，刷怪，开始计时等
    public void StartNewWave()
    {
        

        //已经是最后一波则不开始新的波次
        if (!endless && currentWave >= stageInfo.waveCount - 1) return;

        //结算完成的波次
        CompleteWave();

        //如果是提前释放则增加rank
        if (wave_releasing.Count > 1 && waveCountdown >0) rank++;

        currentWave++;
        //Debug.Log(currentWave + " start");
        this.currentWaveInfo = GetCurrentWaveInfo();
        waveCountdown = currentWaveInfo .waveTime ;

        enemyController.SetWaveAndRank(currentWave,rank);

        wave_waiting.Enqueue(currentWaveInfo);

        
        //StartCoroutine(CreateEnemies(currentWaveInfo ));

    }

    public IEnumerator CreateEnemies(Wave wave)
    {
        int count = 0;
        int id;
        float interval = wave.enemyInterval;

        while (playing && gameState == 0 && count <wave.enemyCount)
        {
            id = wave.GetEnemyAt (count++);
            enemyController.CreateEnemy(id);
            yield return new WaitForSeconds(interval);
        }

        wave_releasing.Remove(wave);


        yield return null;
    }

    //完成当前波次，增加金钱等
    public void CompleteWave()
    {
        if (currentWave >= 0)
        {
            int reward = currentWaveInfo .waveGold;

            //比例x
            float multiplier = waveCountdown/currentWaveInfo .waveTime;
            if (waveCountdown <= 0) multiplier = 0;
            //倍增系数n
            float rate = 1f;
            if (currentWave < 10) rate = 1f;
            else if (currentWave < 20) rate = 2f;
            else if (currentWave < 30) rate = 3f;
            else if (currentWave < 50) rate = 5f;
            else rate = 10f;
            //奖金 = 基础奖金*(n*x^2+1)
            float i = rate * multiplier * multiplier;
            if (PlayerInfo.instance.assist == Utils.ASSIST_REIMU) i *= 2;
            
            reward = (int)((float)reward * (i+1));

            gold += reward;

            TextEffectManager.instance.ShowTextAtPosition("+"+reward, Color.yellow , 30, new Vector3 (-5.5f,6.5f),0.5f);


        }

        //如果正在释放的波数小于等于1，rank归零
        if (wave_releasing.Count <= 1) rank = 0;

    }

    public void EnemyAtHome()
    {
        if (playing)
        {
            life--;

        }
    }


    private Wave GetCurrentWaveInfo()
    {
        return stageInfo.GetWave(currentWave);
    }

    //检测是否赢了或输了
    //游戏中/未开始游戏：0
    //赢了：1 失败：-1
    private int CheckWin()
    {
        if (playing)
        {
            if (life <= 0)
                return -1;

            if(currentWave== stageInfo .waveCount-1)
            {
                if (endless) return 0;
                if(enemyController .enemyList .Count == 0)
                {
                    return 1;
                }
            }
        }
        return 0;
    }

}
