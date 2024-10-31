using UnityEngine;

public class Sword : Object
{
    [SerializeField] private int _multiplier;
    public override void Use(HurtBox target)
    {
            target.UpgradeSword(_multiplier);
            Destroy(gameObject);   
    }
}
