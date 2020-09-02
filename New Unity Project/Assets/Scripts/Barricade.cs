using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barricade : MonoBehaviour
{
    private Player player = null;

    [SerializeField] private int costCreate = 0;
    private int costRepair = 0;
    [SerializeField] private int health;

    private void Start()
    {
        player = FindObjectOfType<Player>();
    }
}
