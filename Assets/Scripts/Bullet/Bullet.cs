using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]private Rigidbody2D rb;
    [SerializeField, Min(0.0f)] private float speed = 100f;
    [SerializeField]private float lifeTime = 10f;

    private BulletMovement movement = null;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        movement = new BulletMovement();

    }

    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    private void Update()
    {
        movement.MoveForward(transform, speed);
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        Destroy(gameObject);
    }
}
