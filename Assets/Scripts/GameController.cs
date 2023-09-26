using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[System.Serializable]
public class Player{
    public Image panel;
    public Text text;
    public Button button;
}

[System.Serializable]
public class PlayerColor{
    public Color panelColor;
    public Color textColor;
}

public class GameController : MonoBehaviour
{
    public Text[] buttonList;
    public GameObject gameOverPanel;
    public Text gameOverText;
    public GameObject restarButton;
    public Player playerX;
    public Player playerO;
    public PlayerColor activePlayerColor;
    public PlayerColor inactivePlayerColor;
    public GameObject startInfo;
    
    private string playerSide;
    private string aiSide;
    public bool playerMove;
    public float delay;
    private int value;
    private int moveCount;

    void Awake(){
        gameOverPanel.SetActive(false);
        SetGameControllerReferenceOnButtons();
        moveCount = 0;
        restarButton.SetActive(false);
        playerMove = true;
    }

    void Update(){
        if (playerMove == false){
            delay += delay * Time.deltaTime;
            if (delay >= 100){
                value = Random.Range(0, 8);
                if (buttonList[value].GetComponentInParent<Button>().interactable == true){
                    buttonList[value].text = GetAiSide();
                    buttonList[value].GetComponentInParent<Button>().interactable = false;
                    EndTurn();
                }
            }
        }
    }
    
    void SetGameControllerReferenceOnButtons(){
        for(int i = 0; i < buttonList.Length; i++){
            buttonList[i].GetComponentInParent<Grid_Space_Script>().SetGameControllerReference(this);
        }
    }

    public void SetStartingSide(string startingSide){
        playerSide = startingSide;
        if (playerSide == "X"){
            aiSide = "O";
            SetPlayerColors(playerX, playerO);
        } else {
            aiSide = "X";
            SetPlayerColors(playerO, playerX);
        }

        StartGame();
    }

    void StartGame(){
        SetBoardInteractable(true);
        SetPlayersButtons(false);
        startInfo.SetActive(false);
    }

    public string GetPlayerSide(){
        return playerSide;
    }

    public string GetAiSide(){
        return aiSide;
    }

    public void EndTurn(){
        
        moveCount++;

        if (buttonList[0].text == playerSide && buttonList[1].text == playerSide && buttonList[2].text == playerSide){
            GameOver(playerSide);
        }
        else if (buttonList[3].text == playerSide && buttonList[4].text == playerSide && buttonList[5].text == playerSide){
            GameOver(playerSide);
        }
        else if (buttonList[6].text == playerSide && buttonList[7].text == playerSide && buttonList[8].text == playerSide){
            GameOver(playerSide);
        }
        else if (buttonList[0].text == playerSide && buttonList[3].text == playerSide && buttonList[6].text == playerSide){
            GameOver(playerSide);
        }
        else if (buttonList[1].text == playerSide && buttonList[4].text == playerSide && buttonList[7].text == playerSide){
            GameOver(playerSide);
        }       
        else if (buttonList[2].text == playerSide && buttonList[5].text == playerSide && buttonList[8].text == playerSide){
            GameOver(playerSide);
        }
        else if (buttonList[0].text == playerSide && buttonList[4].text == playerSide && buttonList[8].text == playerSide){
            GameOver(playerSide);
        }
        else if (buttonList[2].text == playerSide && buttonList[4].text == playerSide && buttonList[6].text == playerSide){
            GameOver(playerSide);
        }

        else if (buttonList[0].text == playerSide && buttonList[1].text == playerSide && buttonList[2].text == playerSide){
            GameOver(aiSide);
        }
        else if (buttonList[3].text == playerSide && buttonList[4].text == playerSide && buttonList[5].text == playerSide){
            GameOver(aiSide);
        }
        else if (buttonList[6].text == playerSide && buttonList[7].text == playerSide && buttonList[8].text == playerSide){
            GameOver(aiSide);
        }
        else if (buttonList[0].text == playerSide && buttonList[3].text == playerSide && buttonList[6].text == playerSide){
            GameOver(aiSide);
        }
        else if (buttonList[1].text == playerSide && buttonList[4].text == playerSide && buttonList[7].text == playerSide){
            GameOver(aiSide);
        }       
        else if (buttonList[2].text == playerSide && buttonList[5].text == playerSide && buttonList[8].text == playerSide){
            GameOver(aiSide);
        }
        else if (buttonList[0].text == playerSide && buttonList[4].text == playerSide && buttonList[8].text == playerSide){
            GameOver(aiSide);
        }
        else if (buttonList[2].text == playerSide && buttonList[4].text == playerSide && buttonList[6].text == playerSide){
            GameOver(aiSide);
        }
        else if (moveCount >= 9){
            GameOver("empatou");
        }else{
            ChangeSide();
            delay = 10;
        }
    }

    void SetPlayerColors(Player newPlayer, Player oldPlayer){
        newPlayer.panel.color = activePlayerColor.panelColor;
        newPlayer.text.color = activePlayerColor.textColor;
        oldPlayer.panel.color = inactivePlayerColor.panelColor;
        oldPlayer.text.color = inactivePlayerColor.textColor;
    }

    void GameOver(string  winnigPlayer){
        SetBoardInteractable(false);

        if (winnigPlayer == "empatou"){
            SetGameOverText("Empatou");
            SetPlayerColorsInactive();
        } else {
            SetGameOverText(winnigPlayer + " Venceu!");
        }
        restarButton.SetActive(true);
    }

    void ChangeSide(){
        //playerSide = (playerSide == "X") ? "O" : "X";
        playerMove = (playerMove == true) ? false :true;

        //if (playerSide == "X"){
        if (playerMove == true){    
            SetPlayerColors(playerX, playerO);
        } else {
            SetPlayerColors(playerO, playerX);
        }
    }

    void SetGameOverText(string value){
       
        gameOverPanel.SetActive(true);
        gameOverText.text = value;
    }

    public void RestarGame(){
        moveCount =  0;
        gameOverPanel.SetActive(false);
        restarButton.SetActive(false);
        SetPlayersButtons(true);
        SetPlayerColorsInactive();
        startInfo.SetActive(true);
        playerMove = true;
        delay = 10;

        for (int i = 0; i < buttonList.Length; i++){
            buttonList[i].text = "";
        }

        
    }

    void SetBoardInteractable(bool toggle){
        for (int i = 0; i < buttonList.Length; i++){
            buttonList[i].GetComponentInParent<Button>().interactable = toggle;
        }
    }

    void SetPlayersButtons(bool toggle){
        playerX.button.interactable = toggle;
        playerO.button.interactable = toggle;
    }

    void SetPlayerColorsInactive(){
        playerX.panel.color = inactivePlayerColor.panelColor;
        playerX.text.color = inactivePlayerColor.textColor;
        playerO.panel.color = inactivePlayerColor.panelColor;
        playerO.text.color = inactivePlayerColor.textColor;
    }
}
