using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    [SerializeField, Min(0.0f)]private float speed = 1f;
    private Transform playerTransform;
    private Rigidbody2D rb;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerTransform = GameManager.instance.player.transform;
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        var direction = (playerTransform.position - transform.position).normalized;
        rb.velocity = direction * speed * Time.deltaTime;

        transform.LookAt(playerTransform);
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        transform.localRotation *= Quaternion.Euler(0, -90, -90);
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Bullet")
        {
            Destroy(gameObject);
        }
    }
}
