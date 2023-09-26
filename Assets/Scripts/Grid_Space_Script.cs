using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Grid_Space_Script : MonoBehaviour
{
    public Button button;
    public Text buttonText;

    private GameController gameController;

    public void SetSpace() {
        if (gameController.playerMove == true){
        buttonText.text = gameController.GetPlayerSide();
        button.interactable = false;
        gameController.EndTurn();
        }
    }

    public void SetGameControllerReference(GameController controller) {
        gameController = controller;
    }
}
