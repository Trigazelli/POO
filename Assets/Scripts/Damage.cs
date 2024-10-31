using Unity.Cinemachine;
using UnityEngine;

public class Damage : MonoBehaviour
{
    [SerializeField] private int _baseAmountOfDamage;
    private int _multiplier = 1;

    public int Multiplier { get { return _multiplier; } set { _multiplier = value; } } 

    private void OnTriggerEnter(Collider collider)
    {
        Debug.Log("Entered Damage");
        HurtBox target;
        if (collider.gameObject.TryGetComponent<HurtBox>(out target))
        {
            Debug.Log(_baseAmountOfDamage *  _multiplier);
            target.Damage(_baseAmountOfDamage * _multiplier);
        }
    }

    private void Reset()
    {
        _baseAmountOfDamage = 10;
    }
}
