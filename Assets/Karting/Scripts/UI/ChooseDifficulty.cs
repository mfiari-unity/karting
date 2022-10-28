using UnityEngine;

public class ChooseDifficulty : MonoBehaviour
{

    public GameObject difficultyGO;
    public GameObject playerGO;
    public GameObject playModeGO;
    public LevelManager.GameDifficulty gameDifficulty;

    public void chooseDifficulty()
    {
        LevelManager.instance.gameDifficulty = gameDifficulty;
        difficultyGO.SetActive(false);
        playerGO.SetActive(true);
    }

    public void previousChoice ()
    {
        difficultyGO.SetActive(false);
        playModeGO.SetActive(true);
    }
}
