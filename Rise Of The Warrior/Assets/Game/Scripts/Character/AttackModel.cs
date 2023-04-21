using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]

public class AttackModel : MonoBehaviour
{
    [SerializeField] private float damageValue = 0f;
    [SerializeField] private float delayPushBack = 0f;
    [SerializeField] private bool isPushBack = false;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.GetComponent<CharacterModel>().Damage(damageValue, isPushBack, delayPushBack);
    }

}
