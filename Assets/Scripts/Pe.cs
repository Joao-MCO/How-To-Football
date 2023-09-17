using UnityEngine;
using System;

namespace HTF{
    public class Pe : MonoBehaviour
    {
        private bool _isColliding = false;
        private bool _canShoot = false;
        private Rigidbody2D _bola;
        private Vector2 _direction;
        public bool CheckGround(){
            return _isColliding;
        }
        private void OnCollisionEnter2D(Collision2D other) {
            if(other.gameObject.layer == 6){
                _isColliding = true;
            }else if(other.gameObject.layer == 9){
               _bola = other.gameObject.GetComponent<Rigidbody2D>();
               _canShoot = true;
               _direction = -(transform.position - other.gameObject.transform.position).normalized;
            }
        }

        private void OnCollisionExit2D(Collision2D other) {
            if(other.gameObject.layer == 6){
                _isColliding = false;
            }            
            if(other.gameObject.layer == 9){
                _canShoot = false;
            }            
        }

        public Rigidbody2D GetBola(){
            return _bola;
        }

        public bool CanShoot(){
            return _canShoot;
        }

        public Vector2 GetDirection(){
            return _direction;
        }
    }
}
