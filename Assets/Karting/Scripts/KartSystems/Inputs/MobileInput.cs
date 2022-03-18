using UnityEngine;

public class MobileInput : MonoBehaviour
{
    public Joystick joystick;

    private int verticalMove = 0;

    public void MoveForward()
    {
        verticalMove = 1;
    }
    public void MoveBackward()
    {
        verticalMove = -1;
    }
    public void StopMove()
    {
        verticalMove = 0;
    }

    public float GetVerticalMove ()
    {
        return joystick.Vertical + verticalMove;
    }

    public float GetHorizontalMove ()
    {
        return joystick.Horizontal;
    }
}
