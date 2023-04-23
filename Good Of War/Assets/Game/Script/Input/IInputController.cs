using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IInputController : MonoBehaviour
{
    public abstract int DirectionMove();
    public abstract bool Jump();
    public abstract bool Crouch();
    public abstract bool UnCrouch();
    public abstract bool BasicAttack();
    public abstract bool DistanceAttack();
    public abstract bool SpecialAttack();
}
