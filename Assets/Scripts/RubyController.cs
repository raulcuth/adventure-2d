using UnityEngine;

public class RubyController : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector2 position = transform.position;
        position.x = position.x + 0.1f * horizontal * Time.deltaTime;
        position.y = position.y + 0.1f * vertical * Time.deltaTime;
        transform.position = position;
    }
}