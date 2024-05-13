using System;
using System.Collections;
using UnityEngine;

public class HealthManager : MonoBehaviour, IDamageable
{
    [SerializeField]
    private SpriteRenderer unitSpriteMaterial;

    [SerializeField] 
    private int maxHealth;
    [SerializeField] 
    private int progressPoints;
    public int currentHealth { get; private set; }

    public static event Action EnemyKilled;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void Heal(int healAmount)
    {
        currentHealth += healAmount;
        if (currentHealth > maxHealth) currentHealth = maxHealth;
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        StartCoroutine(Damaged(Color.red));
        if (currentHealth <= 0)  
        {
            Destroy(gameObject);
            if (gameObject.CompareTag("Enemy")) EnemyKilled?.Invoke();
        }
    }

    private IEnumerator Damaged(Color _color)
    {
        while (unitSpriteMaterial.color != _color)
        {
            unitSpriteMaterial.color = Color.Lerp(unitSpriteMaterial.color, _color, 1f);
        }
        yield return new WaitForSeconds(0.1f);
        unitSpriteMaterial.color = Color.Lerp(unitSpriteMaterial.color, Color.white, 1);
    }
}
