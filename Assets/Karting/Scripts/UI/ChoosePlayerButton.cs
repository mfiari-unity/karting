using KartGame.KartSystems;
using UnityEngine;

public class ChoosePlayerButton : MonoBehaviour
{

    public ArcadeKart arcadeKart;
    public MenuSelectManager manager;

    public void choosePlayer ()
    {
        LevelManager.instance.arcadeKart = arcadeKart;
        manager.Validate();
    }
}
