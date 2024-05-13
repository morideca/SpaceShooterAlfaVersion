using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround : MonoBehaviour
{
    [SerializeField] 
    private SpriteRenderer sprite;

    [SerializeField] 
    private float speed;
    private float positionMinY;

    private Vector2 restartPosition;

    private void Awake()
    {
        restartPosition = transform.position;
        positionMinY = sprite.bounds.size.y * 2 - restartPosition.y;
    }
    void Update()
    {
        Move();
    }

    private void Move()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);
        if (transform.position.y <= -positionMinY)
        {
            transform.position = restartPosition;
        }
    }

}
