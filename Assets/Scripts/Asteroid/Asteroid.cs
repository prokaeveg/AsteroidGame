using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField, Min(0.0f)] private float speed = 200f;
    [SerializeField, Min(0.0f)] private float size = 2.0f;
    [SerializeField, Min(0.0f)] private float lifeTime = 30f;
    [SerializeField, Min(0.0f)] private float minSize = 0.95f;
    [SerializeField, Min(0.0f)] private float maxSize = 3f;

    [SerializeField, Min(0)] private int asteroidMass = 1;
    [SerializeField, Min(0)] private int maxSmallAsteroids = 3;
    [SerializeField, Min(0)] private int minSmallAsteroids = 1;

    public float MaxSize => maxSize;
    public float MinSize => minSize;
    public float Size
    {
        get => size;
        set
        {
            size = value;
        }
    }


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        transform.eulerAngles = new Vector3(0, 0, Random.value * 360f);

        transform.localScale = Vector3.one * size;
        rb.mass = asteroidMass;

        Destroy(gameObject, lifeTime);
    }

    public void SetTrajectory(Vector2 direction)
    {
        rb.AddForce(direction * speed);
    }


    private void OnCollisionEnter2D(Collision2D coll)
    {
        int amountOfAsteroids = Random.Range(minSmallAsteroids, maxSmallAsteroids);

        if (coll.gameObject.tag == "Bullet")
        {
            Debug.Log("Hit");
            if ((size * 0.5f) >= minSize)
            {
                for (int i = 0; i < amountOfAsteroids; i++)
                {
                    SplitAsteroid();
                }
            }
        Destroy(gameObject);
        }
    }

    private Asteroid SplitAsteroid()
    {
        Vector2 position = transform.position;
        position += Random.insideUnitCircle * 0.3f;

        Asteroid half = Instantiate(this, position, transform.rotation);
        half.size = size * 0.5f;
        half.SetTrajectory(Random.insideUnitCircle.normalized);

        return half;
    }

}
