using System;
using System.Diagnostics;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class BulletProjectile : MonoBehaviour
{
    public float speed = 10f;
    public float damage = 10f;
    public int maxPenetration = 1;

    private Vector2 direction;
    private int remainingHits;
    public Rigidbody2D rb;

    void Awake()
    {
        remainingHits = maxPenetration;
    }

    public void SetDirection(Vector2 dir)
    {
        direction = dir.normalized;
        rb.linearVelocity = direction * speed;

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        LifeComponent health = other.GetComponent<LifeComponent>();
        if (health != null)
        {
             health.TakeDamage(damage);

            remainingHits--;
            if (remainingHits <= 0)
            {
                Destroy(gameObject);
            }
        }
        else
        {
        }
    }
}
