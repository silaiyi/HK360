using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerToken : MonoBehaviour
{
    public int Money;
    public static int exMoney,totalMoney;
    public Tile startingTile;
    //public DiceManager diceManager;
    public Text valueText, MoneyText;
    public GameManager gameManager;

    public Tile currentTile;
    public static int OwnedLands;
    Tile[] moveQueue;
    int moveQueueIndex, spacesToMove;
    Vector3 targetPosition, velocity;
    public Tile finalTile;
    public int seeMove;
    private float moveSpeed = 1f;
    private int currentStep = 0;
    public bool isMoving = false;
    public bool isInJail = false; // 玩家是否在监狱中
    public int turnsInJail = 0; // 玩家在监狱中的回合数
    public static bool CanBeInJail = true,CanGetChange = true,CanPlayGane=true,canBuyLand = true,canLoseMoney=true;
    public int seeMoney;
    //public int requiredTurnsInJail = 2; // 玩家需要在监狱中停留的回合数
    public void InitializePlayerToken(int startingMoney)
    {
        Money = startingMoney;
        finalTile = startingTile;
        targetPosition = startingTile.transform.position;
        OwnedLands = 0;
        currentTile = startingTile;
    }
    void Update()
    {
        totalMoney=Money+exMoney;
        seeMoney=totalMoney;
        if (isMoving)
        {
            MovePlayer();
        }
    }
    public void MovePlayer()
    {
        if (currentStep < spacesToMove)
        {
            finalTile = moveQueue[currentStep];
            targetPosition = finalTile.transform.position;

            // 逐步移動至目標位置
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            if (transform.position == targetPosition)
            {
                currentStep++;
            }
        }
        else
        {
            isMoving = false;
            currentStep = 0; // 重置步數
        }
    }
    public void MovePlayerToken(int steps)
    {
        spacesToMove = steps;
        valueText.text = "Steps: " + spacesToMove.ToString();
        moveQueue = new Tile[spacesToMove];
        seeMove = steps;
        for (int i = 0; i < spacesToMove; i++)
        {
            finalTile = finalTile.nextTile;
            targetPosition = finalTile.transform.position;
            moveQueue[i] = finalTile;
        }
        isMoving = true;
    }
    void SetNewTargetPosition(Vector3 pos)
    {
        targetPosition = pos;
        velocity = Vector3.zero;
    }
    public void SendToJail()
    {
        isInJail = true;
        turnsInJail = 1; // 初始化玩家在监狱中的回合数
        // 可以在这里添加其他监狱逻辑，比如禁用移动等
    }
    public void EndTurn()
    {
        if (isInJail)
        {
            // 增加在监狱中的回合数
            turnsInJail++;

            // 如果玩家在监狱中停留了足够的回合，释放玩家
            if (turnsInJail >= 2)
            {
                ReleaseFromJail();
            }
        }
    }
    public void ReleaseFromJail()
    {
        isInJail = false;
        CanBeInJail=false;
        turnsInJail = 0;
        // 重新启用移动等
    }
    
}