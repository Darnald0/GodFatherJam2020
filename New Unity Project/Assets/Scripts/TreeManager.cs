using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeManager : MonoBehaviour
{
    private Tree[] trees;
    [SerializeField] private Sky sky;
    [SerializeField] private Player player;
    [SerializeField] private EndWin endWin;
    [Space]
    public Sprite bigTreeDay = null;
    public Sprite bigTreeNight = null;
    public Sprite littleTreeDay = null;
    public Sprite littleTreeNight = null;
    public Sprite deadTreeDay = null;
    public Sprite deadTreeNight = null;

    void Start()
    {
        trees = GetComponentsInChildren<Tree>();
    }

    void Update()
    {
        int treesAlive = trees.Length;
        for (int i = 0; i < trees.Length; i++)
        {
            switch (trees[i].health)
            {
                case 0:
                    treesAlive--;
                    if (!trees[i].littleTree.gameObject.activeSelf)
                    {
                        trees[i].littleTree.gameObject.SetActive(true);
                        trees[i].bigTree.gameObject.SetActive(false);
                    }

                    if (sky.isNight && trees[i].littleTree.sprite != deadTreeNight)
                    {
                        trees[i].littleTree.sprite = deadTreeNight;
                    }
                    else if (!sky.isNight && trees[i].littleTree.sprite != deadTreeDay)
                    {
                        trees[i].littleTree.sprite = deadTreeDay;
                    }
                    break;

                case 1:
                    if (!trees[i].littleTree.gameObject.activeSelf)
                    {
                        trees[i].littleTree.gameObject.SetActive(true);
                        trees[i].bigTree.gameObject.SetActive(false);
                    }

                    if (sky.isNight && trees[i].littleTree.sprite != littleTreeNight)
                    {
                        trees[i].littleTree.sprite = littleTreeNight;
                    }
                    else if (!sky.isNight && trees[i].littleTree.sprite != littleTreeDay)
                    {
                        trees[i].littleTree.sprite = littleTreeDay;
                    }
                    break;

                case 2:
                    if (!trees[i].bigTree.gameObject.activeSelf)
                    {
                        trees[i].littleTree.gameObject.SetActive(false);
                        trees[i].bigTree.gameObject.SetActive(true);
                    }

                    if (sky.isNight && trees[i].bigTree.sprite != bigTreeNight)
                    {
                        trees[i].bigTree.sprite = bigTreeNight;
                    }
                    else if (!sky.isNight && trees[i].bigTree.sprite != bigTreeDay)
                    {
                        trees[i].bigTree.sprite = bigTreeDay;
                    }
                    break;

                default:
                    break;
            }

            Vector3 posTree = new Vector3(trees[i].gameObject.transform.position.x, 0f, 0f);
            Vector3 posPlayer = new Vector3(player.transform.position.x, 0f, 0f);

            if ((trees[i].nbEnemy > 0 || Vector3.Distance(posPlayer, posTree) <= 4f) && trees[i].health > 0)
            {
                trees[i].ShowLife(true);
            }
            else
            {
                trees[i].ShowLife(false);
            }
        }
        
        if (treesAlive == 0)
        {
            endWin.YouLose();
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
        trees[k].DamageTree(1);
    }
}
