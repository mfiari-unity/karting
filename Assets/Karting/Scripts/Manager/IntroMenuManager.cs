using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class IntroMenuManager : MonoBehaviour
{
    
    [SerializeField]
    private UpdatePopup updatePopup;
    [SerializeField]
    private Button startButton;
    [SerializeField]
    private TMP_Text versionText;

    // Start is called before the first frame update
    void Start()
    {
        string version = LevelManager.instance.version;
        updatePopup.gameObject.SetActive(false);
        versionText.text = "V" + version;
        bool hasMaj = false;
        startButton.enabled = false;
        UpdatePopup.OnPopupClose += CloseUpdatePopup;
        UpdatePopup.OnPopupUpdate += CloseUpdatePopup;
        if (hasMaj)
        {
            updatePopup.Open("Nouvelle mise à jour", "Une nouvelle mise à jour est disponible", "Mettre à jour", "Annuler");
        }
    }

    private void CloseUpdatePopup(UpdatePopup popup)
    {
        startButton.enabled = true;
    }
}
