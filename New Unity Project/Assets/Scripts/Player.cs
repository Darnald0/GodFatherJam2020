using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [Header("Do Not Touch")]
    public int wood = 0;
    public bool buildBarricade = false;
    public bool isBuilding = false;
    public bool isStayingIdol = false;
    public bool isFocusingEnemy = false;
    public bool isCuttingWood = false;
    [SerializeField] private Text woodText;

    [Header("Change This")]
    [SerializeField] private GameObject barricadePrefab = null;
    public BarricadeManager barricadeManager = null;
    public float timeToAttack = 1f;

    private float timerAttack = 0f;
    private Animator animator;

    private int lastWood = 0;

    private void Start()
    {
        timerAttack = timeToAttack;
        animator = GetComponent<Animator>();
        woodText = woodText.GetComponent<Text>();
        woodText.text = wood.ToString();
    }

    void Update()
    {
        if (lastWood != wood)
        {
            woodText.text = wood.ToString();
            lastWood = wood;
        }

        if (isBuilding && !animator.GetBool("Constructing"))
        {
            animator.SetBool("Idle", false);
            animator.SetBool("Walking", false);
            animator.SetBool("Running", false);
            animator.SetBool("Constructing", true);
        }

        if (timeToAttack > 0f)
            timeToAttack -= Time.deltaTime;

        if (!buildBarricade && !isBuilding && !isStayingIdol && Input.GetKeyDown(KeyCode.S))
        {
            BuildBarricade();
        }
    }

    public void ResetAttack()
    {
        timeToAttack = timerAttack;
        isFocusingEnemy = false;
        if (!animator.GetBool("Scaring"))
        {
            animator.SetBool("Idle", false);
            animator.SetBool("Walking", false);
            animator.SetBool("Running", false);
            animator.SetBool("Constructing", false);
            animator.SetBool("Cutting", false);
            animator.SetBool("Scaring", true);
        }
    }

    public void CuttingTree()
    {
        if (!animator.GetBool("Cutting"))
        {
            animator.SetBool("Idle", false);
            animator.SetBool("Walking", false);
            animator.SetBool("Running", false);
            animator.SetBool("Constructing", false);
            animator.SetBool("Scaring", false);
            animator.SetBool("Cutting", true);
        }
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
