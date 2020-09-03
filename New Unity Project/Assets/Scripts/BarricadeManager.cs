using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarricadeManager : MonoBehaviour
{
    public List<Barricade> barricades = new List<Barricade>();

    public void DoubleBarricadeHealth()
    {
        for (int i = 0; i < barricades.Count; i++)
        {
            barricades[i].DoubleHealth();
        }
    }
}
