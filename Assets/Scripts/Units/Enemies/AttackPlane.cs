using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPlane : Enemy
{
    [SerializeField] 
    List<Transform> moveStepsTransforms = new List<Transform>();
    [SerializeField]
    Bullet bullet;
    [SerializeField]
    Transform gun;

    private int index = 0;

    List<Vector2> moveStepsPoints = new List<Vector2>();

    private Coroutine attacking;

    private void OnEnable()
    {
        GameManager.BossIsComing += StopAttack;
        GameManager.BossIsComing += StopAnyAction;
    }

    private void OnDisable()
    {
        GameManager.BossIsComing -= StopAttack;
        GameManager.BossIsComing -= StopAnyAction;
    }
    private void Start()
    {
        CreatePath();
        Attack();

    }
    private void Update()
    {
        if (inMove) Move();
        Leaving();
    }

    private void CreatePath()
    {
        foreach (Transform ponit in moveStepsTransforms)
        {
            moveStepsPoints.Add(ponit.position);
        }
    }

    public override void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, moveStepsPoints[index], moveSpeed * Time.deltaTime);
        if (Vector3.Distance(transform.position, moveStepsPoints[index]) <= 0.01f) { index++; }
        if (index >= moveStepsPoints.Count) Destroy(gameObject);
    }

    public override void TakeDamage(int amountDamage)
    {
        healthManager.TakeDamage(amountDamage);
    }

    public override void Attack()
    {
        attacking = StartCoroutine(Attacking());
    }

    private IEnumerator Attacking()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(1.3f, 3));
            Bullet _bullet = Instantiate(bullet, gun.position, gun.rotation);
            _bullet.bulletDamage = 10;
        }
    }

    private void StopAttack()
    {
        StopCoroutine(attacking);
    }
}
