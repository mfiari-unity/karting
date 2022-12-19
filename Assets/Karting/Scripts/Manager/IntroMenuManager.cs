using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class IntroMenuManager : MonoBehaviour
{
    
    [SerializeField]
    private UpdatePopup updatePopup;
    [SerializeField]
    private Button startButton;
    [SerializeField]
    private TMP_Text versionText;
    [SerializeField]
    private GameObject kartDisplay;
    [SerializeField]
    private HTTPWebGLRequest httpWebGLRequest;

    private HTTPRequest httpRequest;
    private string url = "http://fiarimike.fr/api/";

    // Start is called before the first frame update
    void Start()
    {
        updatePopup.gameObject.SetActive(false);
        versionText.text = "V" + LevelManager.instance.version;
        startButton.enabled = false;
        UpdatePopup.OnPopupClose += CloseUpdatePopup;
        UpdatePopup.OnPopupUpdate += CloseUpdatePopup;
        LoadLastGameVersion();


    }

    private void LoadLastGameVersion()
    {

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
        string version = httpRequest.GetLastVersion(url, LevelManager.instance.isMobile, LevelManager.instance.isWebPlayer);

        checkLastversion(version);
        
    }

    private IEnumerator LoadGameData()
    {
        yield return httpWebGLRequest.GetLastVersion(url, LevelManager.instance.isMobile, LevelManager.instance.isWebPlayer);

        if (httpWebGLRequest.responseCode == 200)
        {
            checkLastversion(httpWebGLRequest.jsonData);
        } else
        {
            startButton.enabled = true;
        }
        yield return null;
    }

    private void checkLastversion (string LastVersion)
    {
        if (hasNewVersion(LastVersion))
        {
            kartDisplay.SetActive(false);
            updatePopup.Init("http://fiarimike.fr/karting-gaming");
            updatePopup.Open("Nouvelle mise à jour", "Une nouvelle mise à jour est disponible [version "+LastVersion+"]", "Mettre à jour", "Annuler");
        } else
        {
            startButton.enabled = true;
        }
    }

    private void CloseUpdatePopup(UpdatePopup popup)
    {
        startButton.enabled = true;
        kartDisplay.SetActive(true);
    }

    private bool hasNewVersion (string lastVersion)
    {
        string[] lastVersionArray = lastVersion.Split('.');
        string[] curentVersionArray = LevelManager.instance.version.Split('.');

        if (lastVersionArray.Length == 3 && curentVersionArray.Length == 3)
        {
            if (int.TryParse(lastVersionArray[0], out int lastMajor) && int.TryParse(lastVersionArray[1], out int lastMinor) 
                && int.TryParse(lastVersionArray[2], out int lastPatch) && int.TryParse(curentVersionArray[0], out int currentMajor) 
                && int.TryParse(curentVersionArray[1], out int currentMinor) && int.TryParse(curentVersionArray[2], out int currentPatch))
            {
                if (lastMajor > currentMajor)
                {
                    return true;
                }
                if (lastMajor == currentMajor && lastMinor > currentMinor)
                {
                    return true;
                }
                if (lastMajor == currentMajor && lastMinor == currentMinor && lastPatch > currentPatch)
                {
                    return true;
                }
            }
            
        }
        return false;
    }
}
