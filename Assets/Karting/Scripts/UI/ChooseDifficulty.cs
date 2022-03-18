using UnityEngine;

public class ChooseDifficulty : MonoBehaviour
{

    public GameObject difficultyGO;
    public GameObject playerGO;
    public LevelManager.GameDifficulty gameDifficulty;

    public void chooseDifficulty()
    {
        LevelManager.instance.gameDifficulty = gameDifficulty;
        difficultyGO.SetActive(false);
        playerGO.SetActive(true);
    }
}
