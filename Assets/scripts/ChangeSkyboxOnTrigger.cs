using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeSkyboxOnTrigger : MonoBehaviour
{

    public Material specificSkybox; // 指定的天空盒材质

    void OnTriggerEnter(Collider other)
    {
        // 检查碰撞的物体是否是玩家
        if (other.CompareTag("Player"))
        {
            // 切换到指定的天空盒
            RenderSettings.skybox = specificSkybox;
        }
    }
}

