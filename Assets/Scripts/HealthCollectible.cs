using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    [Tooltip("The amount of health that the collectible restores")]
    public int healAmount = 1;

    public AudioClip collectedClip;

    private void OnTriggerEnter2D(Collider2D coll)
    {
        var controller = coll.GetComponent<RubyController>();
        if (controller != null)
        {
            if (controller.health < controller.maxHealth)
            {
                controller.ChangeHealth(healAmount);
                Destroy(gameObject);
                controller.PlaySound(collectedClip);
            }
        }
    }
}