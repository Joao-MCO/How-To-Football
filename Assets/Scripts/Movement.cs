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
        private bool _isFlipped = false;
        private bool _isGrounded = true;
        
        private static readonly int IsJumping = Animator.StringToHash("isJumping");
        private static readonly int Speed = Animator.StringToHash("speed");
        private static readonly int Kick1 = Animator.StringToHash("kick");

        [FormerlySerializedAs("type")]
        [Header("Player Properties")] 
        [Space] 
        [SerializeField] private PlayerType playerType;
        [SerializeField] private float speed = 200f;
        [SerializeField] private float jumpStrength = 10f;
        [SerializeField] private Transform groundCheck;
        [SerializeField] private Transform groundArmCheck;
        [SerializeField] private LayerMask groundLayer;

        [Header("Kicking Properties")]
        [Space]
        [SerializeField] private Transform kickPoint;
        [SerializeField] private float kickStrength = 15f;
        [SerializeField] private LayerMask ballLayer;

        // Setando componentes do Objeto
        private void Awake (){
            _rigidbody = GetComponent<Rigidbody2D>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _animator = GetComponent<Animator>();
        }

        // Movimentação do Mandante
        private void Mandante(){
            if (Input.GetKey(KeyCode.A))
                _horizontalMovement = -1f;

            else if (Input.GetKey(KeyCode.D))
                _horizontalMovement = 1f;

            else
                _horizontalMovement = 0f;

            if (_isGrounded && Input.GetKeyDown(KeyCode.W))
                JumpPlayer();

            if (_isGrounded && Input.GetKeyDown(KeyCode.E))
                Kick();

            if (Input.GetKeyDown(KeyCode.S))
                Head();
        }

        // Movimentação do Visitante
        private void Visitante(){
            if (Input.GetKey(KeyCode.J))
                _horizontalMovement = -1f;

            else if (Input.GetKey(KeyCode.L))
                _horizontalMovement = 1f;

            else
                _horizontalMovement = 0f;
            
            if (_isGrounded && Input.GetKeyDown(KeyCode.I))
                JumpPlayer();

            if (_isGrounded && Input.GetKeyDown(KeyCode.U))
                Kick();

            if (Input.GetKeyDown(KeyCode.K))
                Head();
        }

        // Verifica se está no chão
        private bool CheckGrounded(){
            Collider2D[] collisionsFeet = Physics2D.OverlapCircleAll(groundCheck.position, 0.25f, groundLayer);
            bool feetCheck = collisionsFeet.Any(collision => !collision.gameObject.Equals(gameObject));
            Collider2D[] collisionsArms = Physics2D.OverlapCircleAll(groundArmCheck.position, 0.5f, groundLayer);
            bool armCheck = collisionsArms.Any(collision => !collision.gameObject.Equals(gameObject));
            return (feetCheck || armCheck);
        }

        // Movimenta o jogador
        private void MovePlayer (){
            Vector2 movement = new Vector2((_horizontalMovement * speed * Time.fixedDeltaTime),_rigidbody.velocity.y);
            _rigidbody.velocity = movement;
            FlipSprite();
        }
        
        // Responsável pelo pulo do jogador
        private void JumpPlayer(){
            _rigidbody.AddForce(Vector2.up * jumpStrength, ForceMode2D.Impulse);
            // PlayJumpSound();
        }
        
        // Responsável pelo chute do jogador
        private void Kick(){
            // _animator.SetTrigger(Kick1);
            Collider2D[] balls = Physics2D.OverlapCircleAll(kickPoint.position, 0.6f, ballLayer);
            foreach (Collider2D ball in balls){  
                if (ball.gameObject.TryGetComponent<Bola>(out var ballComponent)){
                    // PlayKickSound();
                    ballComponent.BallMove((ball.gameObject.transform.position - kickPoint.position), kickStrength);
                }
            }
        }

        // Inverte o boneco
        private void FlipSprite(){
            switch (_isFlipped){
                case true when _horizontalMovement > 0f:
                    transform.localScale = new Vector3(1f, 1f, 1f);
                    _isFlipped = false;
                    break;
                case false when _horizontalMovement < 0f:
                    transform.localScale = new Vector3(-1f, 1f, 1f);
                    _isFlipped = true;
                    break;
            }
        }

        private void Update (){
            _isGrounded = CheckGrounded();
            
            // _animator.SetBool(IsJumping, !_isGrounded);
            // _animator.SetFloat(Speed, Mathf.Abs(_rigidbody.velocity.x));

            // if (!GameManager.Instance.gameIsOn){
            //     _horizontalMovement = 0f;
            //     return;
            // }
                
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

        }
    }
}