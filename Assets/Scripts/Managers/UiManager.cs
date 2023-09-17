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

    }
}
