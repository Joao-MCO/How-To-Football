using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEditor.Animations;

namespace HTF{
    public class Movement : MonoBehaviour 
    {
        private Rigidbody2D _rigidbody;
        private Animator _animator;
        private float _horizontalMovement;
        public bool _isGrounded = true;
        public bool canShoot = false;
        private Rigidbody2D _bola;
        private Vector2 _direction;

        private static readonly int IsJumping = Animator.StringToHash("IsJumping");
        private static readonly int Speed = Animator.StringToHash("Speed");
        private static readonly int Kick1 = Animator.StringToHash("Kick");
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


        // Setando componentes do Objeto
        private void Awake (){
            _rigidbody = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
        }

        // Movimentação do Mandante
        private void Mandante(){
            if (Input.GetKey(KeyCode.A)){
                _horizontalMovement = -1f;
                gameObject.transform.rotation = new Quaternion(0f,180f,0f,0f);
            }else if (Input.GetKey(KeyCode.D)){
                _horizontalMovement = 1f;
                gameObject.transform.rotation = new Quaternion(0f,0f,0f,0f);
            }else{
                _horizontalMovement = 0f;
            }

            MovePlayer();

            if (_isGrounded && Input.GetKeyDown(KeyCode.W))
                JumpPlayer();

            if (Input.GetKeyDown(KeyCode.E))
                Kick();
        }

        // Movimentação do Visitante
        private void Visitante(){
            if (Input.GetKey(KeyCode.J)){
                _horizontalMovement = -1f;
                gameObject.transform.rotation = new Quaternion(0f,180f,0f,0f);
            }else if (Input.GetKey(KeyCode.L)){
                gameObject.transform.rotation = new Quaternion(0f,0f,0f,0f);
                _horizontalMovement = 1f;
            }else{
                _horizontalMovement = 0f;
            }
            
            MovePlayer();
            
            if (_isGrounded && Input.GetKeyDown(KeyCode.I))
                JumpPlayer();

            if (Input.GetKeyDown(KeyCode.U)){
                Kick();
            }
        }

        // Movimenta o jogador
        private void MovePlayer (){
            Vector2 movement = new Vector2((_horizontalMovement * speed * Time.fixedDeltaTime),_rigidbody.velocity.y);
            _rigidbody.velocity = movement;
        }

        // Responsável pelo pulo do jogador
        private void JumpPlayer()
        {
            _animator.SetTrigger(Jump1);
            _rigidbody.AddForce(Vector2.up * jumpStrength, ForceMode2D.Impulse);
            AudioManager.Instance.Play("Pulo");
        }

        // Responsável pelo chute do jogador
        private void Kick(){
            _animator.SetTrigger(Kick1);
        }

        private void Update (){              
            _animator.SetBool(IsJumping, !_isGrounded);
            _animator.SetFloat(Speed, Mathf.Abs(_rigidbody.velocity.x));
            if(GameManager.Instance.gameIsOn){
                if(canShoot && _bola != null){
                    _bola.AddForce(kickStrength*_direction, ForceMode2D.Impulse);
                    AudioManager.Instance.Play("Chute"); 
                    canShoot = false;
                } 
                switch (playerType){
                    case PlayerType.Mandante:
                        Mandante();
                        break;
                    case PlayerType.Visitante:
                        Visitante();
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        private void OnCollisionEnter2D(Collision2D other) {
            if(other.gameObject.layer == 6){
                _isGrounded = true;
            }
        }

        private void OnCollisionExit2D(Collision2D other) {
            if(other.gameObject.layer == 6){
                _isGrounded = false;
            }
        }

        private void OnTriggerEnter2D(Collider2D other) {
            if(other.gameObject.layer == 9){  
                _bola = other.gameObject.GetComponent<Rigidbody2D>();
            }
        }
        
        private void OnTriggerStay2D(Collider2D other) {
            if(other.gameObject.layer == 9) _direction = -(transform.position - other.gameObject.transform.position).normalized; 
        }

        private void OnTriggerExit2D(Collider2D other) {
            if(other.gameObject.layer == 9){  
                _bola = null; 
            }
        }
    }
}