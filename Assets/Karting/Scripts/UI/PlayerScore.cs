
using UnityEngine;
using TMPro;

public class PlayerScore : MonoBehaviour
{

    public TMP_Text place;
    public TMP_Text playerName;
    public TMP_Text finishTime;

    private UserInfo userInfo;

    public void setUserInfo (UserInfo userInfo)
    {
        this.userInfo = userInfo;
        playerName.text = userInfo.name;
        finishTime.text = userInfo.gameTime.ToString();
    }

    public void Highlight ()
    {
        BlinkingImage blinkingImage = GetComponentInChildren<BlinkingImage>();
        if (blinkingImage != null)
        {
            blinkingImage.enableBlinking();
        }
    }
}
