using UnityEngine;
using UnityEngine.UI;

public class GhostPlayer : MonoBehaviour
{

    [SerializeField]
    private GameObject player;
    [SerializeField]
    private Text playerButtonText;
    [SerializeField]
    private Text playerPositionText;
    [SerializeField]
    private Text playerTimeText;

    public void init (string name, string position, string time)
    {
        playerButtonText.text = name;
        playerPositionText.text = position;
        playerTimeText.text = time;
        player.SetActive(true);
    }

    public void hide ()
    {
        player.SetActive(false);
    }

}
