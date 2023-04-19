using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputPlayer1 : IInput_Controller
{
    public override int MovementInput()
    {
        if (Input.GetKey(KeyCode.D))
        {
            return 1;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            return -1;
        }else
        {
            return 0;
        }
    }
    public override bool KeydownJump()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            return true;
        }
        return false;
    }
    public override bool KeyDownCrouch()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            return true;
        }
        return false;
    }
    public override bool KeyUpCrouch()
    {
        if (Input.GetKeyUp(KeyCode.S))
        {
            return true;
        }
        return false;
    }

    public override bool KeydownAttack1()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            return true;
        }
        return false;
    }
    public override bool KeydownAttack2()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            return true;
        }
        return false;
    }
    public override bool KeydownAttack3()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            return true;
        }
        return false;
    }
}
