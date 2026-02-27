using UnityEngine;

[CreateAssetMenu(fileName = "LevelDatabase", menuName = "Game/LevelDatabase")]
public class LevelDatabase : ScriptableObject
{
    public LevelConfigSO[] levels;

    public LevelConfigSO Get(string levelId)
    {
        foreach (var l in levels)
        {
            if (l != null && l.levelId == levelId)
                return l;
        }

        return null;
    }
}
