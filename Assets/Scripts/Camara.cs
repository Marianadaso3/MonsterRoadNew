using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camara : MonoBehaviour
{
    //Referencia para el movimiento de la camara
    public Movimiento movimiento;
    public float velocidad;

    void Update() //Que se mueva la camara con el personaje en vertical pero en laterales no
    {
     transform.position = Vector3.Lerp(transform.position, Vector3.forward * movimiento.carril, velocidad * Time.deltaTime);
    }
}
