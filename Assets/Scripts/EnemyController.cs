using System;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed = 3.0f;
    public bool vertical;
    public float changeTime = 3.0f;
    public int damageAmount = 1;
    private Rigidbody2D _rigidbody2D;
    private float _timer;
    private int _direction = 1;
    private Animator _animator;
    private static readonly int MoveXProperty = Animator.StringToHash("Move X");
    private static readonly int MoveYProperty = Animator.StringToHash("Move Y");
    private bool _broken;

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _timer = changeTime;
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (!_broken)
        {
            return;
        }

        _timer -= Time.deltaTime;
        if (_timer < 0)
        {
            _direction = -_direction;
            _timer = changeTime;
        }

        //check if you can implement the AI behaviour from the coin-collector project for following the target
        Vector2 position = _rigidbody2D.position;
        if (vertical)
        {
            _animator.SetFloat(MoveXProperty, 0);
            _animator.SetFloat(MoveYProperty, _direction);
            position.y += Time.deltaTime * speed * _direction;
        }
        else
        {
            _animator.SetFloat(MoveXProperty, _direction);
            _animator.SetFloat(MoveYProperty, 0);
            position.x += Time.deltaTime * speed * _direction;
        }

        _rigidbody2D.MovePosition(position);
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        var player = coll.gameObject.GetComponent<RubyController>();
        if (player != null)
        {
            player.ChangeHealth(-damageAmount);
        }
    }

    public void Fix()
    {
        _broken = false;
        _rigidbody2D.simulated = false;
    }
}