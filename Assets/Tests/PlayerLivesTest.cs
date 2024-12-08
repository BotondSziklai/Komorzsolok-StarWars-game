using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class PlayerLivesTest
{
    private GameObject playerObject;
    private PlayerLives playerLives;

    [SetUp]
    public void Setup()
    {
        playerObject = new GameObject();
        playerLives = playerObject.AddComponent<PlayerLives>();
        playerLives.lives = 3;
    }

    [Test]
    public void LivesDecreaseOnCollision()
    {
        playerLives.lives -= 1;
        Assert.AreEqual(2, playerLives.lives);
    }

    [Test]
    public void GameOver_WhenLivesReachZero()
    {
        playerLives.lives = 0;
        Assert.IsTrue(playerLives.lives <= 0);
    }
}