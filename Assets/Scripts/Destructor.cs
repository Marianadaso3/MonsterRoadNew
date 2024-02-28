using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Destructor : MonoBehaviour
{
    // Todo lo que haga contacto con el objeto destructor se va a destruir para ocupar menos espacio
    private void OnTriggerEnter(Collider other) 
    {
        Destroy(other.gameObject);
    }

}
