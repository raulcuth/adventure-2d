using UnityEngine;

public class DamageZone : MonoBehaviour
{
    [Tooltip("The amount of damage that the zone inflicts")]
    public int damageAmount = -1;

    private void OnTriggerEnter2D(Collider2D coll)
    {
        var controller = coll.GetComponent<RubyController>();
        if (controller != null)
        {
            controller.ChangeHealth(damageAmount);
        }
    }
}