using UnityEngine;

public class ChoosePlayModeButton : MonoBehaviour
{

    public MenuSelectManager manager;
    public LevelManager.GameMode gameMode;

    public void chooseGameMode()
    {
        LevelManager.instance.gameMode = gameMode;
        manager.Validate();
    }
}
