using UnityEngine;
using System;

namespace HTF{
    public class Pe : MonoBehaviour
    {
        private bool _isColliding = false;
        public bool CheckGround(){
            return _isColliding;
        }
        private void OnCollisionEnter2D(Collision2D other) {
            if(other.gameObject.layer == 6){
                _isColliding = true;
            }
        }
    }
}
