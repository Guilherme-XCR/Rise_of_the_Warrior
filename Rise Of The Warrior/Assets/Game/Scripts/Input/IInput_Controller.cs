using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IInput_Controller : MonoBehaviour
{
    public abstract int MovementInput();            // entrada de movimento
    public abstract bool KeydownJump();             // apertar a tecla de pulo
    public abstract bool KeyDownCrouch();           // apertar a tecla de agachar
    public abstract bool KeyUpCrouch();           // soltar a tecla de agachar
    public abstract bool KeydownPunch();            // apertar a tecla de soco
    public abstract bool KeydownKick();             // apertar a tecla de chute
    public abstract bool KeydownSpecialAttack();    // apertar a tecla de ataque especial
}
