using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rig = null;
    private float Horizontal = 0f;

    [SerializeField] private float speed = 40f;

    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Horizontal = Input.GetAxisRaw("Horizontal");
    }

    private void FixedUpdate()
    {
        rig.velocity = new Vector2(Horizontal * 10f * speed * Time.fixedDeltaTime, rig.velocity.y);
    }
}
