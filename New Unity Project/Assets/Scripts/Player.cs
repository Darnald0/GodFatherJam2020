using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int wood = 0;
    public bool buildBarricade = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!buildBarricade && Input.GetKeyDown(KeyCode.Z))
        {
            GameObject obj = Instantiate(Resources.Load("Barricade"), transform) as GameObject;
            obj.transform.localPosition += new Vector3(2f, 0f);
            buildBarricade = true;
        }
    }


}
