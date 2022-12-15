using UnityEngine;

public class ChooseDifficulty : MonoBehaviour
{

    public MenuSelectManager manager;
    public LevelManager.GameDifficulty gameDifficulty;

    public void chooseDifficulty()
    {
        LevelManager.instance.gameDifficulty = gameDifficulty;
        manager.Validate();
    }
}
