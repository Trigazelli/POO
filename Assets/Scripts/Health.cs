using System;
using System.Collections;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{

    [SerializeField, ValidateInput(nameof(ValidateMaxLife), "MaxLife is less than 0")] private int _maxHealth;
    [SerializeField, Range(0f, 2f)] private float _timeBeforeDeath;
    [ShowNonSerializedField] private int _currentHealth;

    [SerializeField] private UnityEvent _onDie;
    [SerializeField] private UnityEvent _onTakeDamage;

    [SerializeField, InfoBox("If checked, the gameObject will be pushed back when hit.")] private bool _blowback;
    [SerializeField, ShowIf(nameof(_blowback))] private Rigidbody _rigidBody;
    [SerializeField, ShowIf(nameof(_blowback))] private int _blowbackStrength;


    public event Action OnTakeDamage;
    public event Action DisplayHealth;
    public event Action OnDie;


    public int CurrentHealth
    {
        get { return _currentHealth; }
        set
        {
            if (value < 0) return;
            _currentHealth = value;
        }
    }

    public int MaxHealth { get { return _maxHealth; } }

    #region Editor
    private bool ValidateMaxLife() => _maxHealth > 0;

    private void Reset()
    {
        _maxHealth = 100;
        _timeBeforeDeath = 0;
        _blowback = false;
        _blowbackStrength = 800;
        _rigidBody = GetComponentInChildren<Rigidbody>();
    }

    [Button]
    void TestTakeDamage()
    {
        TakeDamage(10);
    }

    [Button]
    void TestTakeDamageError()
    {
        TakeDamage(-10);
    }

    #endregion

    private void Awake()
    {
        _currentHealth = _maxHealth;
    }

    public void TakeDamage(int damage)
    {
        // Guard
        if (damage <= 0)
        {
            Debug.LogError("damage must be positive");
            return;
        }

        Debug.Log("damage" + damage);
        _currentHealth = Mathf.Clamp(_currentHealth-damage, 0, _maxHealth);
        DisplayHealth?.Invoke();
        // Death
        if (_currentHealth <= 0)
        {
            Die();
            return;
        }

        if (_blowback)
        {
            _rigidBody.AddForce(transform.root.transform.forward * -_blowbackStrength);
        }
        OnTakeDamage?.Invoke();
        _onTakeDamage?.Invoke();
    }

    public void Heal(int healAmount)
    {
        if (healAmount <= 0)
        {
            Debug.LogError("healAmount must be positive");
            return;
        }

        _currentHealth = Mathf.Clamp(_currentHealth+healAmount, 0, _maxHealth);
        DisplayHealth?.Invoke();
        
    }

    private void Die()
    {
        StartCoroutine(DieRoutine());

        IEnumerator DieRoutine()
        {
            _onDie?.Invoke();
            OnDie?.Invoke();
            Debug.Log("Dying");
            if (_blowback)
            {
                _rigidBody.AddForce(transform.root.transform.forward * -_blowbackStrength * 2);
            }
            yield return new WaitForSeconds(_timeBeforeDeath);
            Destroy(gameObject);
        }
    }

    internal void UpgradeMaxHealth(int amount)
    {
        _maxHealth += amount;
        Heal(amount);
        DisplayHealth?.Invoke();
    }
}
