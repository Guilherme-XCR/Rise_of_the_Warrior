using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;
using UnityEngine.SceneManagement;



public class GameController : MonoBehaviour
{
    
    [SerializeField] private GameObject P1;
    [SerializeField] private GameObject P2;

    [SerializeField] private int MaxRounds = 3;
    [SerializeField] private int roundAtual = 1;
    [SerializeField] private int winsP1 = 0;
    [SerializeField] private int winsP2 = 0;

    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private float maxTime = 180f;
    [SerializeField] private float time = 0;

    void Start()
    {
        P1 = GameObject.FindWithTag("Player1");
        P2 = GameObject.FindWithTag("Player2");
        time = maxTime;
    }

    void Update()
    {
        Timer();
        CheckWins();
    }

    public void ContRounds()
    {
        if(winsP1 > MaxRounds / 2)
        {
            //Player 1 ganho o jogo

        }else if(winsP2 > MaxRounds / 2)
        {
            //Player 2 ganho o jogo
        }

        roundAtual = winsP1 + winsP2 + 1;

    }

    public void CheckWins()
    {
        if (P1.GetComponent<PlayerController>().health <= 0)
        {
            //Player 1 morreu
            winsP2++;
            //restart ou win
            ContRounds();
            StartCoroutine(RestartRound());
        }
        else if (P2.GetComponent<PlayerController>().health <= 0)
        {
            //Player 2 morreu
            winsP1++;
            //restart ou win
            ContRounds();
            StartCoroutine(RestartRound());
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
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("Round");
    }



}
