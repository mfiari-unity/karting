using KartGame.KartSystems;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    public static LevelManager instance;

    public enum GameMode
    {
        BEAT_THE_CLOCK,
        CHECKPOINT,
        CRASH_COURSE,
        LAP
    }
    public GameMode gameMode;

    public enum GameLevel
    {
        ADDITIONAL,
        COUNTRY,
        MOUNTAIN,
        OVAL,
        WINDING
    }
    public GameLevel gameLevel;

    public enum GameDifficulty
    {
        EASY,
        NORMAL,
        HARD
    }
    public GameDifficulty gameDifficulty;

    public ArcadeKart arcadeKart;

    private List<GhostSave> recordedGhost = new List<GhostSave>();

    public string curentScene;
    public string lastScene;

    public float remainTime = 0f;

    public bool isWebPlayer = true;
    public bool isMobile = true;

    public string version;

    // Start is called before the first frame update
    void Awake()
    {
        // check if the instance existe
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        //set this to be not destroyable
        DontDestroyOnLoad(gameObject);

    }

    public void setGhost (List<GhostTransform> recordedGhostTransforms, float time)
    {
        for (int i = 0; i < recordedGhost.Count; i++)
        {
            GhostSave ghostSave = recordedGhost[i];
            if (ghostSave.gameDifficulty == gameDifficulty && ghostSave.gameLevel == gameLevel && ghostSave.gameMode == gameMode)
            {

                if (time < ghostSave.time)
                {
                    ghostSave.time = time;
                    ghostSave.recordedGhostTransforms = recordedGhostTransforms;
                }
                return;
            }
        }
        GhostSave newGhostSave = new GhostSave();
        newGhostSave.gameDifficulty = gameDifficulty;
        newGhostSave.gameLevel = gameLevel;
        newGhostSave.gameMode = gameMode;
        newGhostSave.time = time;
        newGhostSave.recordedGhostTransforms = recordedGhostTransforms;
        recordedGhost.Add(newGhostSave);
    }

    public List<GhostTransform> GetGhost()
    {
        for (int i = 0; i < recordedGhost.Count; i++)
        {
            GhostSave ghostSave = recordedGhost[i];
            if (ghostSave.gameDifficulty == gameDifficulty && ghostSave.gameLevel == gameLevel && ghostSave.gameMode == gameMode)
            {

                return ghostSave.recordedGhostTransforms;
            }
        }
        return null;
    }

    public bool hasGhost()
    {
        for (int i = 0; i < recordedGhost.Count; i++)
        {
            GhostSave ghostSave = recordedGhost[i];
            if (ghostSave.gameDifficulty == gameDifficulty && ghostSave.gameLevel == gameLevel && ghostSave.gameMode == gameMode)
            {

                return true;
            }
        }
        return false;
    }

    public void ClearGhost ()
    {
        for (int i = 0; i < recordedGhost.Count; i++)
        {
            GhostSave ghostSave = recordedGhost[i];
            if (ghostSave.gameDifficulty == gameDifficulty && ghostSave.gameLevel == gameLevel && ghostSave.gameMode == gameMode)
            {

                recordedGhost.RemoveAt(i);
            }
        }
    }
}
