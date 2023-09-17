using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HTF{
    public class Gol : MonoBehaviour{
        [SerializeField] private MatchSide goalSide;
        private void OnTriggerEnter2D(Collider2D col){
            if (!GameManager.Instance.gameIsOn)
                return;
            
            if (col.gameObject.CompareTag("Bola")){
                GameManager.Instance.OnScoreNoCallback?.Invoke(goalSide);
            }
        }
    }
}

