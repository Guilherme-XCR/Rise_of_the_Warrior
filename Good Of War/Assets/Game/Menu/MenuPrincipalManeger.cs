using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class MenuPrincipalManeger : MonoBehaviour
{

    [SerializeField] private string jogarScene;

    public void Jogar()
    {
        SceneManager.LoadScene(jogarScene);
    }

    public void Sair()
    {
        Application.Quit();
    }
}
