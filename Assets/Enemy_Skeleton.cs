using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Skeleton : Entity
{
    bool isAttacking;

    [Header("Movement info")]
    [SerializeField] private float moveSpeed;

    [Header("Player detection")]
    [SerializeField] private float playerCheckDistance;
    [SerializeField] private LayerMask WhatIsPlayer;

    private RaycastHit2D isPlayerDetected;

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();

        if (isPlayerDetected)
        {
            if (isPlayerDetected.distance > 1)
            {
                Rb.velocity = new Vector2(moveSpeed * 1.5f * facingDir, Rb.velocity.y);

                Debug.Log("i see you");
                isAttacking = false;
            }
            else
            {
                Debug.Log("attack" + isPlayerDetected);
                isAttacking = true;
            }
        }

        if (!isGround || isWall)
            Flip();

        Movement();
    }

    private void Movement()
    {
        if(!isAttacking)
            Rb.velocity = new Vector2(moveSpeed * facingDir, Rb.velocity.y);
    }

    protected override void CollisonCheck()
    {
        base.CollisonCheck();

        isPlayerDetected = Physics2D.Raycast(transform.position, Vector2.right,playerCheckDistance * facingDir, WhatIsPlayer);
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x + playerCheckDistance * facingDir, transform.position.y));
    }
}
