using UnityEngine;

[CreateAssetMenu(fileName = "LevelConfig", menuName = "Game/LevelConfig")]
public class LevelConfigSO : ScriptableObject
{
    public string levelId = "level_01";
    public string sceneName = "Level1";
    public int startingDay = 1;
    public int startingTime = 0;
    public bool powerStartsOn = false;
}
