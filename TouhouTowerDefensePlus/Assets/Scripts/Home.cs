using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Home : MonoBehaviour
{
    AudioClip warning;
    //public int maxLife = 1, life = 1;
    // Start is called before the first frame update
    void Awake()
    {
        warning = Resources.Load<AudioClip>("SoundEffects/se_warning");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        EnemyScript enemy = collider.gameObject.GetComponent<EnemyScript>();
        if (enemy != null)
        {
            //Debug.Log("enemy at home");
            AudioManager.instance.PlaySound(warning);
            //GameObject.Find("Map").GetComponent<StageController>().EnemyAtHome();
            GameObject.Find("Map").GetComponent<EnemyController>().EnemyAtHome (enemy);

            //Destroy(enemy.gameObject);
        }
    }
}
