using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StarterGiveMoney : MonoBehaviour
{
    public static int wavecount=0;
    //public GameObject winmenu;
    // Start is called before the first frame update
    void Start()
    {
        wavecount=0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider other)
    {
        // 检查碰撞的物体是否是玩家
        if (other.CompareTag("Player"))
        {
            PlayerToken.exMoney+=200;
            //wavecount++;
        }
        if (other.CompareTag("AI"))
        {
            AIToken.exMoney+=200;
            wavecount++;
        }
    }

}
