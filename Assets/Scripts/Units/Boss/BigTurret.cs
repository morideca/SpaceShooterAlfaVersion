using System.Collections;
using UnityEngine;

public class BigTurret : MonoBehaviour
{
    [SerializeField]
    GameObject player;
    [SerializeField]
    Bullet bullet;
    [SerializeField]
    Transform gun;

    private void OnEnable()
    {
        Boss.BossFightStart += StartAttack;
    }

    private void OnDisable()
    {
        Boss.BossFightStart -= StartAttack;
    }

    void Update()
    {
        Vector2 vectorToTarget = (Vector2)player.transform.position - (Vector2)transform.position; 
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg - 90;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    private void StartAttack()
    {
        StartCoroutine(Attacking());
    }

    private IEnumerator Attacking()
    {
        while (true)
        {
            Bullet _bullet = Instantiate(bullet, gun.position, gun.rotation);
            _bullet.bulletDamage = 20;
            _bullet.transform.localScale = new Vector3(2, 2, 2);
            yield return new WaitForSeconds(1.5f);
        }
    }
}
