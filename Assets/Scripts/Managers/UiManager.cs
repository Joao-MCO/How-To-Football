using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace HTF{
    public class UiManager : MonoBehaviour
    {
        public static UiManager Instance;
        public Image[] Mandante = new Image[6];
        public Image[] Visitante = new Image[6];

        public GameObject placar;
        public GameObject vencedor;
        public GameObject brasil, argentina;

        private void Awake (){
            if (Instance == null){
                Instance = this;
            }else{
                Destroy(gameObject, 1f);
                return;
            }
            DontDestroyOnLoad(gameObject);
        }
            
        public void OnScore() {
            int home = GameManager.Instance.ScoreHome;
            int away = GameManager.Instance.ScoreAway;
            if(home > 0){
                Mandante[home].gameObject.SetActive(true);
                Mandante[home-1].gameObject.SetActive(false);
            }
            if(away > 0){
                Visitante[away].gameObject.SetActive(true);
                Visitante[away-1].gameObject.SetActive(false);
            }
        }

        public void InTheEnd(string name){
            placar.SetActive(false);
            vencedor.SetActive(true);
            if(GameManager.Instance.ScoreHome > GameManager.Instance.ScoreAway) brasil.SetActive(true);
            else argentina.SetActive(true);
        }
    }
}
