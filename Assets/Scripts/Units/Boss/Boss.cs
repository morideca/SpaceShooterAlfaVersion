using System;
using UnityEngine;

public class Boss : MonoBehaviour
{

    [SerializeField]
    private GameObject hpBar;

    [SerializeField]
    private Transform startTransfrom;

    private Vector2 startPosition;

    private bool toStartPos = false;

    public static event Action BossFightStart;

    private void OnEnable()
    {
        toStartPos = true;
    }
    void Start()
    {
        startPosition = startTransfrom.position;
    }

    void Update()
    {
        GoToStartPosition();
    }

    private void GoToStartPosition()
    {
        if (toStartPos)
        {
            transform.Translate(Vector3.down * 2 * Time.deltaTime);
        }

        if (Vector3.Distance(transform.position, startPosition) <= 0.1f && toStartPos)
        {
            toStartPos = false;
            hpBar.SetActive(true);
            BossFightStart?.Invoke();
            Enemy.inMove = true;
        }
    }
}
