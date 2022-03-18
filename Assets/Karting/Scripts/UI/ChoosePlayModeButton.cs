using UnityEngine;

public class ChoosePlayModeButton : MonoBehaviour
{

    public GameObject playModeGO;
    public GameObject difficultyGO;
    public LevelManager.GameMode gameMode;

    public void chooseGameMode()
    {
        LevelManager.instance.gameMode = gameMode;
        playModeGO.SetActive(false);
        difficultyGO.SetActive(true);
    }
}
