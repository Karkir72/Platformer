using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]

public class Player : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private Text _scoreText;

    private Animator _animator;
    private Rigidbody2D _rigidbody;
    private int _coins;
    private bool _isDeath;
    private bool _onGround;

    private void Start()
    {
        _coins = 0;
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _isDeath = false;
        _onGround = true;
    }

    private void Update()
    {
        if (_isDeath == false)
        {
            if (Input.GetKeyDown(KeyCode.Space) && _onGround)
            {
                _animator.SetTrigger(AnimatorPlayer.Params.Jump);
                _rigidbody.AddForce(Vector2.up * _jumpForce);
            }

            if (Input.GetKey(KeyCode.A))
            {
                _animator.SetBool(AnimatorPlayer.Params.IsGo, true);
                transform.Translate(-_speed * Time.deltaTime, 0, 0);
                transform.localScale = new Vector3(-1, 1, 1);
            }

            if (Input.GetKey(KeyCode.D))
            {
                _animator.SetBool(AnimatorPlayer.Params.IsGo, true);
                transform.Translate(_speed * Time.deltaTime, 0, 0);
                transform.localScale = new Vector3(1, 1, 1);
            }

            if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
            {
                _animator.SetBool(AnimatorPlayer.Params.IsGo, false);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Scull>())
        {
            _coins++;
            _scoreText.text = _coins.ToString();
            Destroy(collision.gameObject);
        }

        if (collision.GetComponent<Tornado>())
        {
            transform.localScale = new Vector3(1, 1, 1);
            _isDeath = true;
            _animator.SetBool(AnimatorPlayer.Params.IsDead, true);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Ground>())
        {
            _onGround = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Ground>())
        {
            _onGround = true;
        }
    }
}

public static class AnimatorPlayer
{
    public static class Params
    {
        public const string Jump = nameof(Jump);
        public const string IsGo = nameof(IsGo);
        public const string IsDead = nameof(IsDead);
    }

    public static class States
    {
        public const string Idle = nameof(Idle);
        public const string Run = nameof(Run);
        public const string Jump = nameof(Jump);
        public const string Death = nameof(Death);
    }
}
