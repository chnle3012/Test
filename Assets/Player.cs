using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    [Header("Movement info")]
    [SerializeField] private float movespeed;
    [SerializeField] private float jumpforce;

    [Header("Dash info")]
    [SerializeField] private float dashSpeed;
    [SerializeField] private float dashDuration;
    private float dashTime;

    [SerializeField] private float dashCoolDown;
    private float dashCoolDownTimer;

    [Header("Attack info")]
    [SerializeField] private float comboTime;
    private float comboTimeWindow;
    private bool isAttacking;
    private int comboCounter;

    private float xinput;


    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();

        Movement();
        CheckInput();
        AnimatorController();
        FlipController();
        CollisonCheck();

        dashTime -= Time.deltaTime;
        dashCoolDownTimer -= Time.deltaTime;
        comboTimeWindow -= Time.deltaTime;

    }

    public void AttackOver()
    {
        isAttacking = false;
        comboCounter++;

        if(comboCounter > 2)
        {
            comboCounter = 0;
        }

        
    }

    private void CheckInput()
    {
        xinput = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            StartAttackEvent();
        }

        if (Input.GetKeyDown(KeyCode.Space)) 
           {
              Jump();
           }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            DashAbility();
        }
    }

    private void StartAttackEvent()
    {
        if (!isGround)
        {
            return;
        }

        if (comboTimeWindow < 0)
        {
            comboCounter = 0;
        }

        isAttacking = true;
        comboTimeWindow = comboTime;
    }

    private void DashAbility()
    {
        if (dashCoolDownTimer < 0 && !isAttacking)
        {
            dashCoolDownTimer = dashCoolDown;
            dashTime = dashDuration;
        }
    }

    private void Movement()
    {
        if (isAttacking)
        {
            Rb.velocity = new Vector2(0, 0);
        }

        else if(dashTime > 0)
        {
            Rb.velocity = new Vector2(facingDir * dashSpeed, 0);
        }

        else
            Rb.velocity = new Vector2(xinput * movespeed, Rb.velocity.y);
    }

    private void Jump()
    {
        if(isGround)
            Rb.velocity = new Vector2(Rb.velocity.x, jumpforce);
    }

    private void AnimatorController()
    {
        bool isMoving = Rb.velocity.x != 0;

        anim.SetFloat("yvelocity", Rb.velocity.y);
        anim.SetBool("isMoving", isMoving);
        anim.SetBool("isGround", isGround);
        anim.SetBool("isDashing", dashTime > 0);
        anim.SetBool("isAttacking", isAttacking);
        anim.SetInteger("comboCounter", comboCounter);
    }

    private void FlipController()
    {
        if(Rb.velocity.x > 0 && !facingRight)
        {
            Flip();
        }
        else if(Rb.velocity.x < 0 && facingRight)
        {
            Flip();
        }
    }
}
