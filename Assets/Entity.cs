using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    protected Rigidbody2D Rb;
    protected Animator anim;

    protected int facingDir = 1;
    protected bool facingRight = true;

    [Header("Collison info")]
    [SerializeField] protected Transform groundCheck;
    [SerializeField] protected float groundCheckDistance;
    [Space]
    [SerializeField] protected Transform wallCheck;
    [SerializeField] protected float wallCheckDistance;
    [SerializeField] protected LayerMask whatIsGround;

    protected bool isGround;
    protected bool isWall;

    protected virtual void Start()
    {
        Rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();

        if (wallCheck == null)
            wallCheck = transform;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        CollisonCheck();

        
    }

    protected virtual void CollisonCheck()
    {
        isGround = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);
        isWall = Physics2D.Raycast(wallCheck.position, Vector2.right, wallCheckDistance * facingDir, whatIsGround);
    }

    protected virtual void Flip()
    {
        facingDir = facingDir * -1;
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }

    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance * facingDir, wallCheck.position.y));
    }
}
