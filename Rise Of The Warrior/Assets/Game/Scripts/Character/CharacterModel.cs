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
    Animator anim;

    [SerializeField] private float movementSpeed = 3f;
    [SerializeField] private float jumpForce = 15f;


    private bool isAttacking = false;
    private bool isCrouching = false;
    private bool isStunned = false;



    //-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-==-=-=-=-=-=-=-=-=-=-=-
    // METODO START
    //-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-==-=-=-=-=-=-=-=-=-=-=-
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();

        colliderOriginalSize = collider.bounds.size;

        rigidBody.freezeRotation = true;
        rigidBody.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        rigidBody.gravityScale = 3;
    }
    //-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-==-=-=-=-=-=-=-=-=-=-=-
    // METODO REFERENTE A ANIMA��O
    //-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-==-=-=-=-=-=-=-=-=-=-=-




    //-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-==-=-=-=-=-=-=-=-=-=-=-
    // METODOS PUBLICOS
    //-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-==-=-=-=-=-=-=-=-=-=-=-
    //Deixa o personagem de frente para o enimigo.
    public void Flip()
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

    public void Movement(int direction)
    {
        //Caso n�o esteja ocupado em outra a��o ele pode se mover
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
        GetComponent<CapsuleCollider2D>().size = new Vector2(colliderOriginalSize.x, colliderOriginalSize.y / 2);
        GetComponent<CapsuleCollider2D>().offset = new Vector2(0f, -((colliderOriginalSize.y / 2) / 2));
    }
    public void UnCrouch() {
        isCrouching = false;
        //"LEVANTANDO" o colisor
        GetComponent<CapsuleCollider2D>().size = colliderOriginalSize;
        GetComponent<CapsuleCollider2D>().offset = Vector2.zero;
    }
    public void PunchAttack()
    {

    }
    public void KickAttack()
    {

    }

    public void SpecialAttak()
    {

    }

    //-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-==-=-=-=-=-=-=-=-=-=-=-
    // METODOS PRIVADOS
    //-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-==-=-=-=-=-=-=-=-=-=-=-

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
    
    // Verifica se o personagem esta encostado no ch�o
    private bool IsGrounded()
    {
        RaycastHit2D ground = Physics2D.BoxCast(collider.bounds.center, collider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return ground.collider != null;
    }
    //-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-==-=-=-=-=-=-=-=-=-=-=-
    //-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-==-=-=-=-=-=-=-=-=-=-=-
}
