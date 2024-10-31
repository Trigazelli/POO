using NaughtyAttributes;
using UnityEngine;

public class HurtBox : MonoBehaviour
{
    [SerializeField, InfoBox("nécessaire si le game object est le joueur")] private PlayerController _controller;
    [SerializeField] private Health _health;


    public void Damage(int damage)
    {
        _health.TakeDamage(damage);
    }

    public void Heal(int healAmount)
    {
        _health.Heal(healAmount);
    }

    public void UpgradeSword(int multiplier)
    {
        if (_controller == null) return;
        _controller.Damage.Multiplier = multiplier;
    }

    public void UpgradeHealth(int amount)
    {
        _health.UpgradeMaxHealth(amount);
    }

    #region Editor
    private void Reset()
    {
        _controller = transform.root.GetComponent<PlayerController>();
        _health = _controller? _controller.PlayerHealth : transform.root.GetComponent<Health>();
    }

    #endregion
}
