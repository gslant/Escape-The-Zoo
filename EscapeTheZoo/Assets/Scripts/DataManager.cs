using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class DataManager : MonoBehaviour
{
    public string fName = "profiles.txt";
    public void SaveData(Player p)
    {
        List<Player> tempPlayerList = new List<Player>();
        tempPlayerList = LoadData(fName);

        bool alreadyExists = false;
        for(int i = 0; i < tempPlayerList.Count; i++)
        {
            if(String.Equals(tempPlayerList[i].getName(), p.getName()))
            {
                tempPlayerList[i] = p;
                alreadyExists = true;
                break;
            }
        }
        if (!alreadyExists)
        {
            tempPlayerList.Add(p);
        }

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + fName);
        bf.Serialize(file, tempPlayerList);
        file.Close();
    }

    public List<Player> LoadData(string fileName)
    {
        List<Player> playerList = new List<Player>();
        if(File.Exists(Application.persistentDataPath + fileName))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + fileName, FileMode.Open);
            playerList = (List<Player>)bf.Deserialize(file);
            file.Close();
        }
        else
        {
            Debug.Log("Could not open file: " + fileName);
        }
        return playerList;
    }

    public void DeleteFile()
    {

        string filePath = Application.persistentDataPath + fName;

        if (File.Exists(filePath))
        {
            Debug.Log("Deleting file: " + fName + " at location: " + filePath);
            File.Delete(filePath);
        }
    }
}

[Serializable]

public class Player
{
    public string name;
    public int balance;
    public string animal;
    public string accessory;
    public List<string> ownedCosmetics;

    public Player(string name, int balance, string animal, string accessory, List<string> ownedCosmetics)
    {
        this.name = name;
        this.balance = balance;
        this.animal = animal;
        this.accessory = accessory;
        this.ownedCosmetics = ownedCosmetics;
    }

    public void setName(string name)
    {
        this.name = name;
    }

    public string getName()
    {
        return this.name;
    }

    public void deductFromBalance(int amount)
    {
        this.balance -= amount;
    }

    public void addToBalance(int amount)
    {
        this.balance += amount;
    }

    public string getAnimal()
    {
        return this.animal;
    }

    public void setAnimal(string animal)
    {
        this.animal = animal;
    }

    public string getAccessory()
    {
        return this.accessory;
    }

    public void setAccessory(string accessory)
    {
        this.accessory = accessory;
    }

    public List<string> getOwnedCosmetics()
    {
        return this.ownedCosmetics;
    }


    public void giveCosmetic(string cosmeticName)
    {
        this.ownedCosmetics.Add(cosmeticName);
    }
}

