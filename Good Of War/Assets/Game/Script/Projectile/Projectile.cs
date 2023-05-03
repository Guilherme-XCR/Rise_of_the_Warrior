using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Animator))]


public class Projectile : MonoBehaviour
{
    [SerializeField] private new Rigidbody2D rigidbody;
    [SerializeField] private new BoxCollider2D collider;
    [SerializeField] private Animator anim;

    [SerializeField] public int direction = 0;
    [SerializeField] private float speed = 10f;
    [SerializeField] private bool impact = false;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        rigidbody.gravityScale = 0;
        rigidbody.freezeRotation = true;
        collider = GetComponent<BoxCollider2D>();
        collider.isTrigger = true;
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        Move();
        Flip();
    }

    void Move()
    {
        if (!impact) 
        {
            rigidbody.velocity = new Vector2(direction * speed, 0f);
        }
        else
        {
            rigidbody.velocity = Vector2.zero;
        }
    }

    void Flip()
    {
        if(direction < 0)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 10)
        {
            collision.gameObject.GetComponent<PlayerController>().TakeDamage(10, true);
        }
        anim.SetTrigger("end");
        impact = true;
        Destroy(gameObject, .3f);
    }
}
