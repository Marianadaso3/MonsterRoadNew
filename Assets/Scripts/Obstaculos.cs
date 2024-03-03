using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstaculos : MonoBehaviour
{
    
    void Start()
    {
        LanzarRayo();
    }

    // Update is called once per frame
    public void LanzarRayo() //Creamos rayo para que no nos quedemos sin poder avanzar
    {
        RaycastHit hit;
        Ray rayo = new Ray(transform.position + Vector3.up * 3 - Vector3.forward, Vector3.down); // colocando posicion en la cual lanzara el rayo
        if (Physics.Raycast(rayo, out hit))//lanzamos rayo y se topa con algo va a preguntar:
        {
            if (hit.collider.CompareTag("roca")) 
            {
                Destroy(hit.transform.gameObject);  //destruyo el objeto para que pueda pasar
            }

        }

        
        

    }
}
