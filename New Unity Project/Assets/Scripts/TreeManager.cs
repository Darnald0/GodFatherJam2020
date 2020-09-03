using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeManager : MonoBehaviour
{
    [SerializeField] private Tree[] trees;
    [SerializeField] private Player player;

    void Start()
    {
        trees = GetComponentsInChildren<Tree>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad0))
            PassDay();

        for (int i = 0; i < trees.Length; i++)
        {
            Vector3 posTree = new Vector3(trees[i].gameObject.transform.position.x, 0f, 0f);
            Vector3 posPlayer = new Vector3(player.transform.position.x, 0f, 0f);

            if (Vector3.Distance(posPlayer, posTree) <= 4f)
            {
                trees[i].ShowLife(true);
            }
            else
            {
                trees[i].ShowLife(false);
            }
        }
    }

    public void PassDay()
    {
        for (int i = 0; i < trees.Length; i++)
        {
            if (trees[i].gameObject.activeSelf)
                trees[i].PassDay();
        }
    }

    public void DoubleTreeLife()
    {
        for (int i = 0; i < trees.Length; i++)
        {
            trees[i].DoubleHealth();
        }
    }

    public void DestroyTree()
    {
        int j = trees.Length;
        int k = Random.Range(0, j);
        trees[k].DestroyTree();
    }

    public void DamageTree()
    {
        int j = trees.Length;
        int k = Random.Range(0, j);
        trees[k].EnnemyAttack(1);
    }
}
