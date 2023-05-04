using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirections), typeof(Damageable))]
public class Slime : MonoBehaviour
{
    public float walkSpeed = 3f;
    public float walkStopRate = 0.05f;
    public DetectionZone attackZone;
    public DetectionZone edgeDetection;
    Rigidbody2D rb;
    Animator animator;
    TouchingDirections touchingDirections;
    public enum WalkingDirection { Right, Left }
    Damageable damageable;

    private Vector2 walkDirectionVector = Vector2.right;
    private WalkingDirection _walkDirection;

    public WalkingDirection WalkDirection
    {
        get
        { 
            return _walkDirection; 
        }
        private set
        {
            if (_walkDirection != value)
            {
                gameObject.transform.localScale = new Vector2(gameObject.transform.localScale.x * -1, gameObject.transform.localScale.y);

                if (value == WalkingDirection.Right)
                {
                    walkDirectionVector = Vector2.right;
                }
                else if (value == WalkingDirection.Left)
                {
                    walkDirectionVector = Vector2.left;
                }
            }

            _walkDirection = value;
        }
    }

    public bool _targetLock = false;

    public bool TargetLock
    {
        get
        {
            return _targetLock;
        }
        private set
        {
            _targetLock = value;
            animator.SetBool(AnimationStrings.targetLock, value);
        }
    }

    public bool EnableMove
    {
        get
        {
            return animator.GetBool(AnimationStrings.enableMove);
        }
    }

    public float AtkCooldown
    {
        get
        {
            return animator.GetFloat(AnimationStrings.AtkCooldown);
        }
        set
        {
            animator.SetFloat(AnimationStrings.AtkCooldown, Mathf.Max(value, 0));
        }
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        touchingDirections = GetComponent<TouchingDirections>();
        animator = GetComponent<Animator>();
        damageable = GetComponent<Damageable>();
    }

    // Update is called once per frame
    private void Update() 
    {
        TargetLock = attackZone.detectedColliders.Count > 0;

        if (AtkCooldown > 0)
        {
            AtkCooldown -= Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        if (touchingDirections.IsGrounded && touchingDirections.IsOnWall)
        {
            FlipDirection();
        }
        if(!damageable.LockVelocity)
        {
            if(EnableMove)
                rb.velocity = new Vector2(walkSpeed * walkDirectionVector.x, rb.velocity.y);
            else
                rb.velocity = new Vector2(Mathf.Lerp(rb.velocity.x, 0, walkStopRate), rb.velocity.y);
        }
    }

    private void FlipDirection()
    {
        if (WalkDirection == WalkingDirection.Right)
        {
            WalkDirection = WalkingDirection.Left;
        }
        else if (WalkDirection == WalkingDirection.Left)
        {
            WalkDirection = WalkingDirection.Right;
        }
        else
        {
            Debug.LogError("Current direction is neither left nor right!");
        }
    }
   
    public void OnHit(int damage, Vector2 knockback)
    {
        rb.velocity = new Vector2(knockback.x, rb.velocity.y + knockback.y);
    }

    public void OnEdgeDetection()
    {
        if(touchingDirections.IsGrounded)
        {
            FlipDirection();
        }
    }
}
