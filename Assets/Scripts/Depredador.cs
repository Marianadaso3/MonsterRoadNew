using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Depredador : MonoBehaviour
{
    // Variables 
    public float velocidad; 

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0,0, -velocidad * Time.deltaTime); //para que se mueva el objet (gato)
    }
}
