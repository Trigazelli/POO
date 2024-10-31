using System;
using UnityEngine;

public abstract class Object : MonoBehaviour
{
    public Object()
    {

    }

    private void OnTriggerEnter(Collider collider)
    {
        HurtBox target;
        if (collider.gameObject.TryGetComponent<HurtBox>(out target))
        {
            Use(target);
        }
    }

    public abstract void Use(HurtBox target);
}
