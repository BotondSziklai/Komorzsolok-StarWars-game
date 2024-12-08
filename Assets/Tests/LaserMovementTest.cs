using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class LaserMovementTests
{
    private GameObject laserObject;
    private LaserMovement laser;

    [SetUp]
    public void Setup()
    {
        laserObject = new GameObject();
        laser = laserObject.AddComponent<LaserMovement>();
        laser.speed = 5f;
    }

    [Test]
    public void LaserMovesCorrectly()
    {
        var laserObject = new GameObject();
        var laser = laserObject.AddComponent<LaserMovement>();
        laser.speed = 5f;

        Vector3 initialPosition = laserObject.transform.position;

        laserObject.transform.Translate(Vector3.right * laser.speed * 0.02f);

        Assert.AreNotEqual(initialPosition, laserObject.transform.position, "The laser did not move.");
    }



}