using UnityEngine;

public class RubyController : MonoBehaviour
{
    [Tooltip("The maximum health points of the player")]
    public int maxHealth = 5;

    [Tooltip("The speed at which th player moves")]
    public float speed = 3.0f;

    private int currentHealth;
    private Rigidbody2D _rigidbody2D;

    public int health
    {
        get { return currentHealth; }
    }

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    private void Update()
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
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
    }
}