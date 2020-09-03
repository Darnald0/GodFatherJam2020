﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rig = null;
    private float Horizontal = 0f;

    private bool rightFace = true;

    [SerializeField] private float speed = 40f;

    private Player player = null;

    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        player = GetComponent<Player>();
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
    }

    void Update()
    {
        if (!player.isBuilding && !player.isStayingIdol)
        {
            Horizontal = Input.GetAxisRaw("Horizontal");

            if (Horizontal < 0 && !rightFace)
                Flip();
            else if (Horizontal > 0 && rightFace)
                Flip();
        }
    }

    private void FixedUpdate()
    {
        if (!player.isBuilding && !player.isStayingIdol)
            rig.velocity = new Vector2(Horizontal * 10f * speed * Time.fixedDeltaTime, rig.velocity.y);
    }

    private void Flip()
    {
        rightFace = !rightFace;

        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}
