using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSkybox : MonoBehaviour
{
    public Material[] skyboxes; // 存储所有的天空盒材质
    private int currentSkyboxIndex = 0; // 当前使用的天空盒索引

    void Start()
    {
        InvokeRepeating("ChangeSkybox", 30f, 30f); // 每隔30秒调用ChangeSkybox函数
    }

    void ChangeSkybox()
    {
        currentSkyboxIndex = (currentSkyboxIndex + 1) % skyboxes.Length; // 循环切换天空盒
        RenderSettings.skybox = skyboxes[currentSkyboxIndex]; // 更新当前的天空盒材质
    }
}