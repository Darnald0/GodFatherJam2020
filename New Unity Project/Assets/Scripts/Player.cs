using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int wood = 0;
<<<<<<< HEAD
=======
    [SerializeField] private GameObject village;
>>>>>>> 60317639eab8868bec6e57ccf4e6a15d6401ac6c

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.collider.tag == "Village" && Input.GetKeyDown(KeyCode.S))
        {
           if(wood >= village.GetComponent<Village>().costToBuild)
            {
                village.GetComponent<Village>().BuildHouse();
            }
        }
    }
}
