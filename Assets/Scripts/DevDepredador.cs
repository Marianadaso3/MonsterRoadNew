using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevDepredador : MonoBehaviour
{
 
    //Definicion de variables 
    //Metodo (triger que lo va a traspasar)
    private void OnTriggerEnter(Collider other) 
    {
        if (other.CompareTag("depredador"))
        {
            other.transform.Translate(0,0, 25); //hacia adelante positivo, hacia atras negativo 
        }    
    }
}
