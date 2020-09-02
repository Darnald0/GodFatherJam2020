using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private GameObject player = null;
    [Space]
    [SerializeField] private float camXMovment = 0f;

    private float xOffset = 0f;

    void Update()
    {
        xOffset = player.transform.position.x - transform.position.x;
        Debug.Log(xOffset);
        if (xOffset < -camXMovment)
        {
            transform.position = new Vector3(transform.position.x + xOffset + camXMovment, transform.position.y, transform.position.z);
        }
        else if (xOffset > camXMovment)
        {
            transform.position = new Vector3(transform.position.x + xOffset - camXMovment, transform.position.y, transform.position.z);
        }
    }
}
