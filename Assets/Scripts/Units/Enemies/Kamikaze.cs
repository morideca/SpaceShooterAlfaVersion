using UnityEngine;

public class Kamikaze : Enemy
{
    private float currentPositionX;
    private float zigZagSpeed = 3f;
    private float zigZagDistance = 1;
    private bool moveRight;

    private void OnEnable()
    {
        GameManager.BossIsComing += StopAnyAction;
    }

    private void OnDisable()
    {
        GameManager.BossIsComing -= StopAnyAction;
    }
    private void Start()
    {
        currentPositionX = transform.position.x;
    }

    void Update()
    {
        Move();
        Leaving();
    }

    public override void Move()
    {
        if (inMove) {
            {
                transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);
                if (moveRight)
                {
                    transform.Translate(Vector3.right * zigZagSpeed * Time.deltaTime);
                    if (transform.position.x >= currentPositionX + zigZagDistance) moveRight = false;
                }
                else
                {
                    transform.Translate(Vector3.left * zigZagSpeed * Time.deltaTime);
                    if (transform.position.x <= currentPositionX - zigZagDistance) moveRight = true;
                }
            }
        }
    }

    public override void Attack()
    {

    }

    public override void TakeDamage(int amountDamage)
    {
        healthManager.TakeDamage(amountDamage);
    }
}
