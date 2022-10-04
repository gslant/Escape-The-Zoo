using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class CTBTests
{
    private CTBManager _CTBManager;
    private GameObject _bananaObject;

    [SetUp]
    public void SetUp()
    {
        var managerObject = new GameObject();
        _CTBManager = managerObject.AddComponent<CTBManager>();

        // Create banana object similar to the banana prefab
        _bananaObject = new GameObject();
        _bananaObject.transform.position = new Vector3(0, 6, 0);
    }

    [Test]
    public void BananasSpawnRandomlyWithinRange()
    {
        GameObject bananaSpawn = _CTBManager.SpawnBanana(_bananaObject);

        Assert.AreNotEqual(_bananaObject.transform.position.x, bananaSpawn.transform.position.x);
        Assert.IsTrue(bananaSpawn.transform.position.x >= -8 && bananaSpawn.transform.position.x <= 8);
    }

    [Test]
    public void ScoreIncrements()
    {
        _CTBManager.IncrementScore("Player1");
        _CTBManager.IncrementScore("Player2");

        int expected = 1;

        Assert.AreEqual(expected, _CTBManager.Player1Score);
        Assert.AreEqual(expected, _CTBManager.Player2Score);
    }

    [Test]
    public void PlayerWins()
    {
        // Simulating player 1 winning
        int winScore = 20;

        for (int i = 0; i < winScore; i++)
        {
            _CTBManager.IncrementScore("Player1");
        }

        string expectedWinner = "Player1";
        string actualWinner = _CTBManager.CheckWin();

        Assert.AreEqual(expectedWinner, actualWinner);
    }
}
