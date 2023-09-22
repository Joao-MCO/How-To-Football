using System.Text.RegularExpressions;
using System.Collections;
using System.Timers;
using System.Threading;
using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace HTF{
    public class GameManager : MonoBehaviour{
        public static GameManager Instance;

        [SerializeField] private TeamSO homeTeam, awayTeam;

        [SerializeField] private int scoreLimit = 5;
        
        public bool gameIsOn = true;

        public int ScoreHome { get; private set; } = 0;

        public int ScoreAway { get; private set; } = 0;

        public int coolTime = 3;


        [FormerlySerializedAs("type")]
        [Header("Game Objects")] 
        [Space] 
        [SerializeField] private GameObject bola;
        [SerializeField] private GameObject home;
        [SerializeField] private GameObject away;

        [Header("UI Objects")] 
        [Space] 
        [SerializeField] private GameObject gol;
        
        
        private void Awake (){
            if (Instance == null){
                Instance = this;
            }else{
                Destroy(gameObject, 1f);
                return;
            }
            DontDestroyOnLoad(gameObject);
            AudioManager.Instance.Play("Apito");
        }

        public void Score(MatchSide ms){
            if(ms == MatchSide.Away){
                ScoreHome += 1;
            }else{
                ScoreAway += 1;
            }
            StartCoroutine(Gol());
        }

        public void EndGame(String nome){
            gameIsOn = false;     
            UiManager.Instance.InTheEnd(nome);
            Destroy(bola);
            Destroy(home);
            Destroy(away);
        }
        private void Update() {
            UiManager.Instance.OnScore();
            if(ScoreAway == scoreLimit) EndGame(awayTeam.teamName);
            if (ScoreHome == scoreLimit) EndGame(homeTeam.teamName);
            if(Input.GetKeyUp(KeyCode.Escape)){
                if(gameIsOn){
                    BotoesManager.Instance.Pause();
                    gameIsOn = false;
                }else{
                    BotoesManager.Instance.UnPause();
                    gameIsOn = true;
                }
            }
        }

        public void Restart(){
            bola.transform.position = new Vector2(0f, 3f);
            home.transform.position = new Vector2(-5f, -2.65f);
            away.transform.position = new Vector2(5f, -2.65f);
        }

        IEnumerator Gol(){
            AudioManager.Instance.Play("Gol");
            if(scoreLimit != ScoreHome && scoreLimit != ScoreAway){
                gameIsOn = false;
                Restart();
                bola.GetComponent<Rigidbody2D>().gravityScale = 0f;
                bola.GetComponent<Rigidbody2D>().velocity = new Vector2(0f,0f);
                gol.SetActive(true);
                yield return new WaitForSeconds(3);
                gol.SetActive(false);
                gameIsOn = true;
                bola.GetComponent<Rigidbody2D>().gravityScale = 1f;
                AudioManager.Instance.Play("Apito");
            }
        }
    }
}