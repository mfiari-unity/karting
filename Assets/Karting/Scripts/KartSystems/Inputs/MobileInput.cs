using UnityEngine;

public class MobileInput : MonoBehaviour
{
    public Joystick joystick;

    [Tooltip("Horizontal sensibility of the joystick when the input is trigger 0. A higher number means it move faster.")]
    [Range(0.2f, 1)]
    public float sensibility = 0.5f;

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
        return joystick.Horizontal * sensibility;
    }
}
