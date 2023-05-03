using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(IInputController))]



public class PlayerController : MonoBehaviour
{
    private new Rigidbody2D rigidbody;
    private new CapsuleCollider2D collider;
    private LayerMask groundLayer;
    private Animator anim;
    private Transform enemy;
    private IInputController input;
    private GameObject projectile;
    [SerializeField] private Transform pivo;

    //vida
    [SerializeField] public float health = 100;
    [SerializeField] private float healthMax = 100;
    [SerializeField] private Image healthBar;          // -------------


    [SerializeField] public string animator;
    [SerializeField] private int directionMove;
    [SerializeField] private float movementSpeed = 3f;
    [SerializeField] private float jumpForce = 15f;
    [SerializeField] private bool isDead = false;
    [SerializeField] private bool isAttacking = false;
    [SerializeField] private bool isStunned = false;
    [SerializeField] private bool isPushBack = false;
    [SerializeField] private bool isLookRight = false;
    [SerializeField] private bool isGrounded = false;
    [SerializeField] private bool isCrouching = false;
    [SerializeField] private bool cooldownCombo = false;



    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        collider = GetComponent<CapsuleCollider2D>();
        anim = GetComponent<Animator>();
        input = GetComponent<IInputController>();

        rigidbody.freezeRotation = true;
        rigidbody.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        rigidbody.gravityScale = 3;

        anim.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>(animator);

        if(gameObject.tag == "Player1")
        {
            enemy = GameObject.FindWithTag("Player2").transform;
        }
        else
        {
            enemy = GameObject.FindWithTag("Player1").transform;
        }

        groundLayer = LayerMask.GetMask("Ground");


