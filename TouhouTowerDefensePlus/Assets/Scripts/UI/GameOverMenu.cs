using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverMenu : MonoBehaviour
{
    StageController stage;
    
    [SerializeField]
    Button exit, restart;

    void Awake()
    {
        stage = GameObject.Find("Map").GetComponent<StageController>();

        exit.onClick.AddListener(

            delegate {
                stage.Exit();

            }

            );

        restart.onClick.AddListener(

           delegate {
               stage.Restart();
           }

           );

        gameObject.SetActive(false);
    }
}
