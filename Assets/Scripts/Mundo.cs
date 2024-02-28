using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mundo : MonoBehaviour
{
    // Variables
    public int carril = 0;
    public GameObject[] pisos;

    public void CrearPiso()
    {   //vectorforward (0,0,1), por ende si lo multiplicamos por el carril en z siempre nos quedara el valor del carril 
        Instantiate(pisos[Random.Range(0, pisos.Length)], Vector3.forward * carril, Quaternion.identity);
    }
}
