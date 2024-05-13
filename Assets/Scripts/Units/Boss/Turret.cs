using System.Collections;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField]
    Bullet bullet;
    [SerializeField]
    Transform gun;

    private float currentPositionX;
    private float zigZagSpeed = 3f;
    private float zigZagDistance = 10;

    private bool moveRight;

    private void OnEnable()
    {
        Boss.BossFightStart += StartAttack;
    }

    private void OnDisable()
    {
        Boss.BossFightStart -= StartAttack;
    }

    private void Start()
    {
        currentPositionX = transform.position.x;
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        if (Enemy.inMove)
        {
            {
                if (moveRight)
                {
                    transform.Translate(Vector2.right * zigZagSpeed * Time.deltaTime);
                    if (transform.position.x <= currentPositionX - zigZagDistance) moveRight = false;
                }
                else
                {
                    transform.Translate(Vector2.left * zigZagSpeed * Time.deltaTime);
                    if (transform.position.x >= currentPositionX + zigZagDistance) moveRight = true;
                }
            }
        }
    }
    

    private void StartAttack()
    {
        Debug.Log("Начинаю атаку!");
        StartCoroutine(Attacking());
    }

    private IEnumerator Attacking()
    {
        while (true)
        {
            Bullet _bullet = Instantiate(bullet, gun.position, gun.rotation);
            _bullet.bulletDamage = 20;
            yield return new WaitForSeconds(1);
        }
    }
}
