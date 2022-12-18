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
    private HTTPWebGLRequest httpWebGLRequest;

    private HTTPRequest httpRequest;
    private string url = "http://fiarimike.fr/api/";

    // Start is called before the first frame update
    void Start()
    {
        float version = LevelManager.instance.version;
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
        float version = httpRequest.GetLastVersion(url, LevelManager.instance.isMobile, LevelManager.instance.isWebPlayer);

        checkLastversion(version);
        
    }

    private IEnumerator LoadGameData()
    {
        yield return httpWebGLRequest.GetLastVersion(url, LevelManager.instance.isMobile, LevelManager.instance.isWebPlayer);

        if (httpWebGLRequest.responseCode == 200)
        {
            if (!float.TryParse(httpWebGLRequest.jsonData, out float version))
            {
                checkLastversion(version);
            } else
            {
                startButton.enabled = true;
            }
        } else
        {
            startButton.enabled = true;
        }
        yield return null;
    }

    private void checkLastversion (float LastVersion)
    {
        if (LastVersion > LevelManager.instance.version)
        {
            updatePopup.Open("Nouvelle mise à jour", "Une nouvelle mise à jour est disponible [version "+LastVersion+"]", "Mettre à jour", "Annuler");
        } else
        {
            startButton.enabled = true;
        }
    }

    private void CloseUpdatePopup(UpdatePopup popup)
    {
        startButton.enabled = true;
    }
}
