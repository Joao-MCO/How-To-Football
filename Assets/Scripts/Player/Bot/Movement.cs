using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace HTF_Bot{
    public class Movement : MonoBehaviour
    {
        private Rigidbody2D _rigidbody;
        private Animator _animator;
        private SpriteRenderer _renderer;
        private float _horizontalMovement;
        public bool _isGrounded = true;
        public bool canShoot = false;
        private bool mayHead, mayJump, mayKick, mayLeft, mayRight;
        private Rigidbody2D _bola;
        private Vector2 _direction;
        
        private static readonly int IsJumping = Animator.StringToHash("IsJumping");
        private static readonly int Speed = Animator.StringToHash("Speed");
        private static readonly int Kick1 = Animator.StringToHash("Kick");
        private static readonly int Head1 = Animator.StringToHash("Head");
        private static readonly int Jump1 = Animator.StringToHash("Jump");

        [FormerlySerializedAs("type")]
        [Header("Player Properties")] 
        [Space] 
        [SerializeField] private PlayerType playerType;
        [SerializeField] private float speed = 200f;
        [SerializeField] private float jumpStrength = 10f;

        [Header("Kicking Properties")]
        [Space]
        [SerializeField] private float kickStrength = 15f;
        [SerializeField] private Cabeca cabeca;

        [Header("Sensor Properties")]
        [Space]
        [SerializeField] private Controller[] controllers;
        [SerializeField] private Direcao[] direcoes;

        // Setando componentes do Objeto
        private void Awake (){
            _rigidbody = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
            _renderer = GetComponent<SpriteRenderer>();
        }

        private void Visitante(){          
            if (_isGrounded && mayJump)
                JumpPlayer();

            if (mayKick){
                Kick();
            }

            if (mayHead)
                Head();

            if (mayLeft){
                _horizontalMovement = -1f;
                _renderer.flipX = false;
            }else if (mayRight){
                _horizontalMovement = 1f;
                _renderer.flipX = true;
            }else{
                _horizontalMovement = 0f;
            }
            
            cabeca.Orientacao(_renderer.flipX);
            MovePlayer();

        }

        // Movimenta o jogador
        private void MovePlayer (){
            Vector2 movement = new Vector2((_horizontalMovement * speed * Time.fixedDeltaTime),_rigidbody.velocity.y);
            _rigidbody.velocity = movement;
        }
        
        // Responsável pelo pulo do jogador
        private void JumpPlayer(){
            _animator.SetTrigger(Jump1);
            _rigidbody.AddForce(Vector2.up * jumpStrength, ForceMode2D.Impulse);
            AudioManager.Instance.Play("Pulo");
            _isGrounded = false;
        }
        
        // Responsável pelo chute do jogador
        private void Kick(){
            _animator.SetTrigger(Kick1);
        }

        private void FixedUpdate (){   
            if(GameManager.Instance.gameIsOn){
                mayLeft = direcoes[0].Verify();  
                mayRight = !mayLeft;   
                mayJump = direcoes[1].Verify();       
                mayKick = controllers[0].Verify();  
                mayHead = controllers[1].Verify();    
                _animator.SetBool(IsJumping, !_isGrounded);
                _animator.SetFloat(Speed, Mathf.Abs(_rigidbody.velocity.x));
                if(this.canShoot && _bola != null && !_renderer.flipX){
                    _bola.AddForce(kickStrength*_direction, ForceMode2D.Impulse);
                    AudioManager.Instance.Play("Chute"); 
                    canShoot = false;
                } 
                Visitante();
            }
        }

        private void Head(){
            _animator.SetTrigger(Head1);
        }

        private void OnCollisionEnter2D(Collision2D other) {
            if(other.gameObject.layer == 6 && gameObject.layer == 8 && gameObject.tag == "Bot"){
                _isGrounded = true;
            }
        }

        private void OnCollisionExit2D(Collision2D other) {
            if(other.gameObject.layer == 6 && gameObject.layer == 8 && gameObject.tag == "Bot"){
                _isGrounded = false;
            }
        }

        private void OnTriggerEnter2D(Collider2D other) {
            if(other.gameObject.layer == 9 && gameObject.layer == 8 && gameObject.tag == "Bot"){  
                _bola = other.gameObject.GetComponent<Rigidbody2D>();
            }
        }

        private void OnTriggerStay2D(Collider2D other) {
            if(other.gameObject.layer == 9 && gameObject.layer == 8 && gameObject.tag == "Bot") _direction = -(transform.position - other.gameObject.transform.position).normalized; 
        }

        private void OnTriggerExit2D(Collider2D other) {
            if(other.gameObject.layer == 9 && gameObject.layer == 8 && gameObject.tag == "Bot"){  
                _bola = null; 
            }
        }
    }
}
