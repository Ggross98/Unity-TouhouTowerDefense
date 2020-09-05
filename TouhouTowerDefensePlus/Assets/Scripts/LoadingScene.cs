using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScene : MonoBehaviour
{
    public Image progressBar;

    private AsyncOperation operation;

    private int curProgressValue = 0;


    void Start()
    {
        StartCoroutine(LoadAsyncScene());
    }




    private IEnumerator LoadAsyncScene()
    {

        operation = SceneManager.LoadSceneAsync("Game");
        //阻止当加载完成自动切换
        operation.allowSceneActivation = false;

        yield return null;
    }


    void Update()
    {
        float value = 0f;

        if (operation  != null)//如果已经开始加载
        {
            value = operation .progress; //获取加载进度,此处特别注意:加载场景的progress值最大为0.9!!!
        }
        else
        {
            return;
        }
        if (value >= 0.9f)//因为progress值最大为0.9,所以我们需要强制将其等于1
        {
            value = 1;
        }
        
        
        if(curProgressValue  < (int)(100 * value))
        {
            curProgressValue++;
        }


        //loadingText.text = curProgressValue + "%";//实时更新进度百分比的文本显示  

        progressBar.fillAmount = curProgressValue /100f;//实时更新滑动进度图片的fillAmount值  

        if (curProgressValue >=99)
        {

            GameObject playerInfo = GameObject.Find("PlayerInfo");
            if(playerInfo != null)
            {
                DontDestroyOnLoad(playerInfo);
            }


            operation.allowSceneActivation = true;//启用自动加载场景  
            //loadingText.text = "OK";//文本显示完成OK  

        }
    }



}
