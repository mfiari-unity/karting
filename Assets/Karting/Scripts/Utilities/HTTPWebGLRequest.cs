﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class HTTPWebGLRequest : MonoBehaviour
{

    public string jsonData;

    public void SaveData(string url, UserInfo userinfo, string gameMode, string gameLevel, string gameDifficulty, string jsonGhost)
    {

        System.Globalization.CultureInfo customCulture = (System.Globalization.CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
        customCulture.NumberFormat.NumberDecimalSeparator = ".";

        System.Threading.Thread.CurrentThread.CurrentCulture = customCulture;

        var list = new List<KeyValuePair<string, string>>();

        list.Add(new KeyValuePair<string, string>("playerName", userinfo.name));
        list.Add(new KeyValuePair<string, string>("gameMode", gameMode));
        list.Add(new KeyValuePair<string, string>("gameLevel", gameLevel));
        list.Add(new KeyValuePair<string, string>("gameDifficulty", gameDifficulty));
        list.Add(new KeyValuePair<string, string>("finishTime", userinfo.gameTime.ToString("0.00")));
        list.Add(new KeyValuePair<string, string>("ghost", jsonGhost));

        StartCoroutine(sendPostData(url, list));
    }

    protected IEnumerator sendPostData(string url, List<KeyValuePair<string, string>> datas)
    {
        WWWForm form = new WWWForm();
        foreach (var element in datas)
        {
            form.AddField(element.Key, element.Value);
        }

        UnityWebRequest www = UnityWebRequest.Post(url, form);
        www.SetRequestHeader("Content-Type", "application/x-www-form-urlencoded");
        yield return www.SendWebRequest();

        if (www.responseCode != 200)
        {
            //Debug.Log(www.error);
        }
        else
        {
            //Debug.Log("Form upload complete!");
        }
    }



    public IEnumerator LoadData(string url, string gameMode, string gameLevel, string gameDifficulty)
    {
        //Debug.Log("LoadData");
        url = url + "?gameMode=" + gameMode + "&gameLevel=" + gameLevel + "&gameDifficulty=" + gameDifficulty;
        yield return GetJsonData(url);
    }

    protected IEnumerator GetJsonData(string url)
    {
        //Debug.Log(url);
        UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();

        if (www.responseCode != 200)
        {
            jsonData = "";
        }
        else
        {
            jsonData = www.downloadHandler.text;
        }
        //Debug.Log(jsonData);
        yield return null;
    }
}
