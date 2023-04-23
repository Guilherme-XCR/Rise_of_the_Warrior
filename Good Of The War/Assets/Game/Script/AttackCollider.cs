using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCollider : MonoBehaviour
{
    [SerializeField] private float damage = 5f;
    [SerializeField] private bool pushBack = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 10)
        {
            collision.gameObject.GetComponent<PlayerController>().TakeDamage(damage, pushBack);
        }
    }
}
