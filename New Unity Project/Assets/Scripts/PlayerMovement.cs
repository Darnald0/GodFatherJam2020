using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rig = null;
    private float Horizontal = 0f;

    [SerializeField] private float speed = 40f;

    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Horizontal = Input.GetAxisRaw("Horizontal");
    }

    private void FixedUpdate()
    {
        rig.velocity = new Vector2(Horizontal * 10f * speed * Time.fixedDeltaTime, rig.velocity.y);
    }
}
