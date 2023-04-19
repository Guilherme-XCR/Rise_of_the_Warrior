using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(IInput_Controller))]

public class PlayerController : MonoBehaviour
{


    [SerializeField] GameObject myBuneco;
    IInput_Controller input;

    


    // Start is called before the first frame update
    void Start()
    {
        input = GetComponent<IInput_Controller>();
    }

    // Update is called once per frame
    void Update()
    {
        int directionMove = input.MovementInput();
        myBuneco.GetComponent<CharacterModel>().Flip();

        myBuneco.GetComponent<CharacterModel>().Movement(directionMove);

        if (input.KeydownJump())
        {
            myBuneco.GetComponent<CharacterModel>().Jump(0);
        }

        if (input.KeyDownCrouch())
        {
            myBuneco.GetComponent<CharacterModel>().Crouch();
        }

        if (input.KeyUpCrouch())
        {
            myBuneco.GetComponent<CharacterModel>().UnCrouch();
        }

        if (input.KeydownAttack1())
        {
            myBuneco.GetComponent<CharacterModel>().Attack1();
        }

        if (input.KeydownAttack2())
        {
            myBuneco.GetComponent<CharacterModel>().Attack2();
        }

        if (input.KeydownAttack3())
        {
            myBuneco.GetComponent<CharacterModel>().Attack3();
        }


    }
}
