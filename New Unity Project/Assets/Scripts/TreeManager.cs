using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeManager : MonoBehaviour
{
    [SerializeField] private Tree[] trees;

    void Start()
    {
        trees = GetComponentsInChildren<Tree>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad0))
            PassDay();
    }

    public void PassDay()
    {
        for (int i = 0; i < trees.Length; i++)
        {
            if (trees[i].gameObject.activeSelf)
                trees[i].PassDay();
        }
    }
}
