using UnityEngine;
using UnityEngine.SceneManagement;

public class ChooseTrackButton : MonoBehaviour
{

    public LevelManager.GameLevel gameLevel;
    

    public void ChooseTrack ()
    {
        LevelManager.instance.gameLevel = gameLevel;
        SceneManager.LoadSceneAsync(getScene());

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
