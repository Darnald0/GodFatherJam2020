using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeArrow : MonoBehaviour
{
    [SerializeField] private GameObject arrow;

    private float onePercent = 4f;
    private Vector2 newPosition;
    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        newPosition = new Vector2(arrow.transform.localPosition.x, 0);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer > 1f)
        {
            timer = 0;
            MoveArrow();
        }
    }

    public void MoveArrow()
    {
        newPosition.x += onePercent;
        if(newPosition.x >= 360)
        {
            newPosition.x = 0;
        }
        arrow.transform.localPosition = newPosition;
    }
}
