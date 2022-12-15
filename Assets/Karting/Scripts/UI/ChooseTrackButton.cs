using UnityEngine;

public class ChooseTrackButton : MonoBehaviour
{

    public LevelManager.GameLevel gameLevel;
    public MenuSelectManager manager;


    public void ChooseTrack ()
    {
        LevelManager.instance.gameLevel = gameLevel;
        manager.Validate();

    }
}
