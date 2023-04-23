using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputControllerPlayer1 : IInputController
{
    public override int DirectionMove()
    {
        if (Input.GetKey(KeyCode.D))
        {
            //direita
            return 1;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            //esquerda
            return -1;
        }
        //parado
        return 0;
    }
    public override bool Jump()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            return true;
        }
        return false;
    }
    public override bool Crouch()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            return true;
        }
        return false;
    }
    public override bool UnCrouch()
    {
        if (Input.GetKeyUp(KeyCode.S))
        {
            return true;
        }
        return false;
    }
    public override bool BasicAttack()
    { 
        if (Input.GetKeyDown(KeyCode.F))
        {
            return true;
        }
        return false;
    }
    public override bool DistanceAttack()
    {

        if (Input.GetKeyDown(KeyCode.G))
        {
            return true;
        }
        return false;

    }
    public override bool SpecialAttack()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            return true;
        }
        return false;
    }
}
