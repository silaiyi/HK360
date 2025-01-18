using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LandPurchase : MonoBehaviour
{
    public Tile tile; // 土地的屬性
    public GameManager gameManager; // GameManager的參考
/*
    public void BuyLand()
    {
        if (tile != null)
        {
            if (tile.isPlayerOwned){
                int currentPrice = tile.Price;
                int currentLevel = tile.Level;

                if (gameManager.player.Money >= currentPrice)
                {
                    // 購買土地
                    gameManager.player.Money -= currentPrice; // 減去金錢
                    tile.Level++; // 土地等級+1
                    tile.UpdateTileVisuals(true);
                     // 更新UI顯示
                    Debug.Log("Land purchased successfully!");
                }
                else
                {
                    Debug.Log("Not enough money to buy this land.");
                }
            }
            
        }
    }*/
}