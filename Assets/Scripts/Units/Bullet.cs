using UnityEngine;

public class Bullet : MonoBehaviour
{   
    [SerializeField]
    private bool isEnemy;
    [SerializeField] 
    bool isBigTurret;

    private Player player;

    private float bulletSpeed = 3;

    private Rigidbody2D rigidbody;

    private Vector2 vectorToPlayer;

    public int bulletDamage { get; set; }


    private void OnEnable()
    {
        GameManager.BossIsComing += DestroyBullet;
        if (isEnemy == true && isBigTurret == true)
        {
            player = FindObjectOfType<Player>();
            vectorToPlayer = player.transform.position - transform.position;
        }
    }

    private void OnDisable()
    {
        GameManager.BossIsComing -= DestroyBullet;
    }
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        if(isEnemy == false)
        {
            player = FindObjectOfType<Player>();
            bulletSpeed = player.bulletSpeed;
        }

    }

    private void FixedUpdate()
    {
        if (isEnemy && isBigTurret == false) 
            rigidbody.velocity = Vector3.down * bulletSpeed;
        else if (isEnemy == false && isBigTurret == false)
            rigidbody.velocity = Vector3.up * bulletSpeed;
        else if (isEnemy == true && isBigTurret == true)
        {
            rigidbody.velocity = vectorToPlayer.normalized * bulletSpeed;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out IDamageable damageable))
            {
            if (isEnemy && collision.gameObject.CompareTag("Player"))
            {
                collision.gameObject.TryGetComponent(out Player player);
                player.TakeDamage(bulletDamage);
                Destroy(gameObject);
            }
            else if(isEnemy == false && collision.gameObject.CompareTag("Enemy"))
            {
                damageable.TakeDamage(bulletDamage);
                Destroy(gameObject);
            }    
        }
    }

    private void DestroyBullet()
    {
        Destroy(gameObject);
    }
}
