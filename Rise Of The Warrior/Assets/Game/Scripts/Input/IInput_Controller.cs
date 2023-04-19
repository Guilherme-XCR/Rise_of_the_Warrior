using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IInput_Controller : MonoBehaviour
{
    public abstract int MovementInput();            // entrada de movimento
    public abstract bool KeydownJump();             // apertar a tecla de pulo
    public abstract bool KeyDownCrouch();           // apertar a tecla de agachar
    public abstract bool KeyUpCrouch();           // soltar a tecla de agachar
    public abstract bool KeydownAttack1();            // apertar a tecla de ataque 1
    public abstract bool KeydownAttack2();            // apertar a tecla de ataque 2
    public abstract bool KeydownAttack3();     // apertar a tecla de ataque 3
}
