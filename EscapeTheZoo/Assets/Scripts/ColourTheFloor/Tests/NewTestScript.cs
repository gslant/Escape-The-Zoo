using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class NewTestScript
{
    private CTFController _CTFController;
    private GameObject _squareObject;
    private CTFPlayerController _PlayerCon;
    private BoxCollider2D playerCollider;

    [SetUp]
    public void SetUp()
    {
        var controllerObject = new GameObject();
        _CTFController = controllerObject.AddComponent<CTFController>();

        _squareObject = new GameObject();

        var playerObject = new GameObject();
        _PlayerCon = playerObject.AddComponent<CTFPlayerController>();
        playerCollider = playerObject.AddComponent<BoxCollider2D>();
    }

    // A Test behaves as an ordinary method
    [Test]
    public void SquaresSpawnCorrectNumber()
    {
        Assert.NotNull(_squareObject);

        _CTFController.SpawnSquares(_squareObject);

        Assert.NotNull(_CTFController.squares.Count);
        Assert.AreEqual(665, _CTFController.squares.Count);
    }

    [Test]
    public void playerHitboxExpandsOnPowerUp()
    {
        Assert.NotNull(_PlayerCon);
        Vector3 initialSize = playerCollider.size;

        _CTFController.increaseSize(playerCollider);

        Assert.Greater(playerCollider.size.x, initialSize.x);
    }

}
