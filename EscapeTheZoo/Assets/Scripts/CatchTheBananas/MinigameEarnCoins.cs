using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MinigameEarnCoins : MonoBehaviour
{
    // This script will change the game over text in the minigame and will give coins to the players

    // Assign this script to the same object with the script determining who wins so that you can just easily type:
    // GetComponent<MinigameEarnCoins>().GameOver(winner);
    // to accesss the method

    // Constants for the amount of coins earned
    private const int WINNING_COINS = 6;
    private const int LOSING_COINS = 2;

    [SerializeField] TextMeshProUGUI gameOverText;
    [SerializeField] GameObject player1, player2;

    static List<Player> listOfPlayersPlaying;

    public void GameOver(string winner)
    {
        listOfPlayersPlaying = MinigameLoadPlayers.GetListOfPlayersPlaying();

        int winIndex = -1;
        int loseIndex = -1;

        if (winner == player1.name)
        {
            winIndex = 0;
            loseIndex = 1;
            gameOverText.SetText(listOfPlayersPlaying[winIndex].getName() + " wins!");
        }
        else if (winner == player2.name)
        {
            winIndex = 1;
            loseIndex = 0;
            gameOverText.SetText(listOfPlayersPlaying[winIndex].getName() + " wins!");
        }
        else
        {
            Debug.Log("Error occurred - perhaps check the name of the variable passed or of player objects.");
        }

        EarnCoins(winIndex, loseIndex);
    }

    void EarnCoins(int winnerIndex, int loserIndex)
    {
        listOfPlayersPlaying[winnerIndex].changeGameBalanceByAmount(WINNING_COINS);
        listOfPlayersPlaying[loserIndex].changeGameBalanceByAmount(LOSING_COINS);
    }
}
