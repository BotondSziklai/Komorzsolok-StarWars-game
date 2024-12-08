using NUnit.Framework;
using UnityEngine;

public class BossBehaviorTest
{
    private GameObject bossObject;
    private BossBehavior boss;

    [SetUp]
    public void Setup()
    {
        bossObject = new GameObject();
        boss = bossObject.AddComponent<BossBehavior>();
        boss.maxHealth = 10;

        var levelCompleteManagerObject = new GameObject();
        var levelCompleteManager = levelCompleteManagerObject.AddComponent<LevelCompleteManager>();

        var nextLevelData = ScriptableObject.CreateInstance<LevelData>();
        nextLevelData.levelName = "Level 2";
        levelCompleteManager.nextLevelData = nextLevelData;

        var levelCompleteCanvas = new GameObject("LevelCompleteCanvas");
        levelCompleteManager.levelCompleteCanvas = levelCompleteCanvas;

        bossObject.SendMessage("Start");
    }



    [Test]
    public void TakeDamage_ReducesHealth()
    {
        boss.TakeDamage(3);
        Assert.AreEqual(7, boss.GetCurrentHealth());
    }

    [Test]
    public void TakeDamage_TriggersDeathAtZeroHealth()
    {
        boss.TakeDamage(10);
        Assert.AreEqual(0, boss.GetCurrentHealth());
    }
}
