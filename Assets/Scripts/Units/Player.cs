using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Bullet bullet;
    [SerializeField] private GameObject gun;
    [SerializeField] private GameObject gunLeft;
    [SerializeField] private GameObject gunRight;

    private HealthManager healthManager;

    private Vector3 mousePosition;

    private int weaponLevel = 0;
    private int maxWeaponLevel = 2;
    private int damage = 10;

    private float speed = 5f;
    private float attackCooldown = 0.2f;
    public float bulletSpeed { get; private set; } = 10;

    private Coroutine attacking;

    private void Awake()
    {
        healthManager = GetComponent<HealthManager>();
    }

    void Update()
    {
        Move();
        Attack();
    }

    private void Move()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        transform.position = Vector3.Lerp(transform.position, mousePosition, speed * Time.deltaTime);
    }

    private void Attack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            attacking = StartCoroutine(Attacking());
        }
        if (Input.GetMouseButtonUp(0))
        {
            StopCoroutine(attacking);
        }
    }

    private IEnumerator Attacking () 
    {
        while (true)
        {
            switch (weaponLevel)
            {
                case 0:
                    Bullet _bullet = Instantiate(bullet, gun.transform.position, gun.transform.rotation);
                    _bullet.bulletDamage = damage;
                    break;
                case 1:
                    _bullet = Instantiate(bullet, gunLeft.transform.position, gun.transform.rotation);
                    _bullet.bulletDamage = damage;
                    _bullet = Instantiate(bullet, gunRight.transform.position, gun.transform.rotation);
                    _bullet.bulletDamage = damage;
                    break;
                case 2:
                    _bullet = Instantiate(bullet, gun.transform.position, gun.transform.rotation);
                    _bullet.bulletDamage = damage;
                    _bullet = Instantiate(bullet, gunLeft.transform.position, gun.transform.rotation);
                    _bullet.bulletDamage = damage;
                    _bullet = Instantiate(bullet, gunRight.transform.position, gun.transform.rotation);
                    _bullet.bulletDamage = damage;
                    break;
            }
            yield return new WaitForSeconds(attackCooldown);
        }
    }
    public void TakeDamage(int amountDamage)
    {
        healthManager.TakeDamage(amountDamage);
        if (weaponLevel != 0) weaponLevel -= 1;

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(20);
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("Bonus"))
        {
            if (weaponLevel != maxWeaponLevel) weaponLevel += 1;
            Destroy(collision.gameObject);
        }
    }
}
