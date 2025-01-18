using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public PlayerToken player;
    public AIToken aiToken; 
    public static int difficultyLevel=2; // 1: 簡單, 2: 普通, 3: 困難, 4: 極難

    private bool playerTurn = true;
    private bool playerCanMove = true;
    public bool aiIsStaying = false;
    public Button rollDiceButton,playerBuyButton;
    //public LandPurchase landPurchase,ailandPurchase;
    public UIManager uiManager;
    public Text textUI;
    public GameObject skipButton,DiceButton;
    private Coroutine aiTurnCoroutine;
    public int movechange=1;
    private bool isAITurnEnding = false;
    public GameObject WinMenu,LoseMenu; 
    //public List<PlayerToken> players; // 玩家列表
    //public List<AIToken> ais; // AI列表

    void Start()
    {
        player.InitializePlayerToken(1000);
        aiToken.InitializeAIToken(difficultyLevel * 500);
        rollDiceButton.onClick.AddListener(PlayerTurnStart);
        //uiManager.UpdatePlayerUI(player.totalMoney, player.OwnedLands.Count);
        //uiManager.UpdateAIUI(aiToken.Money, aiToken.OwnedLands.Count);
    }
    void PlayerTurnStart()
    {
        if (playerTurn && playerCanMove)
        {
            rolldice(); // 觸發擲骰子
        }
        
    }
    public void SkipRound(){
        if (playerTurn)
        {
            if(aiToken.isInJail==false){
                aiToken.MoveAIToken(Random.Range(1, 5));
                //aiToken.MoveAIToken(1);
            }else{
                //aiToken.MoveAIToken(0);
            }
            
            isAITurnEnding=false;
            playerTurn = false; // 換AI回合
        }
    }
    public void rolldice(){
            int diceRoll = Random.Range(1, 5); // 擲骰子，獲得1-4之間的隨機數
            
            player.MovePlayerToken(diceRoll);
            //player.MovePlayerToken(1);
            playerCanMove = false;
            movechange=0;
    }
        void Update()
    {
        //uiManager.UpdatePlayerUI(player.totalMoney, player.OwnedLands.Count);
        //uiManager.UpdateAIUI(aiToken.totalMoney, aiToken.OwnedLands.Count);
        GameSummary();
        if (playerTurn)
        {
            if(player.isInJail==true){
                SkipRound();
                player.EndTurn();
                DiceButton.SetActive(false);
                skipButton.SetActive(false);
                //Debug.Log("In jail!");
            }
            if (playerCanMove)
            {
                // 玩家回合
                if(player.isInJail==true){
                    DiceButton.SetActive(false);
                    skipButton.SetActive(false);
                }else{
                    skipButton.SetActive(false);
                    DiceButton.SetActive(true);
                }
                
            }
            else
            {
                if(player.isInJail==true){
                    DiceButton.SetActive(false);
                    skipButton.SetActive(false);
                }else{
                    skipButton.SetActive(true);
                    DiceButton.SetActive(false);
                    }
                
            }
            
        }
        else
        {
            // AI回合
                if(aiToken.isInJail==true){
                    aiToken.EndTurn();
                    EndAITurn();
                }else{
                    // 根據難易度設定AI 購買/升級地皮的機率
                    int buyOrUpgradeChance = Random.Range(1, 101);
                    int threshold = 0;

                    switch (difficultyLevel)
                    {
                        case 1:
                            threshold = 25;
                            break;
                        case 2:
                            threshold = 50;
                            break;
                        case 3:
                            threshold = 75;
                            break;
                        case 4:
                           threshold = 100;
                           break;
                    }

                if (buyOrUpgradeChance <= threshold)
                {
                    // AI 買地或升級的機率
                    int action = Random.Range(1, 101);

                    if (action <= threshold)
                    {


                    }
                }
                aiTurnCoroutine = StartCoroutine(StartAITurnTimer());
                }
                skipButton.SetActive(false);
                
        }

    }
    IEnumerator StartAITurnTimer()
    {
        yield return new WaitForSeconds(3); // 等待
        // 十秒後自動跳轉回玩家回合
        if (!isAITurnEnding) // 添加检查
        {
            EndAITurn();
        }
    }

    public void EndAITurn()
    {
        if (isAITurnEnding) return;
        isAITurnEnding = true;
        playerTurn = true; // 換玩家回合
        playerCanMove = true; // 確保玩家可以移動
        StopCoroutine(aiTurnCoroutine); // 停止計時協程
        //UpdatePlayerUI(); // 更新玩家的 UI
        movechange+=1;
        //Debug.Log("Is player turn!");
    }
    void GameSummary(){
        if(PlayerToken.totalMoney<=0){
            LoseMenu.SetActive(true);  
        }
        if(AIToken.totalMoney<=0){
            WinMenu.SetActive(true);  
        }
        if(StarterGiveMoney.wavecount>=20){
            //PlayerToken.totalMoney+PlayerToken.OwnedLands;
            if(PlayerToken.totalMoney+PlayerToken.OwnedLands>AIToken.totalMoney+AIToken.OwnedLands){
                WinMenu.SetActive(true);  
            }else if(PlayerToken.totalMoney+PlayerToken.OwnedLands<AIToken.totalMoney+AIToken.OwnedLands){
                LoseMenu.SetActive(true);  
            }else{
                WinMenu.SetActive(true);  
            }
        }
    }    
  
}