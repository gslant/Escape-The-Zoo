using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class PowerUpsTest
{
    private PowerUpsImplementation powerUps;
    private PlayerScript player;
    private PlayerMovement playerMovement;
    private GameControl control;



    [SetUp]
    public void SetUp()
    {
        var powerupsObject = new GameObject();
        powerUps = powerupsObject.AddComponent<PowerUpsImplementation>();


        var playerObject = new GameObject();
        player = playerObject.AddComponent<PlayerScript>();

        var movementrObject = new GameObject();
        playerMovement = movementrObject.AddComponent<PlayerMovement>();

        var controllerObject = new GameObject();
        control = controllerObject.AddComponent<GameControl>();

    }


    [Test]
    public void gainCoinsTest()
    {
        powerUps.gainCoins(player);

        int expectedValue = 5;

        Assert.AreEqual(expectedValue, player.getGameBalance());
    }

    [Test]
    public void loseCoinsTest()
    {
        player.changeGameBalanceByAmount(25);

        powerUps.loseCoins(player);

        int expectedValue = 15;

        Assert.AreEqual(expectedValue, player.getGameBalance());
    }

    [Test]
    public void moveUpTest()
    {
        powerUps.moveup(player);
        int expectedResult = powerUps.up + 1;

        Assert.AreEqual(expectedResult, control.perviousdiceSideThrown);

    }


}
