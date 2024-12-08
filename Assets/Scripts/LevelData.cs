using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "Game/Level Data")]
public class LevelData : ScriptableObject
{
    public string levelName; // Name of the current level
    public string nextLevelCutscene; // Name of the next level's cutscene
}
