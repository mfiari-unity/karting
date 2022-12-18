using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using System.Collections.Generic;

public class MenuSelectManager : MonoBehaviour
{

    [SerializeField]
    private GameObject[] choosePanels;
    [SerializeField]
    private string[] titles;
    [SerializeField]
    private TMP_Text title;
    [SerializeField]
    private GhostPlayer[] players;
    [SerializeField]
    private HTTPWebGLRequest httpWebGLRequest;

    private int chooseIndex = 0;

    private HTTPRequest httpRequest;
    private string url = "http://fiarimike.fr/api/karting-score.php";

    private string gameMode;
    private string gameLevel;
    private string gameDifficulty;

    private UserInfo[] users;

    private enum MenuSelectState
    {
        LOADING,
        ERROR,
        LOADED,
        IDLE
    }
    private MenuSelectState state;

    // Start is called before the first frame update
    void Start()
    {
        title.text = titles[chooseIndex];
        choosePanels[chooseIndex].SetActive(true);
        state = MenuSelectState.IDLE;
    }

    // Start is called before the first frame update
    void Update()
    {
        if (MenuSelectState.LOADED.Equals(state))
        {
            state = MenuSelectState.IDLE;
            DisplayGhosts();
        } else if (MenuSelectState.ERROR.Equals(state))
        {
            state = MenuSelectState.IDLE;
            ClearGhosts();
        }
    }

    public void Validate ()
    {
        if (chooseIndex >= choosePanels.Length -1)
        {
            SceneManager.LoadSceneAsync(getScene());
        } else
        {
            choosePanels[chooseIndex].SetActive(false);
            chooseIndex++;
            choosePanels[chooseIndex].SetActive(true);
            title.text = titles[chooseIndex];
            if (chooseIndex == 4)
            {
                GetGhosts();
            }
        }
    }

    public void Previous ()
    {
        if (chooseIndex > 0)
        {
            choosePanels[chooseIndex].SetActive(false);
            chooseIndex--;
            choosePanels[chooseIndex].SetActive(true);
        } else
        {
            SceneManager.LoadSceneAsync("IntroMenu");
        }
    }

    public void SelectGhost (int index)
    {
        if (index == -1)
        {
            LevelManager.instance.ClearGhost();
        } else if (index >= 0 && index < users.Length)
        {
            LevelManager.instance.setGhost(getGhostTransform(users[index].ghost), users[index].gameTime);
        }
    }

    private List<GhostTransform> getGhostTransform(string jsonGhost)
    {
        JsonGhostSave jsonGhostSave = JsonUtility.FromJson<JsonGhostSave>(jsonGhost);
        List<GhostTransform> list = new List<GhostTransform>();
        if (jsonGhostSave == null)
        {
            return null;
        }

        foreach (JsonGhostTransform jsonGhostTransform in jsonGhostSave.ghostTransforms)
        {
            GhostTransform ghostTransform = new GhostTransform();
            ghostTransform.position = new Vector3(jsonGhostTransform.position.x, jsonGhostTransform.position.y, jsonGhostTransform.position.z);
            ghostTransform.rotation = new Quaternion(jsonGhostTransform.rotation.x, jsonGhostTransform.rotation.y, jsonGhostTransform.rotation.z, jsonGhostTransform.rotation.w);

            list.Add(ghostTransform);
        }

        return list;
    }

    public void GetGhosts ()
    {
        LoadGhost();
    }

    private void DisplayGhosts ()
    {
        for (int i = 0; i < players.Length; i++)
        {
            if (users.Length > i)
            {
                string place = "";
                if (i == 0)
                {
                    place = "1st";
                } else if (i == 1)
                {
                    place = "2nd";
                }
                if (i == 2)
                {
                    place = "3rd";
                }
                players[i].init(users[i].name, place, users[i].gameTime.ToString());
            } else
            {
                players[i].hide();
            }
        }
    }

    private void ClearGhosts ()
    {
        LevelManager.instance.ClearGhost();
    }

    private string getScene()
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

    private void LoadGhost ()
    {
        gameMode = LevelManager.instance.gameMode.ToString();
        gameLevel = LevelManager.instance.gameLevel.ToString();
        gameDifficulty = LevelManager.instance.gameDifficulty.ToString();

        state = MenuSelectState.LOADING;

        httpRequest = new HTTPRequest();

        if (LevelManager.instance.isWebPlayer)
        {
            StartCoroutine(LoadGameData());
        }
        else
        {
            LoadAsyncGameData();
        }
    }

    private void LoadAsyncGameData()
    {
        string json = httpRequest.LoadData(url, gameMode, gameLevel, gameDifficulty);

        ParseJsonToUserInfo(json);
    }

    private IEnumerator LoadGameData()
    {
        yield return httpWebGLRequest.LoadData(url, gameMode, gameLevel, gameDifficulty);

        if (httpWebGLRequest.responseCode == 200)
        {
            ParseJsonToUserInfo(httpWebGLRequest.jsonData);
        } else
        {
            users = new UserInfo[0];
            state = MenuSelectState.ERROR;
        }

        yield return null;
    }

    private void ParseJsonToUserInfo(string json)
    {
        try
        {
            RootUserInfo rootUser = JsonUtility.FromJson<RootUserInfo>("{\"users\":" + json + "}");
            users = rootUser.users;
            state = MenuSelectState.LOADED;
        }
        catch (Exception e)
        {
            users = new UserInfo[0];
            state = MenuSelectState.ERROR;
        }
        Array.Sort(users, delegate (UserInfo user1, UserInfo user2) {
            return user2.gameTime.CompareTo(user1.gameTime);
        });
    }
}
