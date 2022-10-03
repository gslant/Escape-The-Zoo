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

    // test score increments
    /*    public IEnumerator ScoreIncrementsAfterCatchingBanana()
        {
            yield return null;

        }*/

    // test player win
    /*    public IEnumerator PlayerWins()
        {
            yield return null;

        }*/
}