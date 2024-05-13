using UnityEngine;
using System.Collections;

public class Bonus : MonoBehaviour
{
    private float speed = 1f;

    private SpriteRenderer spriteMaterial;

    private void Start()
    {
        spriteMaterial = GetComponent<SpriteRenderer>();
        StartCoroutine(Shining());
    }

    private void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);
    }

    private IEnumerator Shining()
    {
        while (true)
        {
            while (spriteMaterial.color != Color.red)
            {
                spriteMaterial.color = Color.Lerp(spriteMaterial.color, Color.red, 1);
            }
            yield return new WaitForSeconds(0.5f);
            while (spriteMaterial.color != Color.yellow)
            {
                spriteMaterial.color = Color.Lerp(spriteMaterial.color, Color.yellow, 1);
            }
            yield return new WaitForSeconds(0.5f);
        }
    }
}
