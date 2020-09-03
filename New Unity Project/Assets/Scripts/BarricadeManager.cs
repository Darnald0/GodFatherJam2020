using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarricadeManager : MonoBehaviour
{
    [Header("Do Not Touch")]
    public List<Barricade> barricades = new List<Barricade>();

    public void DoubleBarricadeHealth()
    {
        for (int i = 0; i < barricades.Count; i++)
        {
            barricades[i].DoubleHealth();
        }
    }
}
