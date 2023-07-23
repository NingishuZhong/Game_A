using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalDrop : MonoBehaviour
{
    private float gravity = 20f;
    private Vector3 velocity;
    private SpriteRenderer sr;
    public float existTime;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = AnimalLib.animals[Random.Range(0, 5)];
        velocity = new Vector3(Random.Range(-1.2f, 1.2f), Random.Range(8f, 10f), 0);
    }

    private void Update()
    {
        if (existTime > 0)
        {
            existTime -= Time.deltaTime;
        }
        else
        {
            Destroy(gameObject);
        }
        gameObject.transform.position += velocity * Time.deltaTime;
        velocity.y -= gravity * Time.deltaTime;
    }
}
