using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bola : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    
    public float maxVelocity = 250f;
    private void Awake() {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    // void FixedUpdate(){
    //     if(_rigidbody.velocity.magnitude > maxVelocity)
    //         _rigidbody.velocity = _rigidbody.velocity.normalized * maxVelocity;
    // }
}
