using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;

public class GameScore : MonoBehaviour
{

    public PlayerScore[] playerScores;

    public InputField nameInputField;
    public GameObject gameScorePanel;
    public GameObject messagePanel;
    public GameObject namePanel;

    public HTTPWebGLRequest httpWebGLRequest;

    private string gameMode;
    private string gameLevel;
    private string gameDifficulty;
    private float remainTime;
    private UserInfo userInfo;

    private UserInfo[] users;

    private HTTPRequest httpRequest;
    private string url = "http://fiarimike.fr/api/karting-score.php";

    private string floatFormat = "0.00";

    private enum GameScoreState
    {
        INIT,
        LOADING,
        ERROR,
        LOADED,
        IDLE,
        SAVING,
        CLOSE
    }
    private GameScoreState state;

    // Start is called before the first frame update
    void Start()
    {
        state = GameScoreState.INIT;
        gameMode = LevelManager.instance.gameMode.ToString();
        gameLevel = LevelManager.instance.gameLevel.ToString();
        gameDifficulty = LevelManager.instance.gameDifficulty.ToString();
        remainTime = LevelManager.instance.remainTime;

        userInfo = new UserInfo();
        userInfo.gameTime = remainTime;

        httpRequest = new HTTPRequest();

        if (LevelManager.instance.isWebPlayer)
        {
            StartCoroutine(LoadGameData());
        } else
        {
            LoadAsyncGameData();
        }
    }

    void Update()
    {
        switch (state)
        {
            case GameScoreState.LOADED:
                state = GameScoreState.IDLE;
                StartCoroutine(ShowGameScore(users));
                break;
        }
    }

    public void MessagePanelYesButton()
    {
        namePanel.SetActive(true);
    }

    public void MessagePanelNoButton()
    {
        messagePanel.SetActive(false);
        StartCoroutine(CloseGameScore());
    }

    public void SaveData()
    {
        userInfo.name = nameInputField.text;

        string jsonGhost = getJsonGhost();

        if (LevelManager.instance.isWebPlayer)
        {
            httpWebGLRequest.SaveData(url, userInfo, gameMode, gameLevel, gameDifficulty, jsonGhost);
        } else
        {
            httpRequest.SaveData(url, userInfo, gameMode, gameLevel, gameDifficulty, jsonGhost);
        }

        StartCoroutine(CloseGameScore());
    }

    public void NamePanelCancel ()
    {
        namePanel.SetActive(false);
    }

    private string getJsonGhost ()
    {
        JsonGhostSave jsonGhostSave = new JsonGhostSave();
        List<GhostTransform> list = LevelManager.instance.GetGhost();
        if (list == null)
        {
            return "";
        }
        List<JsonGhostTransform> jsonList = new List<JsonGhostTransform>();
        foreach (GhostTransform ghostTransform in list)
        {
            JsonGhostTransform jsonGhostTransform = new JsonGhostTransform();

            JsonPosition jsonPosition = new JsonPosition();
            jsonPosition.x = ghostTransform.position.x;
            jsonPosition.y = ghostTransform.position.y;
            jsonPosition.z = ghostTransform.position.z;

            JsonRotation jsonRotation = new JsonRotation();
            jsonRotation.x = ghostTransform.rotation.x;
            jsonRotation.y = ghostTransform.rotation.y;
            jsonRotation.z = ghostTransform.rotation.z;
            jsonRotation.w = ghostTransform.rotation.w;

            jsonGhostTransform.position = jsonPosition;
            jsonGhostTransform.rotation = jsonRotation;

            jsonList.Add(jsonGhostTransform);
        }

        jsonGhostSave.ghostTransforms = jsonList.ToArray();

        return JsonUtility.ToJson(jsonGhostSave);
    }

    private void displayTime (TMP_Text tmp_text, float timeToDisplay)
    {
        tmp_text.text = timeToDisplay.ToString(floatFormat) + " s";
    }

    private void displayPlayerScore (PlayerScore playerScore, float time, string name, int place, bool highlight)
    {
        displayTime(playerScore.finishTime, time);
        playerScore.playerName.text = name;
        playerScore.place.text = place.ToString();
        if (highlight)
        {
            playerScore.Highlight();
        }
    }

    private IEnumerator LoadGameData ()
    {
        StartCoroutine(httpWebGLRequest.LoadData(url, gameMode, gameLevel, gameDifficulty));
        yield return new WaitForSeconds(1f);

        ParseJsonToUserInfo(httpWebGLRequest.jsonData);

        yield return null;
    }

