using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI player,computer;
    public TextMeshProUGUI continueButton;

    public int playerScore;
    public int computerScore;
    
    public void IncrementScore(string colliderName)
    {
        switch (colliderName)
        {
            case "Bounds North":
                computerScore++;
                computer.text = "Computer Score: "+ computerScore;
                return;
            case "Bounds South":
                playerScore++;
                player.text = "Player Score: " + computerScore;
                return;
        }
    }
    public void GoBack()
    {
        SceneManager.LoadScene(1);
    }
}
