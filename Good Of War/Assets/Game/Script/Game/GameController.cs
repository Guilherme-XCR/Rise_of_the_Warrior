using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;
using UnityEngine.SceneManagement;



public class GameController : MonoBehaviour
{
    

    [SerializeField] private GameObject P1;
    [SerializeField] private GameObject P2;

    [SerializeField] private int roundAtual;
    [SerializeField] private int winsP1 = 0;
    [SerializeField] private int winsP2 = 0;

    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private float maxTime = 180f;
    [SerializeField] private float time = 0;

    [SerializeField] private SpriteRenderer p1_coin1;
    [SerializeField] private SpriteRenderer p1_coin2;

    [SerializeField] private SpriteRenderer p2_coin1;
    [SerializeField] private SpriteRenderer p2_coin2;

    [SerializeField] private TextMeshProUGUI roundText;

    private bool checkRoundWinsCooldown = false;

    void Start()
    {
        LoadData();

        roundAtual = winsP1 + winsP2 + 1;
        
        P1 = GameObject.FindWithTag("Player1");
        P2 = GameObject.FindWithTag("Player2");
        
        time = maxTime;
    }

    void Update()
    {
        Timer();
        CheckWins();
        Hud();
    }
    
    void LoadData()
    {
        P1.GetComponent<PlayerController>().animator = DataController.p1_select;
        P2.GetComponent<PlayerController>().animator = DataController.p2_select;
        winsP1 = DataController.p1_round;
        winsP2 = DataController.p2_round;
    }
    
    void Hud()
    {
        if(winsP1 == 0)
        {
            p1_coin1.color = Color.gray;
            p1_coin2.color = Color.gray;
        }else if(winsP1 == 1)
        {
            p1_coin1.color = Color.white;
            p1_coin2.color = Color.gray;
        }
        else
        {
            p1_coin1.color = Color.white;
            p1_coin2.color = Color.white;
        }

        if (winsP2 == 0)
        {
            p2_coin1.color = Color.gray;
            p2_coin2.color = Color.gray;
        }
        else if (winsP2 == 1)
        {
            p2_coin1.color = Color.white;
            p2_coin2.color = Color.gray;
        }
        else
        {
            p2_coin1.color = Color.white;
            p2_coin2.color = Color.white;
        }

        roundText.text = "Round " + roundAtual.ToString();
    }

    public void CheckGameWins()
    {
        if(winsP1 >= 2)
        {
            //Player 1 ganho o jogo
            DataController.p1_round = 0;
            DataController.p2_round = 0;
            //ir para tela de vitoria p1 criar cena
            SceneManager.LoadScene("EndP1");


        }
        else if(winsP2 >= 2)
        {
            //Player 2 ganho o jogo
            DataController.p1_round = 0;
            DataController.p2_round = 0;
            //ir para tela de vitoria p2
            SceneManager.LoadScene("EndP2");

        }

    }

    public void CheckWins()
    {
        if (!checkRoundWinsCooldown)
        {
            checkRoundWinsCooldown = true;
            StartCoroutine(CheckWinsRotine());
        }
    }

    public void Timer()
    {
        time -= Time.deltaTime;
        int minutos = Mathf.FloorToInt(time / 60f);
        int segundos = Mathf.RoundToInt(time % 60f);
        if (time <= 0)
        {
            // O tempo acabou, faça o que for necessário.
            winsP1++;
            winsP2++;
            StartCoroutine(RestartRound());
        }else if (minutos < 1)
        {
            timerText.text = string.Format("{0:00}", segundos);
        }
        else
        {
            timerText.text = string.Format("{0:00}:{1:00}", minutos, segundos);
        }
    }
    IEnumerator RestartRound()
    {
        yield return new WaitForSeconds(2f); ;

        SceneManager.LoadScene("Round");
    }
    IEnumerator CheckWinsRotine()
    {
        if (P1.GetComponent<PlayerController>().health <= 0)
        {
            //Player 1 morreu
            DataController.p2_round++;
            winsP2++;
            //restart ou win
            CheckGameWins();
            StartCoroutine(RestartRound());
        }
        else if (P2.GetComponent<PlayerController>().health <= 0)
        {
            //Player 2 morreu
            DataController.p1_round++;
            winsP1++;
            //restart ou win
            CheckGameWins();
            StartCoroutine(RestartRound());
        }
        yield return new WaitForSeconds(2f);
        checkRoundWinsCooldown = false;
    }
}
