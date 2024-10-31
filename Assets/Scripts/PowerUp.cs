using UnityEngine;

public class PowerUp : Object
{
    [SerializeField] private int amountToAdd;
    public override void Use(HurtBox target)
    {
        target.UpgradeHealth(amountToAdd);
        Destroy(gameObject);
    }
}
