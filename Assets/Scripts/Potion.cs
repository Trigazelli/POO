using UnityEngine;

public class Potion : Object
{
    [SerializeField] private int _healAmount;


    public override void Use(HurtBox target)
    {
        target.Heal(_healAmount);
        Destroy(gameObject);   
    }

    private void Reset()
    {
        _healAmount = 10;
    }
}
