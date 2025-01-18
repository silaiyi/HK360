using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Tile : MonoBehaviour
{
    public Tile nextTile;
    public int price= 100; // 地皮價格
    int rent=50;
    public int level = 0; // 地皮等級
    //public string buildingType; // 地皮建築類型
    public GameObject buildingModelLv1,buildingModelLv2,buildingModelLv3,LandInfo,playLandMark,AiLandMark,star1,star2,star3,star4,landCube,Landouter,locatBtn,InfoBtn,BuyBtn,InfoPage,LocatPage;
    //public GameObject LandInfo,playLandMark,AiLandMark,star1,star2,star3,star4,landCube,Landouter,locatBtn,InfoBtn,BuyBtn,InfoPage,LocatPage;
    public Text Cost,Rent;
    public Material playerLand,playerLand2,AiLand,AiLand2;
    public Material skyboxMaterial; // 天空盒材質
    private Renderer objectRenderer;
    public bool isPurchased = false;
    public int purchasePrice;
    public int compensationPrice;
    public bool isPlayerOwned,isAiOwned;
    //public string txtFileName;
    public bool canBuy= true;
    public bool isJailTile = false,isOpZone = false,isGameZone = false;
    public GameObject HugebuildingModelLv1,HugebuildingModelLv2,HugebuildingModelLv3;
    public bool isBought = false;
    //private GameManager gameManager;
/*
Renderer rend2 = Landouter.GetComponent<Renderer>();
rend2.material = playerLand;
Renderer rend = landCube.GetComponent<Renderer>();
rend.material= playerLand2;
Renderer rend2 = Landouter.GetComponent<Renderer>();
rend2.material = AiLand;
Renderer rend = landCube.GetComponent<Renderer>();
rend.material= AiLand2;
*/
    private void Start()
    {
        // 初始化碰撞体
        Collider collider = GetComponent<Collider>();
        if (collider != null)
        {
            collider.isTrigger = true; // 设置为触发器
        }
        Cost.text=""+price+"";
        Rent.text=""+rent+"";
    }
    void Update(){
        Cost.text=""+price+"";
        Rent.text=""+rent+"";
        LandLv();
    }
    private void OnTriggerStay(Collider other)
    {
        // 检查是否是玩家棋子
        if (other.CompareTag("Player") && !other.GetComponent<PlayerToken>().isMoving)
        {
                PlaySkybox();
                LandInfo.SetActive(true);
                /*
                InfoPage.SetActive(true);
                InfoBtn.SetActive(false);
                LocatPage.SetActive(false);
                locatBtn.SetActive(true);*/
          
        }else{
            //LandInfo.SetActive(false);
        }
        if (other.CompareTag("Player")&&!other.GetComponent<PlayerToken>().isMoving)
        {
            // 检查此Tile是否是监狱格
            if (this.isJailTile)
            {                
                if(PlayerToken.CanBeInJail==true){
                    other.GetComponent<PlayerToken>().SendToJail();
                    Debug.Log("Player in jail!");
                }
            }
            // 检查此Tile是否是機會格
            if(this.isOpZone){
                if(PlayerToken.CanGetChange==true){
                    PlayerTriggerOpportunityCardEffect();                   
                }
            }
            if(this.isGameZone){
                if(PlayerToken.CanPlayGane==true){
                    PlayerMINIGAME();
                }
            }
            if(this.canBuy){
                if(isAiOwned==false){
                    if(PlayerToken.canBuyLand == true){
                        BuyBtn.SetActive(true);
                    }else{
                        BuyBtn.SetActive(false);
                    }   
                }else{
                    
                    BuyBtn.SetActive(false);
                    if(PlayerToken.canLoseMoney==true){
                        PlayerToken.exMoney-=rent;
                        AIToken.exMoney+=rent;
                        PlayerToken.canLoseMoney=false;
                    }
                }
            }
        }
        if (other.CompareTag("AI")&&!other.GetComponent<AIToken>().isMoving)
        {
            // 检查此Tile是否是监狱格
            if (this.isJailTile)
            {
                if(AIToken.CanBeInJail==true){
                    other.GetComponent<AIToken>().SendToJail();
                    //Debug.Log("AI in jail!");
                    //AIToken.CanBeInJail=false;   
                }
            }
            // 检查此Tile是否是機會格
            if(this.isOpZone){
                if(AIToken.CanGetChange==true){
                    AITriggerOpportunityCardEffect();
                }
            }
            // 检查此Tile是否是遊戲格
            if(this.isGameZone){
                if(AIToken.CanPlayGane==true){
                    AiMINIGAME();
                }
            }
            if(this.canBuy){
                if(isPlayerOwned==false){
                    //AIBuyLand();
                    if(AIToken.canBuyLand==true){
                        AIBuyLand();
                    }
                }else{
                    
                    if(AIToken.canLoseMoney==true){
                        PlayerToken.exMoney+=rent;
                        AIToken.exMoney-=rent;
                        AIToken.canLoseMoney=false;
                    }
                }
            }
        }
    }
    private void OnTriggerExit(Collider other){
        if (other.CompareTag("AI"))
        {
            // 检查此Tile是否是监狱格
            if (this.isJailTile)
            {
                AIToken.CanBeInJail=true;                
            }
            if(this.isOpZone){
                AIToken.CanGetChange=true;
            }
            if(this.isGameZone){
                AIToken.CanPlayGane=true;
            }
            if(this.canBuy){
                AIToken.canBuyLand=true;
            }
            AIToken.canLoseMoney=true;
        }
        if (other.CompareTag("Player"))
        {            
            if (this.isJailTile)
            {
                PlayerToken.CanBeInJail=true;                
            }
            if(this.isOpZone){
                PlayerToken.CanGetChange=true;
            }
            if(this.isGameZone){
                PlayerToken.CanPlayGane=true;
            }
            if(this.canBuy){
                PlayerToken.canBuyLand=true;
            }
            LandInfo.SetActive(false);
            PlayerToken.canLoseMoney=true;
        }
    }
    private void PlaySkybox()
    {
        RenderSettings.skybox = skyboxMaterial;
    }
    public void ToInfoPage(){
        InfoPage.SetActive(true);
        InfoBtn.SetActive(false);
        LocatPage.SetActive(false);
        locatBtn.SetActive(true);
    }
    public void TolocatePage(){
        InfoPage.SetActive(false);
        InfoBtn.SetActive(true);
        LocatPage.SetActive(true);
        locatBtn.SetActive(false);
    }
    private void PlayerTriggerOpportunityCardEffect()
{
    // 随机选择一个效果
    int effectIndex = Random.Range(0, 3); // 生成0, 1或2的随机数

    switch (effectIndex)
    {
        case 0:
            // 获得50元
            PlayerToken.exMoney+=50;
            Debug.Log("机会卡效果：获得50元");
            PlayerToken.CanGetChange=false;
            // 添加逻辑来增加玩家的金钱
            break;
        case 1:
            // 获得100元
            PlayerToken.exMoney+=100;
            Debug.Log("机会卡效果：获得100元");
            PlayerToken.CanGetChange=false;
            // 添加逻辑来增加玩家的金钱
            break;
        case 2:
            // 获得300元
            PlayerToken.exMoney+=300;
            Debug.Log("机会卡效果：获得300元");
            PlayerToken.CanGetChange=false;
            // 添加逻辑来增加玩家的金钱
            break;
    }
    PlayerToken.CanGetChange=false;
}
private void AITriggerOpportunityCardEffect()
{
    // 随机选择一个效果
    int effectIndex = Random.Range(0, 3); // 生成0, 1或2的随机数

    switch (effectIndex)
    {
        case 0:
            // 获得50元
            AIToken.exMoney+=50;
            Debug.Log("机会卡效果：获得50元");
            AIToken.CanGetChange=false;
            // 添加逻辑来增加玩家的金钱
            break;
        case 1:
            // 获得100元
            AIToken.exMoney+=100;
            Debug.Log("机会卡效果：获得100元");
            AIToken.CanGetChange=false;
            // 添加逻辑来增加玩家的金钱
            break;
        case 2:
            // 获得300元
            AIToken.exMoney+=300;
            Debug.Log("机会卡效果：获得300元");
            AIToken.CanGetChange=false;
            // 添加逻辑来增加玩家的金钱
            break;
    }
}
private void PlayerMINIGAME(){
    int randomRoll = Random.Range(0, 100);
    int randommoney = Random.Range(1, 301);
    if (randomRoll < 50)
        {
            // 玩家获胜
            PlayerToken.exMoney+=randommoney;
            AIToken.exMoney-=randommoney;
            Debug.Log("Player Win " + randommoney + " !");
        }
        else
        {
            // AI获胜
            PlayerToken.exMoney-=randommoney;
            AIToken.exMoney+=randommoney;
            Debug.Log("AI Win " + randommoney + " !");
        }
        PlayerToken.CanPlayGane=false;
}
private void AiMINIGAME(){
    int randomRoll = Random.Range(0, 100);
    int randommoney = Random.Range(1, 301);
    if (randomRoll < 50)
        {
            // 玩家获胜
            PlayerToken.exMoney+=randommoney;
            AIToken.exMoney-=randommoney;
            Debug.Log("Player Win " + randommoney + " !");
        }
        else
        {
            // AI获胜
            PlayerToken.exMoney-=randommoney;
            AIToken.exMoney+=randommoney;
            Debug.Log("AI Win " + randommoney + " !");
        }
        AIToken.CanPlayGane=false;
}
public void PlayerBuyLand(){
    if(PlayerToken.totalMoney-price>=0){
        PlayerToken.exMoney-=price;
        price+=50;
        rent+=50;
        level++;
        Renderer rend2 = Landouter.GetComponent<Renderer>();
        rend2.material = playerLand;
        Renderer rend = landCube.GetComponent<Renderer>();
        rend.material= playerLand2;  
        PlayerToken.canBuyLand=false;
        isPlayerOwned=true;
        LandLv();                          
    }
}
public void AIBuyLand(){
    int buyOrUpgradeChance = Random.Range(1, 101);
    int threshold = 0;
        switch (GameManager.difficultyLevel)
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
            if(AIToken.totalMoney-price>=0){
                AIToken.exMoney-=price;
                price+=50;
                rent+=50;
                level++;
                Renderer rend2 = Landouter.GetComponent<Renderer>();
                rend2.material = AiLand;
                Renderer rend = landCube.GetComponent<Renderer>();
                rend.material= AiLand2;  
                AIToken.canBuyLand=false;
                isAiOwned=true;                          
            }

        }else{
            AIToken.canBuyLand=false;
        }
    }
    LandLv();
    
}
public void LandLv(){
    //Debug.Log("Lv1");
    //bool isBought = false;
    if(level==0){
            buildingModelLv1.SetActive(false);
            buildingModelLv2.SetActive(false);
            buildingModelLv3.SetActive(false);
            HugebuildingModelLv1.SetActive(false);
            HugebuildingModelLv2.SetActive(false);
            HugebuildingModelLv3.SetActive(false);
            star1.SetActive(false);
            star2.SetActive(false);
            star3.SetActive(false);
            star4.SetActive(false);
            playLandMark.SetActive(false);
            AiLandMark.SetActive(false);
            //Debug.Log("Lv1");
        }else if(level==1){
            buildingModelLv1.SetActive(false);
            buildingModelLv2.SetActive(false);
            buildingModelLv3.SetActive(false);
            HugebuildingModelLv1.SetActive(false);
            HugebuildingModelLv2.SetActive(false);
            HugebuildingModelLv3.SetActive(false);
            star1.SetActive(true);
            star2.SetActive(false);
            star3.SetActive(false);
            star4.SetActive(false);
            Debug.Log("Lv1");
            if(isPlayerOwned==true){
                playLandMark.SetActive(true);
                Debug.Log("Lv1");
                if(isBought==false){
                    PlayerToken.OwnedLands+=50;
                    isBought=true;
                }else{}
            }else if(isAiOwned==true){
                AiLandMark.SetActive(true);
                if(isBought==false){
                    AIToken.OwnedLands+=50;
                    isBought=true;
                }else{}
            }
            //playLandMark.SetActive(true);
        }else if(level==2){
            buildingModelLv1.SetActive(true);
            buildingModelLv2.SetActive(false);
            buildingModelLv3.SetActive(false);
            HugebuildingModelLv1.SetActive(true);
            HugebuildingModelLv2.SetActive(false);
            HugebuildingModelLv3.SetActive(false);
            star1.SetActive(true);
            star2.SetActive(true);
            star3.SetActive(false);
            star4.SetActive(false);
            if(isPlayerOwned==true){
                playLandMark.SetActive(true);
                //Debug.Log("Lv1");
                if(isBought==false){
                    PlayerToken.OwnedLands+=50;
                    isBought=true;
                }else{}
            }else if(isAiOwned==true){
                AiLandMark.SetActive(true);
                if(isBought==false){
                    AIToken.OwnedLands+=50;
                    isBought=true;
                }else{}
            }
        }else if(level==3){
            buildingModelLv1.SetActive(false);
            buildingModelLv2.SetActive(true);
            buildingModelLv3.SetActive(false);
            HugebuildingModelLv1.SetActive(false);
            HugebuildingModelLv2.SetActive(true);
            HugebuildingModelLv3.SetActive(false);
            star1.SetActive(true);
            star2.SetActive(true);
            star3.SetActive(true);
            star4.SetActive(false);
            if(isPlayerOwned==true){
                playLandMark.SetActive(true);
                Debug.Log("Lv1");
                if(isBought==false){
                    PlayerToken.OwnedLands+=50;
                    isBought=true;
                }else{}
            }else if(isAiOwned==true){
                AiLandMark.SetActive(true);
                if(isBought==false){
                    AIToken.OwnedLands+=50;
                    isBought=true;
                }else{}
            }
        }else if(level==4){
            buildingModelLv1.SetActive(false);
            buildingModelLv2.SetActive(false);
            buildingModelLv3.SetActive(true);
            HugebuildingModelLv1.SetActive(false);
            HugebuildingModelLv2.SetActive(false);
            HugebuildingModelLv3.SetActive(true);
            star1.SetActive(true);
            star2.SetActive(true);
            star3.SetActive(true);
            star4.SetActive(true);
            BuyBtn.SetActive(false);
            if(isPlayerOwned==true){
                playLandMark.SetActive(true);
                Debug.Log("Lv1");
                if(isBought==false){
                    PlayerToken.OwnedLands+=50;
                    isBought=true;
                }else{}
            }else if(isAiOwned==true){
                AiLandMark.SetActive(true);
                if(isBought==false){
                    AIToken.OwnedLands+=50;
                    isBought=true;
                }else{}
            }
        }
}

}