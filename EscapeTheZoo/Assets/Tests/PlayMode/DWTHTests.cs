using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class DWTHTests
{
    public DWTHPlayerController player;
    public DWTHPushableObject pushableObject;
    public BoxCollider2D border;

    [SetUp]
    public void SetUp()
    {
        var gameObject = new GameObject();
        var gameObject2 = gameObject.AddComponent<Rigidbody2D>();
        var gameObject3 = gameObject2.gameObject.AddComponent<BoxCollider2D>();
        player = gameObject3.gameObject.AddComponent<DWTHPlayerController>();
        player.gameObject.tag = "Player 1";
        player.upKey = KeyCode.W;
        player.downKey = KeyCode.S;
        player.rightKey = KeyCode.D;
        player.leftKey = KeyCode.A;
        player.GetComponent<Transform>().localPosition = new Vector3(-5, 0, 0);

        gameObject = new GameObject();
        gameObject2 = gameObject.AddComponent<Rigidbody2D>();
        gameObject3 = gameObject2.gameObject.AddComponent<BoxCollider2D>();
        pushableObject = gameObject3.gameObject.AddComponent<DWTHPushableObject>();
        pushableObject.GetComponent<Transform>().localPosition = new Vector3(0, 0, 0);
        pushableObject.player1 = player;

        gameObject = new GameObject();
        border = gameObject.AddComponent<BoxCollider2D>();
        border.gameObject.tag = "Border";
        border.GetComponent<Transform>().localPosition = new Vector3(5, 0, 0);
    }

    [UnityTest]
    public IEnumerator MoveUp() // Press the "Up" key to test if it moves the player up
    {
        float originalYPosition = player.gameObject.transform.position.y;

        yield return new WaitForSeconds(5);

        Assert.IsTrue((player.gameObject.transform.position.y > originalYPosition));
    }

    [UnityTest]
    public IEnumerator MoveDown() // Press the "Down" key to test if it moves the player down
    {
        float originalYPosition = player.gameObject.transform.position.y;

        yield return new WaitForSeconds(5);

        Assert.IsTrue((player.gameObject.transform.position.y < originalYPosition));
    }

    [UnityTest]
    public IEnumerator MoveRight() // Press the "Right" key to test if it moves the player right
    {
        float originalXPosition = player.gameObject.transform.position.x;

        yield return new WaitForSeconds(5);

        Assert.IsTrue((player.gameObject.transform.position.x > originalXPosition));
    }

    [UnityTest]
    public IEnumerator MoveLeft() // Press the "Left" key to test if it moves the player left
    {
        float originalXPosition = player.gameObject.transform.position.x;

        yield return new WaitForSeconds(5);

        Assert.IsTrue((player.gameObject.transform.position.x < originalXPosition));
    }

    [UnityTest]
    public IEnumerator Player1PushesAnObjectOff() // Move the player right to see if the object is pushed off
    {
        yield return new WaitForSeconds(5);

        Assert.AreEqual(expected: 1, actual: player.numPushed);
    }

    [UnityTest]
    public IEnumerator OneObjectRemoved() // Move the player right to see if the object is pushed off and removed
    {
        DWTHController.numObjects = 1;

        yield return new WaitForSeconds(5);

        Assert.AreEqual(expected: 0, actual: DWTHController.numObjects);
    }
}
