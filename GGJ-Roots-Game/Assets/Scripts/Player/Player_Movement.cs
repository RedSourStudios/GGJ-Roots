using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    private Rigidbody2D rb;
    private CapsuleCollider2D capsule;
    private float movement;
    private Vector2 localScaleValue;
    [SerializeField] private float gravity;
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float cutJumpHeight;
    [SerializeField] private float jumpRememberTime;
    [SerializeField] private float jumpRemember;
    [SerializeField] private float groundRememberTime;
    [SerializeField] private float groundRemember;
    [SerializeField] private int hasJumps;
    [SerializeField] private LayerMask groundLayerMask;
    [SerializeField] private LayerMask wallLayerMask;
    [SerializeField] private float extraHeight = 0.1f;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private bool isRootWall;
    [SerializeField] private bool isTouchingWall;
    [SerializeField] private WallCheck _WallCheck;

    void Start()
    {
        isRootWall = false;

        localScaleValue = transform.localScale;
        capsule = GetComponent<CapsuleCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = gravity;
    }

    void Update()
    {
        Move();
        GroundCheck();
        Jump();
        Flip(); //girar 180° no eixo Y

        if (Input.GetKeyDown(KeyCode.J))
            WallRoot();

        if (Input.GetKeyDown(KeyCode.K))
            RootTeleport();
    }

    void Move()
    {
        movement = Input.GetAxisRaw("Horizontal"); //Não utilizei o novo sistema de input
        if((movement > 0 || movement < 0) && !isRootWall) {
            rb.velocity = new Vector2(movement * speed, rb.velocity.y);
        }
        
    }

    void Jump() //preciso arrumar
    {
        //função isGrounded possibilita o pulo
        //timer de grounded funciona para pular até 0.x segundos depois de deixar o chão
        //timer de jumpremember funciona para pular caso aperte espaço 0.x segundos antes de tocar o chão

        jumpRemember -= Time.deltaTime;

        if(Input.GetKeyDown(KeyCode.Space)) {
            
            jumpRemember = jumpRememberTime;
            
            // if(isJumping) { //Estava testando a mecânica de enraizar nas paredes
            //     RaycastHit2D raycastRootHit = Physics2D.Raycast(capsule.bounds.center, Vector2.right, extraHeight, wallLayerMask);
            //     if(raycastRootHit.collider != null) {
            //         Debug.Log("Pode enraizar");
            //     }
            // } //(capsule.bounds.size.x + 1)

        }

        if(Input.GetKeyUp(KeyCode.Space)) {
            if(rb.velocity.y > 0) {
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * cutJumpHeight);
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) && isRootWall)
        {
            jumpRemember = 100;
            groundRemember = 100;
            hasJumps = 1;
            WallRoot();
        }

        if((groundRemember > 0) && (jumpRemember > 0) && hasJumps > 0) { //pulo
                // rb.velocity = new Vector2(rb.velocity.x, 0);
                // rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse); //fazer o pulo com forceMode ou rb.velocity.y?

                rb.velocity = new Vector2(rb.velocity.x, jumpForce);

                jumpRemember = 0;
                groundRemember = 0;
            }
    }

    void GroundCheck() //função para setar groundRemember e impossibilitar qualquer tipo de pulo duplo se aproveitando dos timers
    {
        groundRemember -= Time.deltaTime;

        if(isGrounded()) {
            hasJumps = 1;
            groundRemember = groundRememberTime;
        }
        else {
            hasJumps = 0;
        }
    }

    public bool isGrounded()
    {
        //Não coloquei overlapAll e nem adicionei algum valor ao "capsule.bounds.size" para evitar pular enquanto estiver no ar e quase alcançando alguma plataforma
        RaycastHit2D raycastHit = Physics2D.BoxCast(capsule.bounds.center, capsule.bounds.size, 0f, Vector2.down, extraHeight, groundLayerMask);
    
        return raycastHit.collider != null;
    }

    void Flip() {
        if(movement > 0f) {
            transform.localScale = new Vector2(localScaleValue.x, localScaleValue.y);
            //transform.eulerAngles = new Vector3(0f, 0f, 0f);
        }
        if(movement < 0f) {
            transform.localScale = new Vector2(-localScaleValue.x, localScaleValue.y);
            //transform.eulerAngles = new Vector3(0f, 180f, 0f);
        }
    }

    #region WallRoot

    /*private bool IsRoot() 
    {
        return _WallCheck.isTouchingWall;
        //return Physics2D.OverlapCircle(wallCheck.position, 0.2f, wallLayerMask);
    }*/

    void WallRoot() {
        if (isRootWall)
        {
            isRootWall = false;
            rb.gravityScale = gravity;
        }
        else if(_WallCheck.isTouchingWall && !isGrounded()) { //horizontal != 0f
            Debug.Log("Grudou");
            isRootWall = true;
            rb.gravityScale = 0f;
            rb.velocity = Vector2.zero;

        }
    }

    #endregion

    #region RootTeleport

    private void RootTeleport()
    {
        Vector2 movementVect = new Vector2(Input.GetAxisRaw("Horizontal"), 
            Input.GetAxisRaw("Vertical"));
        
        if (Mathf.Abs(movementVect.normalized.x) <= -movementVect.normalized.y)
        {
            /*RaycastHit2D[] forwardCast = Physics2D.RaycastAll(transform.position, 
                movementVect.normalized, 15f, groundLayerMask);*/
            RaycastHit2D[] backwardsCast = Physics2D.RaycastAll(
                new Vector2(transform.position.x, transform.position.y - 15f), 
                Vector2.up, 15f, groundLayerMask);

            transform.position = backwardsCast[backwardsCast.Length - 1].point;
        }
        
        
    }

    #endregion

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
    }
}
