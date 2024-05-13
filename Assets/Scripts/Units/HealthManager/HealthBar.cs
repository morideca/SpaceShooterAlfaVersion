using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    private Slider healthbar;
    private HealthManager healthManager;
    private void Awake()
    {
        healthManager = GetComponent<HealthManager>();
    }

    private void Start()
    {
        healthbar.maxValue = healthManager.currentHealth;
    }

    void FixedUpdate()
    {
        healthbar.value = healthManager.currentHealth;
    }
}
