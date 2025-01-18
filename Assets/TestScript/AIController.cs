using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AIController : MonoBehaviour
{
    public int aiPosition = 0; // AI当前的位置
    public int diceValue; // 骰子点数
    public bool isAITurn = false; // 是否是AI的回合
    public Text txt;

    public void RollDice()
    {
        diceValue = Random.Range(1, 7); // 生成1至6的随机数
        StartCoroutine(MoveAI());
        txt.text="Step: "+diceValue;
        
    }

    public IEnumerator MoveAI()
{
    for (int i = 0; i < diceValue; i++)
    {
        aiPosition = (aiPosition + 1) % 25; // 5x5棋盘共有25个格子，使用模运算来循环
        Vector3 newPosition = GameObject.Find("Land" + aiPosition).transform.position;
        while (transform.position != newPosition)
        {
            transform.position = Vector3.MoveTowards(transform.position, newPosition, 1f * Time.deltaTime);
            yield return null;
        }
    }
}

    public void EndTurn()
    {
        isAITurn = false;
        // 触发玩家的回合
    }
}
