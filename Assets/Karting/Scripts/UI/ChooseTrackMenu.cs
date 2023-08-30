using UnityEngine;

public class ChooseTrackMenu : MonoBehaviour
{

    [SerializeField]
    private GameObject bonusStageTrack;
    [SerializeField]
    private GameObject raceTrack;


    public void init()
    {
        LevelManager.GameMode gameMode = LevelManager.instance.gameMode;

        if (LevelManager.GameMode.LAP.Equals(gameMode))
        {
            bonusStageTrack.SetActive(false);
            raceTrack.SetActive(true);
        } else
        {
            bonusStageTrack.SetActive(true);
            raceTrack.SetActive(false);
        }
    }
}
