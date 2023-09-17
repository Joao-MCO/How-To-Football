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

        
        private void Awake (){
            if (Instance == null){
                Instance = this;
            }else{
                Destroy(gameObject, 1f);
                return;
            }
            DontDestroyOnLoad(gameObject);
        }

        public void Score(MatchSide ms){
            Restart();
            gameIsOn = false;
            float ini = 0;
            if(ms == MatchSide.Away){
                ScoreHome += 1;
            }else{
                ScoreAway += 1;
            }
            gameIsOn = true;
        }

        public void EndGame(){
            if(ScoreHome == scoreLimit){
                gameIsOn = false;
            }else if(ScoreAway == scoreLimit){
                gameIsOn = false;                
            }else{
                return;
            }
        }
        private void Update() {
            UiManager.Instance.OnScore();
            EndGame();
        }

        public void Restart(){
            bola.transform.position = new Vector2(0f, 3f);
            home.transform.position = new Vector2(-5f, -2.65f);
            away.transform.position = new Vector2(5f, -2.65f);
        }
    }
}