using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputControllerPlayer2 : IInputController
{
    public override int DirectionMove()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            //direita
            return 1;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            //esquerda
            return -1;
        }
        //parado
        return 0;
    }
    public override bool Jump()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            return true;
        }
        return false;
    }
    public override bool Crouch()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            return true;
        }
        return false;
    }
    public override bool UnCrouch()
    {
        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            return true;
        }
        return false;
    }
    public override bool BasicAttack()
    {
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            return true;
        }
        return false;
    }
    public override bool DistanceAttack()
    {

        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            return true;
        }
        return false;

    }
    public override bool SpecialAttack()
    {
        if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            return true;
        }
        return false;
    }
}