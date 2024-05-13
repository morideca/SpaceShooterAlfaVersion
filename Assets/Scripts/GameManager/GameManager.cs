using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private Vector3 spawnPositionKamikaze;

    [SerializeField]
    private int maxProgressPoints = 100;
    private int currentProgressPoints;

    [SerializeField]
    private float kamikazeSpawnTime;
    [SerializeField]
    private float attackPlaneSpawntime;

    [SerializeField]
    private Transform kamikazeSpawnPoint;
    [SerializeField]
    private Transform attackPlaneTopSpawnPoint;
    [SerializeField]
    private Transform attackPlaneLeftSpawnPoint;
    [SerializeField]
    private Transform attackPlaneRightSpawnPoint;

    [SerializeField]
    private GameObject kamikaze;
    [SerializeField]
    private GameObject attackPlaneTop;
    [SerializeField]
    private GameObject attackPlaneRight;
    [SerializeField]
    private GameObject attackPlaneLeft;
    [SerializeField]
    private GameObject boss;

    [SerializeField]
    private Slider progressBar;

    public static event Action BossIsComing;

    private void OnEnable()
    {
        HealthManager.EnemyKilled += GetProgressPoints;
    }

    private void OnDisable()
    {
        HealthManager.EnemyKilled -= GetProgressPoints;
    }
    private void Awake()
    {
        StartCoroutine(SpawnKamikaze());
        StartCoroutine(SpawnAttackPlane());
    }
    private void Start()
    {
        progressBar.maxValue = maxProgressPoints;
        progressBar.value = currentProgressPoints;
    }

    private void GetProgressPoints()
    {
        currentProgressPoints += 1;
        MakeGameHarder();
        progressBar.value = currentProgressPoints;
        if (currentProgressPoints >= maxProgressPoints)
        {
            BossIsComing?.Invoke();
            StopAllCoroutines();
            StartCoroutine(BossEnterTheGame());
        }
    }

    private IEnumerator SpawnKamikaze()
    {
        while (true)
        {
            spawnPositionKamikaze = new Vector3(kamikazeSpawnPoint.position.x + UnityEngine.Random.Range(-15, 15), 
                kamikazeSpawnPoint.position.y, 0);
            Instantiate(kamikaze, spawnPositionKamikaze, kamikazeSpawnPoint.rotation);
            yield return new WaitForSeconds(kamikazeSpawnTime);
        }
    }

    private IEnumerator SpawnAttackPlane()
    {
        while (true)
        {
            int i = UnityEngine.Random.Range(0, 5);
            int enemyAmount = UnityEngine.Random.Range(4, 8);
            while (enemyAmount > 0)
            {
                switch (i)
                {
                    case 0: case 1:
                        Instantiate(attackPlaneTop, attackPlaneTopSpawnPoint.position, attackPlaneTopSpawnPoint.rotation);
                        break;
                    case 2: case 3:
                        Instantiate(attackPlaneLeft, attackPlaneLeftSpawnPoint.position, attackPlaneLeftSpawnPoint.rotation);
                        break;
                    case 4: case 5:
                        Instantiate(attackPlaneRight, attackPlaneRightSpawnPoint.position, attackPlaneRightSpawnPoint.rotation);
                        break;
                }
                enemyAmount--;
                yield return new WaitForSeconds(0.5f);
            }
            yield return new WaitForSeconds(attackPlaneSpawntime);
        }
    }

    IEnumerator BossEnterTheGame()
    {
        yield return new WaitForSeconds(10);
        boss.SetActive(true);
    }

    private void MakeGameHarder()
    {
        attackPlaneSpawntime *= 0.99f;
    }
}
