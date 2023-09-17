using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

namespace HTF{
    public class Movement : MonoBehaviour 
    {
        private Rigidbody2D _rigidbody;
        private SpriteRenderer _spriteRenderer;
        private Animator _animator;
        private float _horizontalMovement;
        private bool _isGrounded = true;
        
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
        [SerializeField] private Pe pe;

        // Setando componentes do Objeto
        private void Awake (){
            _rigidbody = GetComponent<Rigidbody2D>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
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

            if (_isGrounded && Input.GetKeyDown(KeyCode.E))
                Kick();

            if (Input.GetKeyDown(KeyCode.S))
                Head();
        }

        // Movimentação do Visitante
        private void Visitante(){
            if (Input.GetKey(KeyCode.J)){
                _horizontalMovement = -1f;
                gameObject.transform.rotation = new Quaternion(0f,0f,0f,0f);
            }else if (Input.GetKey(KeyCode.L)){
                _horizontalMovement = 1f;
                gameObject.transform.rotation = new Quaternion(0f,180f,0f,0f);
            }else{
                _horizontalMovement = 0f;
            }
            
            MovePlayer();
            
            if (_isGrounded && Input.GetKeyDown(KeyCode.I))
                JumpPlayer();

            if (_isGrounded && Input.GetKeyDown(KeyCode.U))
                Kick();

            if (Input.GetKeyDown(KeyCode.K))
                Head();
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
            // PlayJumpSound();
        }
        
        // Responsável pelo chute do jogador
        private void Kick(){
            _animator.SetTrigger(Kick1);
            if(pe.CanShoot()){
                pe.GetBola().AddForce(kickStrength*pe.GetDirection(), ForceMode2D.Impulse);
            }
        }

        private void Update (){     
            _isGrounded = pe.CheckGround();              
            _animator.SetBool(IsJumping, !_isGrounded);
            _animator.SetFloat(Speed, Mathf.Abs(_rigidbody.velocity.x));
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

        private void Head(){
            _animator.SetTrigger(Head1);
        }
    }
}