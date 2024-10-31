using System;
using System.Collections;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("References")]
    [SerializeField, Required] private Rigidbody _body;
    [SerializeField, Required] private Camera _camera;
    [SerializeField, Required] private Health _playerHealth;
    [SerializeField, Required] private BoxCollider _hitBox;
    [SerializeField, Required] private Damage _damage;



    [SerializeField] private int _speed;

    public event Action OnMove;
    public event Action OnMoveRelease;
    public event Action OnAttack;
    public Health PlayerHealth { get { return _playerHealth; } }
    public Damage Damage { get { return _damage; } }

    private Vector3 _movement;

    private void Start()
    {
        Debug.Log(_playerHealth.ToString());
    }

    public void SprintStarted(InputAction.CallbackContext context)
    {
        _speed *= 2;
    }

    public void SprintReleased(InputAction.CallbackContext context)
    {
        _speed /= 2;
    }

    public void MoveForward(InputAction.CallbackContext context)
    {
        OnMove?.Invoke();
        Vector2 input = context.ReadValue<Vector2>();
        _movement = _camera.transform.forward * input.y + _camera.transform.right * input.x;
        _movement.y = 0;
    }

    public void MoveRelease(InputAction.CallbackContext context)
    {
        OnMoveRelease?.Invoke();
        _movement = Vector3.zero;
    }

    public void Attack(InputAction.CallbackContext context)
    {
        StartCoroutine(Attack());

        IEnumerator Attack()
        {
            OnAttack?.Invoke();
            _hitBox.enabled = true;
            yield return new WaitForSeconds(1f);
            _hitBox.enabled = false;
        }
    }

    private void FixedUpdate()
    {
        _body.AddForce(_movement * _speed * Time.fixedDeltaTime);
        transform.LookAt(transform.position + _movement);
    }

    private void Reset()
    {
        _playerHealth = GetComponent<Health>();
        _body = GetComponent<Rigidbody>();
        _camera = FindAnyObjectByType<Camera>();
        _speed = 1000;
    }
}
