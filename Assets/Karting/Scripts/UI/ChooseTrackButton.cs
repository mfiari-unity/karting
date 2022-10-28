using UnityEngine;
using UnityEngine.SceneManagement;

public class ChooseTrackButton : MonoBehaviour
{

    public LevelManager.GameLevel gameLevel;
    public GameObject playerGO;
    public GameObject trackGO;


    public void ChooseTrack ()
    {
        LevelManager.instance.gameLevel = gameLevel;
        SceneManager.LoadSceneAsync(getScene());

    }

    public void previousChoice()
    {
        trackGO.SetActive(false);
        playerGO.SetActive(true);
    }

    public string getScene ()
    {
        switch (LevelManager.instance.gameMode)
        {
            case LevelManager.GameMode.BEAT_THE_CLOCK:
                return "BeatTheClockScene";
            case LevelManager.GameMode.CHECKPOINT:
                return "CheckpointsScene";
            case LevelManager.GameMode.CRASH_COURSE:
                return "CrashCourseScene";
            case LevelManager.GameMode.LAP:
                return "LapScene";
        }
        return "CheckpointsScene";
    }
}
