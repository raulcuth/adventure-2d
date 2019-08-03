using UnityEditor.Rendering;
using UnityEngine;

public class RubyController : MonoBehaviour
{
    [Tooltip("The speed at which th player moves")]
    public float speed = 3.0f;

    [Tooltip("The maximum health points of the player")]
    public int maxHealth = 5;

    [Tooltip("The window of time when the player is invincible after taking damage")]
    public float timeInvincible = 2.0f;

    public GameObject projectilePrefab;

    private int _currentHealth;
    private bool _isInvincible;
    private float _invincibleTimer;
    private Rigidbody2D _rigidbody2D;

    private Animator _animator;
    private Vector2 _lookDirection = new Vector2(1, 0);

    public int health => _currentHealth;

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _currentHealth = maxHealth;
    }

    // Update is called once per frame
    private void Update()
    {
        Move();
        //countdown the time the player is invincible
        if (_isInvincible)
        {
            _invincibleTimer -= Time.deltaTime;
            if (_invincibleTimer < 0)
            {
                _isInvincible = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            Launch();
        }
    }

    private void Move()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector2 move = new Vector2(horizontal, vertical);
        if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            _lookDirection.Set(move.x, move.y);
            _lookDirection.Normalize();
        }

        _animator.SetFloat("Look X", _lookDirection.x);
        _animator.SetFloat("Look Y", _lookDirection.y);
        _animator.SetFloat("Speed", move.magnitude);

        Vector2 position = _rigidbody2D.position;
        position += speed * Time.deltaTime * move;

        _rigidbody2D.MovePosition(position);
    }

    public void ChangeHealth(int amount)
    {
        //if the player took damage, make him invincible, this works for obstacles but 
        //try another solution for enemy damage later. ALL INVINCIBLE LOGIC CAN BE DELETED
        if (amount < 0)
        {
            if (_isInvincible)
            {
                return;
            }

            _isInvincible = true;
            _invincibleTimer = timeInvincible;
        }

        _currentHealth = Mathf.Clamp(_currentHealth + amount, 0, maxHealth);
        _animator.SetTrigger("Hit");
        UIHealthBar.instance.SetValue(_currentHealth / (float) maxHealth);
    }

    private void Launch()
    {
        GameObject projectileObject =
            Instantiate(projectilePrefab, _rigidbody2D.position + Vector2.up * 0.5f, Quaternion.identity);

        Projectile projectile = projectileObject.GetComponent<Projectile>();
        projectile.Launch(_lookDirection, 300);

        _animator.SetTrigger("Launch");
    }
}