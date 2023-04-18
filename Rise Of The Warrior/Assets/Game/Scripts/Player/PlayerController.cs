using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(IInput_Controller))]

public class PlayerController : MonoBehaviour
{


    [SerializeField] GameObject myBuneco;
    IInput_Controller ip1;

    


    // Start is called before the first frame update
    void Start()
    {
        ip1 = GetComponent<IInput_Controller>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ip1.KeydownJump())
        {
            myBuneco.GetComponent<CharacterModel>().Jump(0);
        }
        
    }
}
