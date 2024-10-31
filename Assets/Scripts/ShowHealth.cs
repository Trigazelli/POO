using NaughtyAttributes;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShowHealth : MonoBehaviour
{
    [SerializeField] Health _health;
    [SerializeField] Slider _slider;
    [SerializeField, Required] TMP_Text _text;

    private void Start()
    {
        _health.DisplayHealth += DisplayHealth;
        DisplayHealth();
    }

    private void DisplayHealth()
    {
        Debug.Log("Dis moiiiiiiii");
        _slider.value = (float)_health.CurrentHealth / (float)_health.MaxHealth;
        _text.text = _health.CurrentHealth.ToString() + "/" + _health.MaxHealth;

        Debug.Log(_slider.value);
    }

    private void Reset()
    {
        _slider = GetComponentInChildren<Slider>();
        _text = GetComponentInChildren<TMP_Text>();
    }
}
