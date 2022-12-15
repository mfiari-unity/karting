using UnityEngine;

public class ChooseGhostButton : MonoBehaviour
{

    public int index;
    public MenuSelectManager manager;

    public void chooseGhost()
    {
        manager.SelectGhost(index);
        manager.Validate();
    }
}
