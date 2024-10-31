using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    [SerializeField] private PlayerController _player;

    [SerializeField] private InputActionReference _move;
    [SerializeField] private InputActionReference _sprint;
    [SerializeField] private InputActionReference _attack;

    private void OnEnable()
    {
        _move.action.Enable();
        _attack.action.Enable();
        _sprint.action.Enable();

        _sprint.action.started += _player.SprintStarted;
        _sprint.action.canceled += _player.SprintReleased;
        _move.action.performed += _player.MoveForward;
        _move.action.canceled += _player.MoveRelease;
        _attack.action.started += _player.Attack;
        _player.PlayerHealth.OnDie += DisableInputs;
    }

    private void OnDisable()
    {
        _move.action.performed -= _player.MoveForward;
        _move.action.canceled -= _player.MoveRelease;
        _sprint.action.started -= _player.SprintStarted;
        _sprint.action.canceled -= _player.SprintReleased;
        _attack.action.started -= _player.Attack;

        DisableInputs();
    }

    private void DisableInputs()
    {
        _sprint.action.Disable();
        _move.action.Disable();
        _attack.action.Disable();
    }
}
