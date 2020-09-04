using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rig = null;
    private float Horizontal = 0f;

    private bool rightFace = true;

    [SerializeField] private float speed = 30f;
    [SerializeField] private float runSpeed = 40f;

    private Player player = null;
    private Animator animator;
    private bool isWalking = false;
    private bool isRunning = false;

    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        player = GetComponent<Player>();
        animator = GetComponent<Animator>();
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
    }

    void Update()
    {
        isRunning = Input.GetKey(KeyCode.Space);

        if (!player.isBuilding && !player.isStayingIdol && !player.isCuttingWood)
        {
            Horizontal = Input.GetAxisRaw("Horizontal");

            if ((Horizontal == -1 || Horizontal == 1) && !isRunning)
                isWalking = true;
            else
                isWalking = false;

            if (Horizontal < 0 && !rightFace)
                Flip();
            else if (Horizontal > 0 && rightFace)
                Flip();
        }

        if (rig.velocity.x < 0.05f && rig.velocity.x > -0.05f && !isWalking)
        {
            if (!animator.GetBool("Idle"))
            {
                animator.SetBool("Walking", false);
                animator.SetBool("Running", false);
                animator.SetBool("Constructing", false);
                animator.SetBool("Scaring", false);
                animator.SetBool("Cutting", false);
                animator.SetBool("Idle", true);
            }
        }
        else if (isWalking)
        {
            if (!animator.GetBool("Walking"))
            {
                animator.SetBool("Idle", false);
                animator.SetBool("Constructing", false);
                animator.SetBool("Running", false);
                animator.SetBool("Scaring", false);
                animator.SetBool("Cutting", false);
                animator.SetBool("Walking", true);
            }
        }
        else if (isRunning)
        {
            if (!animator.GetBool("Running"))
            {
                animator.SetBool("Idle", false);
                animator.SetBool("Walking", false);
                animator.SetBool("Cutting", false);
                animator.SetBool("Scaring", false);
                animator.SetBool("Running", true);
            }
        }
    }

    private void FixedUpdate()
    {
        if (!player.isBuilding && !player.isStayingIdol && !player.isCuttingWood)
        {
            rig.velocity = new Vector2(Horizontal * (isRunning ? runSpeed : speed) * 10f * Time.fixedDeltaTime, rig.velocity.y);
        }
        else
        {
            if (rig.velocity.x > 0.1f || rig.velocity.x < -0.1f)
                rig.velocity = new Vector2(0f, rig.velocity.y);
        }
    }

    private void Flip()
    {
        rightFace = !rightFace;

        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}
