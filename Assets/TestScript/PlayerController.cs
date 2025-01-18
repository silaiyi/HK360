using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerController : MonoBehaviour
{
    public int playerPosition = 0; // 玩家当前的位置
    public int diceValue; // 骰子点数
    public bool isPlayerTurn = true; // 是否是玩家的回合
    public Text txt;
    public Land[] lands;

    public void RollDice()
    {
        diceValue = Random.Range(1, 7); // 生成1至6的随机数
        StartCoroutine(MovePlayer()); 
        txt.text="Step: "+diceValue;

    }

    public IEnumerator MovePlayer()
    {
    Land currentLand = lands[playerPosition];
    for (int i = 0; i < diceValue; i++)
    {
        playerPosition = (playerPosition + 1) % 25; // 5x5棋盘共有25个格子，使用模运算来循环
        Vector3 newPosition = GameObject.Find("Land" + playerPosition).transform.position;
        while (transform.position != newPosition)
        {
            transform.position = Vector3.MoveTowards(transform.position, newPosition, 1f * Time.deltaTime);
            yield return null;
        }
        //playerPosition = Array.IndexOf(lands, currentLand);
    }
    }

    public void EndTurn()
    {
        isPlayerTurn = false;
        // 触发AI的回合
    }
}

