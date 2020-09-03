using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Do Not Touch")]
    public int wood = 0;
    public bool buildBarricade = false;
    public bool isBuilding = false;
    public bool isStayingIdol = false;
    public bool isFocusingEnemy = false;

    [Header("Change This")]
    [SerializeField] private GameObject barricadePrefab = null;
    public BarricadeManager barricadeManager = null;
    public float timeToAttack = 1f;

    private float timerAttack = 0f;

    private void Start()
    {
        timerAttack = timeToAttack;
    }

    void Update()
    {
        if (timeToAttack > 0f)
            timeToAttack -= Time.deltaTime;

        if (!buildBarricade && !isStayingIdol && Input.GetKeyDown(KeyCode.S))
        {
            BuildBarricade();
        }
    }

    public void ResetAttack()
    {
        timeToAttack = timerAttack;
        isFocusingEnemy = false;
    }

    private void BuildBarricade()
    {
        GameObject obj = Instantiate(barricadePrefab, transform) as GameObject;
        obj.transform.localPosition += new Vector3(-6f, 2.8f);
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