    private async void LoadAsyncGameData()
    {
        await Task.Run(async () =>
        {
            await Task.Delay(1000);
            string json = httpRequest.LoadData(url, gameMode, gameLevel, gameDifficulty);

            ParseJsonToUserInfo(json);
        });

    }

    private void ParseJsonToUserInfo (string json)
    {
        try
        {
            RootUserInfo rootUser = JsonUtility.FromJson<RootUserInfo>("{\"users\":" + json + "}");
            users = rootUser.users;
        }
        catch (Exception e)
        {
            Debug.LogError(e);
            users = new UserInfo[0];
        }
        Array.Sort(users, delegate (UserInfo user1, UserInfo user2) {
            return user2.gameTime.CompareTo(user1.gameTime);
        });

        state = GameScoreState.LOADED;
    }

    private void ShowSaveMessage()
    {
        messagePanel.SetActive(true);
    }

    private IEnumerator CloseGameScore ()
    {
        yield return new WaitForSeconds(1f);

        gameObject.SetActive(false);
    }

    private IEnumerator ShowGameScore(UserInfo[] users)
    {
        int place = 0;
        if (users.Length == 0)
        {
            displayPlayerScore(playerScores[0], remainTime, "You", 1, true);
            for (int i = 1; i < playerScores.Length; i++)
            {
                playerScores[i].gameObject.SetActive(false);
            }
        }
        else if (users.Length < 5)
        {
            bool playerIsShow = false;
            for (int i = 0; i < users.Length; i++)
            {
                if (users[i].gameTime < remainTime && !playerIsShow)
                {
                    place = i + 1;
                    displayPlayerScore(playerScores[i], remainTime, "You", place, true);
                    playerIsShow = true;
                }
                int index = playerIsShow ? i + 1 : i;
                place = index + 1;
                displayPlayerScore(playerScores[index], users[i].gameTime, users[i].name, place, false);
            }
            if (!playerIsShow)
            {
                displayPlayerScore(playerScores[users.Length], remainTime, "You", place + 1, true);
            }
            for (int i = users.Length + 1; i < playerScores.Length; i++)
            {
                playerScores[i].gameObject.SetActive(false);
            }
        }
        else
        {
            // Get user place
            for (int i = 0; i < users.Length; i++)
            {
                if (users[i].gameTime < remainTime)
                {
                    place = i + 1;
                    break;
                }
            }
            if (place == 0)
            {
                place = users.Length + 1;
            }

            // check if between 3 and length -2 show five
            if (place > 2 && place < users.Length - 2)
            {
                bool playerIsShow = false;
                int playerScoreIndex = 0;
                for (int i = place - 2; i < place + 2; i++)
                {
                    if (users[i].gameTime < remainTime && !playerIsShow)
                    {
                        place = i + 1;
                        displayPlayerScore(playerScores[playerScoreIndex], remainTime, "You", place, true);
                        playerIsShow = true;
                        playerScoreIndex++;
                    }
                    int index = playerIsShow ? i + 1 : i;
                    place = index + 1;
                    displayPlayerScore(playerScores[playerScoreIndex], users[i].gameTime, users[i].name, place, false);
                    playerScoreIndex++;
                }
            }
            else if (place == 1 || place == 2)
            {
                // if 1 or 2 show under
                if (place == 1)
                {
                    displayPlayerScore(playerScores[0], remainTime, "You", 1, true);
                }
                else
                {
                    displayPlayerScore(playerScores[0], users[0].gameTime, users[0].name, 1, false);

                    displayPlayerScore(playerScores[1], remainTime, "You", 2, true);
                }
                for (int i = place; i < 5; i++)
                {
                    place = i + 1;
                    displayPlayerScore(playerScores[i], users[i].gameTime, users[i].name, place, false);
                }
            }
            else
            {
                // lenght -2 show above
                for (int i = users.Length - 4; i < users.Length - 1; i++)
                {
                    int playerPlace = i + 1;
                    int index = i - (users.Length - 4);
                    displayPlayerScore(playerScores[index], users[i].gameTime, users[i].name, playerPlace, false);
                }
                if (place == users.Length)
                {
                    displayPlayerScore(playerScores[3], remainTime, "You", place, true);

                    displayPlayerScore(playerScores[4], users[users.Length - 1].gameTime, users[users.Length - 1].name, users.Length+1, false);
                }
                else
                {
                    displayPlayerScore(playerScores[3], users[users.Length - 1].gameTime, users[users.Length - 1].name, users.Length, false);

                    displayPlayerScore(playerScores[4], remainTime, "You", place, true);
                }
            }
        }

        gameScorePanel.SetActive(true);

        yield return new WaitForSeconds(3f);

        ShowSaveMessage();
    }
}
