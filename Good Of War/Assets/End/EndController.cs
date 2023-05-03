using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class EndController : MonoBehaviour
{
    public void ReMatch()
    {
        SceneManager.LoadScene("Round");
    }
    public void Select_P()
    {
        SceneManager.LoadScene("Select_P");
    }
    public void Menu()
    {
        SceneManager.LoadScene("Start");
    }
}