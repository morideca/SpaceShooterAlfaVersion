using System.Collections;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    protected float moveSpeed = 3f;

    protected int lootChance = 4;
    protected int tryLuckForLoot;

    public static bool goingOut = false;
    public static bool inMove = true;

    protected HealthManager healthManager;

    [SerializeField]
    private GameObject bonus;

    private void Awake()
    {
        healthManager = GetComponent<HealthManager>();
        tryLuckForLoot = Random.Range(1, 100);
    }
    protected void Leaving()
    {
        if (goingOut) gameObject.transform.Translate(Vector3.up * 5 * Time.deltaTime);
    }

    private void OnDestroy()
    {
        if (tryLuckForLoot <= lootChance)
        {
            Instantiate(bonus, transform.position, transform.rotation);
        }
    }

    protected void StopAnyAction()
    {
        inMove = false;
        StartCoroutine(GoingOut());
    }

    protected IEnumerator GoingOut()
    {
        yield return new WaitForSeconds(3f);
        // ƒелаю через очко, ибо как только вставл€ю while - юнити крашитс€
        goingOut = true;
    }

    public abstract void Move();

    public abstract void Attack();

    public abstract void TakeDamage(int amountDamage);





}
