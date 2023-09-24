using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HTF_Bot{
    public class Cabeca : MonoBehaviour
    {
        
        public bool canShoot = false;
        private Rigidbody2D _bola;        
        private Vector2 _direction;

        private bool lado = false;

        public float headStrength = 5f;

        private void OnTriggerStay2D(Collider2D other) {
            if(other.gameObject.layer == 9 && gameObject.layer == 8 && gameObject.tag == "Bot") _direction = -(transform.position - other.gameObject.transform.position).normalized; 
        }

        private void OnTriggerEnter2D(Collider2D other) {
            if(other.gameObject.layer == 9 && gameObject.layer == 8 && gameObject.tag == "Bot"){  
                _bola = other.gameObject.GetComponent<Rigidbody2D>();
            }
        }

        private void OnTriggerExit2D(Collider2D other) {
            if(other.gameObject.layer == 9 && gameObject.layer == 8 && gameObject.tag == "Bot"){  
                _bola = null; 
            }
        }

        public void Orientacao(bool orientacao){
            lado = orientacao;
        }

        private void Update() {
            if(GameManager.Instance.gameIsOn){
                if(this.canShoot && _bola != null && !lado){
                    _bola.AddForce(headStrength*_direction, ForceMode2D.Impulse);
                    AudioManager.Instance.Play("Chute"); 
                    canShoot = false;
                } 
            }
        }
    }
}