using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HTF{
    public class Cabeca : MonoBehaviour
    {
        
        public bool canShoot = false;
        private Rigidbody2D _bola;        
        private Vector2 _direction;

        public float headStrength = 5f;

        public void OnHead(){
            if(canShoot){
                _bola.AddForce(headStrength*_direction, ForceMode2D.Impulse);
            }  
        }

        private void OnTriggerEnter2D(Collider2D other) {
            if(other.gameObject.layer == 9){    
                _bola = other.gameObject.GetComponent<Rigidbody2D>();
                canShoot = true;
            }
        }

        private void OnTriggerStay2D(Collider2D other) {
            if(other.gameObject.layer == 9) _direction = -(transform.position - other.gameObject.transform.position).normalized; 
        }
        
        private void OnTriggerExit2D(Collider2D other) {
            if(other.gameObject.layer == 9){  
                _bola = null; 
                canShoot = false;
            }
        }
    }
}