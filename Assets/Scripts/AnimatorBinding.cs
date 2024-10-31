using UnityEngine;
using NaughtyAttributes;
using System;

public class AnimatorBinding : MonoBehaviour
{
    [SerializeField] private Animator _anim;

    [SerializeField] private PlayerController _controller;

    [AnimatorParam(nameof(_anim), AnimatorControllerParameterType.Bool)]
    [SerializeField] private string _walking;

    [AnimatorParam(nameof(_anim), AnimatorControllerParameterType.Trigger)]
    [SerializeField] private string _attack;

    [AnimatorParam(nameof(_anim), AnimatorControllerParameterType.Trigger)]
    [SerializeField] private string _hit;

    [AnimatorParam(nameof(_anim), AnimatorControllerParameterType.Trigger)]
    [SerializeField] private string _death;


    private void Start()
    {
        _controller.OnMove += StartMoveAnim;
        _controller.OnMoveRelease += StartMoveAnimRelease;
        _controller.OnAttack += StartAtkAnimation;
        _controller.PlayerHealth.OnTakeDamage += StartHitAnimation;
        _controller.PlayerHealth.OnDie += StartDeathAnimation;
    }

    private void StartDeathAnimation()
    {
        _anim.SetTrigger(_death);
    }

    private void StartHitAnimation()
    {
        _anim.SetTrigger(_hit);
    }

    private void StartAtkAnimation()
    {
        _anim.SetTrigger(_attack);
    }

    private void StartMoveAnim()
    {
        _anim.SetBool(_walking, true);
    }

    private void StartMoveAnimRelease()
    {
        _anim.SetBool(_walking, false);
    }

    private void Reset()
    {
        _anim = GetComponent<Animator>();
        _walking = "Walking";
        _attack = "Attack";
        _hit = "GetHit";
        _attack = "Death";
    }
}
