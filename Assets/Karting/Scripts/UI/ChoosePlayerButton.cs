using KartGame.KartSystems;
using UnityEngine;

public class ChoosePlayerButton : MonoBehaviour
{

    public ArcadeKart arcadeKart;
    public GameObject playerGO;
    public GameObject trackGO;
    public GameObject difficultyGO;

    public void choosePlayer ()
    {
        LevelManager.instance.arcadeKart = arcadeKart;
        playerGO.SetActive(false);
        trackGO.SetActive(true);
    }

    public void previousChoice()
    {
        playerGO.SetActive(false);
        difficultyGO.SetActive(true);
    }
}
