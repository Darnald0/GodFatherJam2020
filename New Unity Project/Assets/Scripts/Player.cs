using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Do Not Touch")]
    public int wood = 0;
    public bool buildBarricade = false;
    public bool isBuilding = false;
    public bool isStayingIdol = false;

    [Header("Change This")]
    [SerializeField] private GameObject barricadePrefab = null;
    public BarricadeManager barricadeManager = null;

    void Update()
    {
        if (!buildBarricade && !isStayingIdol && Input.GetKeyDown(KeyCode.S))
        {
            BuildBarricade();
        }
    }

    private void BuildBarricade()
    {
        GameObject obj = Instantiate(barricadePrefab, transform) as GameObject;
        obj.transform.localPosition += new Vector3(-4f, 1.5f);
        obj.transform.localScale = new Vector3(2f, 2f, 1f);
        buildBarricade = true;
    }

    public void HalfWood()
    {
        wood = wood / 2;
    }

    public void DoubleWood()
    {
        wood = wood * 2;
    }
}
