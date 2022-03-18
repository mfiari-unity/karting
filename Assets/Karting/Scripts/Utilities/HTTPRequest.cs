using System.Net;
using System.Collections.Generic;
using System.Text;
using System.IO;

public class HTTPRequest
{

    public void SaveData (string url, UserInfo userinfo, string gameMode, string gameLevel, string gameDifficulty, string jsonGhost)
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

        var response = sendPostData(url, list);

        var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
    }

    protected HttpWebResponse sendPostData(string url, List<KeyValuePair<string, string>> datas)
    {
        var request = (HttpWebRequest)WebRequest.Create(url);

        var postData = "";
        var separator = "";
        foreach (var element in datas)
        {
            postData += separator + element.Key + "=" + element.Value;
            separator = "&";
        }

        var data = Encoding.ASCII.GetBytes(postData);

        request.Method = "POST";
        request.ContentType = "application/x-www-form-urlencoded";
        request.ContentLength = data.Length;

        using (var stream = request.GetRequestStream())
        {
            stream.Write(data, 0, data.Length);
        }

        return (HttpWebResponse)request.GetResponse();
    }



    public string LoadData (string url, string gameMode, string gameLevel, string gameDifficulty)
    {
        url = url + "?gameMode=" + gameMode + "&gameLevel=" + gameLevel + "&gameDifficulty=" + gameDifficulty;
        return GetJsonData(url);
    }

    protected string GetJsonData (string url)
    {
        var json = "";
        using (WebClient wc = new WebClient())
        {
            json = wc.DownloadString(url);
        }
        return json;
    }
}