        if(animator == "Caera")
        {
            projectile = Resources.Load<GameObject>("Caera_Projectile");
        }
        else if(animator == "Lorrigan")
        {
            projectile = Resources.Load<GameObject>("Lorrigan_Projectile");
        }
        else if (animator == "Ragnar")
        {
            projectile = Resources.Load<GameObject>("Ragnar_Projectile");
        }
        
    }



    void Update()
    {
        if (!isDead)
        {
            Flip();
            IsGrounded();
            AnimationController();
            InputsRecive();
            Movement();
            StopMovement();
            CoolDownCombo();
            pushBackMovement();

        }
        else
        {
            rigidbody.velocity = new Vector2(0f, rigidbody.velocity.y);
        }
        UpdateHealthBar();






    }

    

    /*
     * =-=-=-=-=-=-=-=-=-=-=- AnimationController -=-=-=-=-=-=-=-=-=-=-=-
    */
    public void AnimationController()
    {
        if (isLookRight)
        {
            anim.SetFloat("HorizontalSpeed", rigidbody.velocity.x / movementSpeed);
        }
        else
        {
            anim.SetFloat("HorizontalSpeed", -(rigidbody.velocity.x / movementSpeed));
        }

        anim.SetBool("isGrounded", isGrounded);
        anim.SetBool("isCrouching", isCrouching);
        anim.SetBool("CooldownCombo", cooldownCombo);

    }

    /*
     * =-=-=-=-=-=-=-=-=-=- Flip -=-=-=-=-=-=-=-=-=-=-=-=-
    */
    private void Flip()
    {
        if (transform.position.x - enemy.position.x < 0)
        {
            isLookRight = true;
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else
        {
            isLookRight = false;
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
    }

    /*
     * =-=-=-=-=-=-=-=-=-=-=- Movement -=-=-=-=-=-=-=-=-=-=-=-
    */
    public void Movement()
    {
        if (isGrounded && !isStunned && !isAttacking && !isCrouching)
        {
            rigidbody.velocity = new Vector2(directionMove * movementSpeed, rigidbody.velocity.y);
        }
    }

    public void StopMovement()
    {
        if(isAttacking || isCrouching ||isStunned)
        {
            rigidbody.velocity = new Vector2(0f, rigidbody.velocity.y);
        }
    }

    public void pushBackMovement()
    {
        if (isPushBack)
        {
            if (isLookRight)
            {
                rigidbody.velocity = new Vector2(-5f, 0f);
            }
            else
            {
                rigidbody.velocity = new Vector2(5f, 0f);
            }
        }
    }

    public void Jump()
    {
        if (isGrounded && !isStunned && !isAttacking && !isCrouching)
        {
            rigidbody.velocity = new Vector2(directionMove, jumpForce);
        }
    }

    public void Crouch()
    {
        isCrouching = true;
        StopMovement();
    }
    public void UnCrouch()
    {
        isCrouching = false;
    }
    public void BasicAttack()
    {

        if (!isStunned)
        {
            anim.SetTrigger("BasicAttack");
        }
    }
    public void DistanceAttack()
    {
        if (isGrounded && !isStunned && !isCrouching && !isAttacking)
        {
            anim.SetTrigger("DistanceAttack");
            StartCoroutine(CreateDistanceAttack());

        }
    }
    public void SpecialAttack()
    {
        if (isGrounded && !isStunned && !isCrouching && !isAttacking)
        {
            anim.SetTrigger("SpecialAttack");
        }
    }

    void CoolDownCombo()
    {
        if (cooldownCombo)
        {
            StartCoroutine(CooldownComboCorroutine());
        }
    }

    /*
     * =-=-=-=-=-=-=-=-=-=- Inputs -=-=-=-=-=-=-=-=-=-=-=-=-
    */

    public void InputsRecive()
    {
        directionMove = input.DirectionMove();

        if (input.Jump())
        {
            Jump();
        }
        if (input.Crouch())
        {
            Crouch();
        }
        if (input.UnCrouch())
        {
            UnCrouch();
        }
        if (input.BasicAttack())
        {
            BasicAttack();
        }
        if (input.DistanceAttack())
        {
            DistanceAttack();
        }
        if (input.SpecialAttack())
        {
            SpecialAttack();
        }
    }


    /*
     * =-=-=-=-=-=-=-=-=-=-=- Colisão -=-=-=-=-=-=-=-=-=-=-=-
    */

    private void IsGrounded()
    {
        RaycastHit2D ground = Physics2D.BoxCast(collider.bounds.center, collider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        if (ground.collider != null)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }

    public void TakeDamage(float damage, bool pushBack)
    {
        if (!isDead)
        {
            isStunned = true;
            health -= damage;

            if (health <= 0)
            {
                isDead = true;
                anim.SetTrigger("Death");
            }
            else
            {
                anim.SetTrigger("Damage");
            }

            StartCoroutine(CooldownDamage());

            if (pushBack)
            {
                isStunned = false;
                isPushBack = true;
                
            }
        }
    }

    /*
     * =-=-=-=-=-=-=-=-=-=- Corroutinas -=-=-=-=-=-=-=-=-=-=-=-=-
    */

    IEnumerator CooldownDamage()
    {
        yield return new WaitForSeconds(1f);
        isPushBack = false;
        isStunned = false;
    }

    IEnumerator CooldownComboCorroutine()
    {
        yield return new WaitForSeconds(.5f);
        anim.ResetTrigger("BasicAttack");
        cooldownCombo = false;
    }

    IEnumerator CreateDistanceAttack()
    {
        yield return new WaitForSeconds(.5f);
        
        GameObject intanciaProjectile = Instantiate(projectile, pivo.position, Quaternion.identity);
        if(intanciaProjectile != null)
        {
            if (isLookRight)
            {
                intanciaProjectile.GetComponent<Projectile>().direction = 1;
            }
            else
            {
                intanciaProjectile.GetComponent<Projectile>().direction = -1;
            }
        }

    }


    /*
     * =-=-=-=-=-=-=-=-=-=- Controle da barra de vida -=-=-=-=-=-=-=-=-=-=-=-=-
    */
    private void UpdateHealthBar()
    {
        healthBar.fillAmount = health / healthMax;
    }


    /*
     * =-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-
    */


    /*
     * =-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-
    */




}
