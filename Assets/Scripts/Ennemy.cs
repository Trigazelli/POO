using System;
using UnityEngine;

public class Ennemy : MonoBehaviour
{
    [SerializeField] private Transform _target;

    private void FixedUpdate()
    {
        Vector3 newPos = Vector3.MoveTowards(transform.position, _target.position, 0.3f);
        newPos.y = 0;
        transform.LookAt(transform.position + newPos);
        transform.position = newPos;
    }

}
