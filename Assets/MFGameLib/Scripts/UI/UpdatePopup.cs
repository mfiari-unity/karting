using UnityEngine;
using UnityEngine.UI;
using System;

public class UpdatePopup : MonoBehaviour
{

    [SerializeField]
    private GameObject updatePopUpPanel;
    [SerializeField]
    private Text titleText;
    [SerializeField]
    private Text contentText;
    [SerializeField]
    private Text okButton;
    [SerializeField]
    private Text cancelButton;

    private string downloadUrl;

    public static event Action<UpdatePopup> OnPopupClose;
    public static event Action<UpdatePopup> OnPopupUpdate;

    public void Init(string url)
    {
        downloadUrl = url;
    }

    public void Open (string title, string content, string okLabel, string cancelLabel)
    {
        titleText.text = title;
        contentText.text = content;
        okButton.text = okLabel;
        cancelButton.text = cancelLabel;
        updatePopUpPanel.gameObject.SetActive(true);
    }

    public void CloseButton ()
    {
        updatePopUpPanel.gameObject.SetActive(false);
        OnPopupClose.Invoke(this);
    }

    public void UpdateButton ()
    {
        Application.OpenURL(downloadUrl);
        OnPopupUpdate.Invoke(this);
    }
}
