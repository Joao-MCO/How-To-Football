using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HTF{
    public class Bola : MonoBehaviour
    {
        private Rigidbody2D _rigidbody;

        private void Awake (){
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        // Movimento da Bola
        public void BallMove (Vector2 direction, float kickStrength){
            _rigidbody.AddForce(direction * kickStrength, ForceMode2D.Impulse);
        }

        // Verificação do contato da Bola
        private void OnCollisionEnter2D (Collision2D col) {
            switch (col.gameObject.tag){
                case "Crossbar":
                    GameManager.Instance.OnCrossbar?.Invoke();
                    break;
                case "Player":
                    GameManager.Instance.OnKick?.Invoke();
                    break;
                default:
                    break;
            }
        }
    }
}
