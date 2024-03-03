using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camara : MonoBehaviour
{
    public float velocidadMovimiento;

    void Update()
    {
        // Mueve la cámara hacia adelante continuamente
        transform.Translate(Vector3.forward * velocidadMovimiento * Time.deltaTime);

        // Mantén la misma altura que el personaje (puedes ajustar el valor de 'y' según tu escena)
        float nuevaAltura = transform.position.y;

        // Mantén la misma posición lateral que el personaje
        float nuevaLateral = transform.position.x; // o movimiento.lateral si lo prefieres

        // Establece la nueva posición de la cámara
        transform.position = new Vector3(nuevaLateral, nuevaAltura, transform.position.z);
    }
}
