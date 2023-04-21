using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Animator))]

public class CharacterModel : MonoBehaviour
{
    //-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-==-=-=-=-=-=-=-=-=-=-=-
    // ATRIBUTOS
    //-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-==-=-=-=-=-=-=-=-=-=-=-

    [SerializeField] Transform enemy;
    [SerializeField] private LayerMask groundLayer;
    
    Rigidbody2D rigidBody;
    new Collider2D collider;
    Vector2 colliderOriginalSize;
    Vector2 colliderOriginalOffset;
    
    Animator anim;

    [SerializeField] private float movementSpeed = 3f;
    [SerializeField] private float jumpForce = 15f;
    [SerializeField] private float life = 100;
    [SerializeField] private bool isDead = false;


    public bool isAttacking = false;
    public bool isCrouching = false;
    public bool isStunned = false;

    public bool animationStop = false;
    public bool isPushBack = false;



    //-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-==-=-=-=-=-=-=-=-=-=-=-
    // METODO START
    //-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-==-=-=-=-=-=-=-=-=-=-=-
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();

        colliderOriginalSize = collider.bounds.size;
        colliderOriginalOffset = collider.offset;

        rigidBody.freezeRotation = true;
        rigidBody.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        rigidBody.gravityScale = 3;
    }
    private void Update()
    {
        Flip();
        AnimationController();
        IsDead();

        if (animationStop)
        {
            StopMovement();
        }

        if (isPushBack)
        {
            PushBack();
        }
    }
    private void Flip()
    {
        if (IsLookRight())
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
    }
    private void IsDead()
    {
        if(life <= 0)
        {
            isDead = true;
            StopMovement();
        
        }
        else
        {
            isDead = false;
        }
    }
    public void StopMovement()
    {
        rigidBody.velocity = new Vector2(0f, rigidBody.velocity.y);
    }
    public void PushBack()
    {
            if (IsLookRight())
            {
                rigidBody.velocity = new Vector2(-5, rigidBody.velocity.y);
            }
            else
            {
                rigidBody.velocity = new Vector2(5, rigidBody.velocity.y);
            }
    }


    //-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-==-=-=-=-=-=-=-=-=-=-=-
    // METODO REFERENTE A ANIMAÇÃO
    //-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-==-=-=-=-=-=-=-=-=-=-=-
    public void AnimationController()
    {
        anim.SetFloat("HorizontalSpeed", rigidBody.velocity.x / movementSpeed);
        anim.SetFloat("VerticalSpeed", rigidBody.velocity.y / jumpForce);

        anim.SetBool("isLookRight", IsLookRight()); 
        anim.SetBool("isGrounded", IsGrounded()); 
        anim.SetBool("isCrouching", isCrouching);
        anim.SetBool("isDead", isDead);

    }

    public void Presentation()
    {
        anim.ResetTrigger("Start");
    }

    //-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-==-=-=-=-=-=-=-=-=-=-=-
    // METODOS PUBLICOS
    //-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-==-=-=-=-=-=-=-=-=-=-=-
    public void Movement(int direction)
    {
        if (Idle())
        {
            rigidBody.velocity = new Vector2(direction * movementSpeed, rigidBody.velocity.y);
        }
        
    }
    public void Jump(int direction)
    {
        if (Idle())
        {
            rigidBody.velocity = new Vector2(direction * jumpForce / 2, jumpForce);
        }
    }
    public void Crouch()
    {
        isCrouching = true;
        rigidBody.velocity = new Vector2(0f, rigidBody.velocity.y);

        //"ABAIXANDO" o colisor
        GetComponent<CapsuleCollider2D>().size = new Vector2(colliderOriginalSize.x, colliderOriginalSize.y / 1.7f);
        GetComponent<CapsuleCollider2D>().offset = new Vector2(0f, -(colliderOriginalSize.y / 4) + (colliderOriginalOffset.y / 4));
    }
    public void UnCrouch() {
        isCrouching = false;
        //"LEVANTANDO" o colisor
        GetComponent<CapsuleCollider2D>().size = colliderOriginalSize;
        GetComponent<CapsuleCollider2D>().offset = colliderOriginalOffset;
    }
    public void BasicAttack()
    {
        if (!isAttacking)
        {
            anim.SetTrigger("BasicAttack");

        }
    }
    public void DistanceAttack()
    {
        if (!isAttacking)
        {
            anim.SetTrigger("DistanceAttack");
        }
    }

    public void SpecialAttack()
    {
        if (!isAttacking)
        {
            anim.SetTrigger("SpecialAttack");
        }
    }

    public void Damage(float damageValue, bool isPB, float delayPushBack)
    {
        anim.SetTrigger("Hurt");
        life -= damageValue;

        if (isPB)
        {
            StartCoroutine(PushBackCorrotine(delayPushBack));
        }
    }

    //-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-==-=-=-=-=-=-=-=-=-=-=-
    // METODOS PRIVADOS
    //-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-==-=-=-=-=-=-=-=-=-=-=-

    // Verifica para qual lado o personagem esta olhando
    private bool IsLookRight()
    {
        if (transform.position.x - enemy.position.x < 0)
        {
            return true;
        }
        return false;
    }

    // Verifica se o personagem esta ocioso
    private bool Idle()
    {
        if (IsGrounded() && !isStunned && !isAttacking && !isCrouching)
        {
            return true;
        }
        return false;
    }
    
    // Verifica se o personagem esta encostado no chão
    private bool IsGrounded()
    {
        RaycastHit2D ground = Physics2D.BoxCast(collider.bounds.center, collider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return ground.collider != null;
    }
    //-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-==-=-=-=-=-=-=-=-=-=-=-
    // COROTINES
    //-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-==-=-=-=-=-=-=-=-=-=-=-

    IEnumerator PushBackCorrotine(float delay)
    {
        yield return new WaitForSeconds(delay);
        isPushBack = true;
        anim.SetTrigger("Hurt");
        yield return new WaitForSeconds(0.5f);
        isPushBack = false;
    }
}
