using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputPlayer2 : IInput_Controller
{
    public override int MovementInput()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            return 1;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            return -1;
        }else
        {
            return 0;
        }
    }
    public override bool KeydownJump()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            return true;
        }
        return false;
    }
    public override bool KeyDownCrouch()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            return true;
        }
        return false;
    }
    public override bool KeyUpCrouch()
    {
        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            return true;
        }
        return false;
    }
    public override bool KeydownPunch()
    {
        if (Input.GetKeyDown(KeyCode.Comma))
        {
            return true;
        }
        return false;
    }
    public override bool KeydownKick()
    {
        if (Input.GetKeyDown(KeyCode.Period))
        {
            return true;
        }
        return false;
    }
    public override bool KeydownSpecialAttack()
    {
        if (Input.GetKeyDown(KeyCode.Semicolon))
        {
            return true;
        }
        return false;
    }
}
