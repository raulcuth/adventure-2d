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

    private int _currentHealth;
    private bool _isInvincible;
    private float _invincibleTimer;
    private Rigidbody2D _rigidbody2D;

    public int health => _currentHealth;

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
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
    }

    private void Move()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector2 position = _rigidbody2D.position;
        position.x = position.x + speed * horizontal * Time.deltaTime;
        position.y = position.y + speed * vertical * Time.deltaTime;
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
        Debug.Log(_currentHealth + "/" + maxHealth);
    }
}