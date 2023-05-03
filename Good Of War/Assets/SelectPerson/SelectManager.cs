using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class SelectManager : MonoBehaviour
{
    [SerializeField] private SpriteRenderer Caera;
    [SerializeField] private SpriteRenderer Lorrigan;
    [SerializeField] private SpriteRenderer Ragnar;

    private SpriteRenderer[] listaSpriteRenderer;
    private string[] listaPersonagens;


    [SerializeField] private int p1_select;
    [SerializeField] private int p2_select;

    [SerializeField] private bool p1_confirm;
    [SerializeField] private bool p2_confirm;

    private void Start()
    {
        listaSpriteRenderer = new SpriteRenderer[3];
        listaSpriteRenderer[0] = Caera;
        listaSpriteRenderer[1] = Lorrigan;
        listaSpriteRenderer[2] = Ragnar;

        listaPersonagens = new string[3];
        listaPersonagens[0] = "Caera";
        listaPersonagens[1] = "Lorrigan";
        listaPersonagens[2] = "Ragnar";
    }

    private void Update()
    {
        Controller();
        Animation();
    }
    void Prev(int player)
    {
        if(player == 1)
        {
            if(p1_select > 0)
            {
                p1_select--;
            }
            else
            {
                p1_select = 2;
            }
        }
        else
        {
            if (p2_select > 0)
            {
                p2_select--;
            }
            else
            {
                p2_select = 2;
            }
        }
    }
    void Next(int player)
    {
        if (player == 1)
        {
            if (p1_select < 2)
            {
                p1_select++;
            }
            else
            {
                p1_select = 0;
            }
        }
        else
        {
            if (p2_select < 2)
            {
                p2_select++;
            }
            else
            {
                p2_select = 0;
            }
        }
    }

    void Controller()
    {
        if (Input.GetKeyDown(KeyCode.A) && !p1_confirm)
        {
            Prev(1);
        }
        if (Input.GetKeyDown(KeyCode.D) && !p1_confirm)
        {
            Next(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (p1_confirm)
            {
                p1_confirm = false;
            }
            else
            {
                p1_confirm = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow) && !p2_confirm)
        {
            Prev(2);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) && !p2_confirm)
        {
            Next(2);
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            if (p2_confirm)
            {
                p2_confirm = false;
            }
            else
            {
                p2_confirm = true;
            }
        }
    }
    void Animation()
    {
        for(int i = 0; i < 3; i++)
        {
            listaSpriteRenderer[i].color = Color.gray;
        }
        if(p1_select == p2_select)
        {
            if(p1_confirm && p2_confirm)
            {
                listaSpriteRenderer[p1_select].color = new Color(0, .5f,0);
            }
            else if(p1_confirm || p2_confirm)
            {
                listaSpriteRenderer[p1_select].color = new Color(0, .75f, 0);
            }
            else
            {
                listaSpriteRenderer[p1_select].color = Color.green;
            }

        }
        else
        {
            if (p1_confirm)
            {
                listaSpriteRenderer[p1_select].color = Color.blue;
            }
            else
            {
                listaSpriteRenderer[p1_select].color = Color.cyan;
            }

            if (p2_confirm)
            {
                listaSpriteRenderer[p2_select].color = new Color(1,.5f,0);
            }
            else
            {
                listaSpriteRenderer[p2_select].color = Color.yellow;
            }

        }
    }


    //---------------------BOTOES LIKE-----------------------
    [SerializeField] private string startScene;
    [SerializeField] private string jogarScene;

    public void Next()
    {
        if(p1_confirm && p2_confirm)
        {
           
            GameObject data = GameObject.Find("Data");
            DataController.p1_select = listaPersonagens[p1_select];
            DataController.p2_select = listaPersonagens[p2_select];
            DataController.p1_round = 0;
            DataController.p2_round = 0;

            SceneManager.LoadScene(jogarScene);
        }
    }
    public void Prev()
    {
        SceneManager.LoadScene(startScene);
    }
    //--------------------------------------------
}
