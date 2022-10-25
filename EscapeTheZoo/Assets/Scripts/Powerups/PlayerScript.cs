using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private string playername; // The name of the player
    private int totalBalance; // The total currency a player has. Can be used in the shop.
    private int gameBalance; // The amount of currency a player has in a game of "Escape The Zoo!". Cannot be used in the shop.


    public PlayerScript(string name, int totalBalance)
    {
        this.playername = name;
        this.totalBalance = totalBalance;
        this.gameBalance = 0;
    }

    // Gets the player's name
    public string getName()
    {
        return this.playername;
    }

    // Sets the player's name
    public void setName(string name)
    {
        this.playername = name;
    }

    // Gets the player's current totalBalance
    public int getTotalBalance()
    {
        return this.totalBalance;
    }

    // Sets the player's totalBalance to a certain value
    public void setTotalBalance(int amount)
    {
        this.totalBalance = amount;
    }

    // Gets the player's current gameBalance
    public int getGameBalance()
    {
        return this.gameBalance;
    }

    // Sets the player's gameBalance to a certain value
    public void setGameBalance(int amount)
    {
        this.gameBalance = amount;

        if (this.gameBalance < 0)
        {
            this.gameBalance = 0;
        }
    }

    // Changes the gameBalance by "amount"
    public void changeGameBalanceByAmount(int amount)
    {
        this.gameBalance += amount;

        if (this.gameBalance < 0)
        {
            this.gameBalance = 0;
        }
    }

    // Adds the entire gameBalance of a player to their totalBalance, then sets their gameBalance to 0
    public void addGameBalanceToTotalBalance()
    {
        this.totalBalance += this.gameBalance;
        this.gameBalance = 0;
    }
}
