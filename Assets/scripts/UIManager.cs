using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text playerMoneyText;
    public Text playerLandCountText;
    public Text aiMoneyText;
    public Text aiLandCountText;
    public GameObject ChessBoard,HideBtn,ShowBtn,InfoMenu,pausebtn;
    
    
    // 更新玩家金錢和土地數的UI顯示
    void Update(){
        UpdatePlayerUI();
        UpdateAIUI();
    }
    
    public void UpdatePlayerUI()
    {
        playerMoneyText.text = ""+PlayerToken.totalMoney;
        playerLandCountText.text = ""+PlayerToken.OwnedLands;
    }
    
    // 更新AI金錢和土地數的UI顯示
    public void UpdateAIUI()
    {
        aiMoneyText.text = "" + AIToken.totalMoney;
        aiLandCountText.text = "" + AIToken.OwnedLands;
    }
    public void HideChessboard(){
        ChessBoard.SetActive(false);
        HideBtn.SetActive(false);
        InfoMenu.SetActive(false);
        pausebtn.SetActive(false);
        ShowBtn.SetActive(true);
    }
    public void ShowChessboard(){
        ChessBoard.SetActive(true);
        HideBtn.SetActive(true);
        InfoMenu.SetActive(true);
        pausebtn.SetActive(true);
        ShowBtn.SetActive(false);
    }
    
}