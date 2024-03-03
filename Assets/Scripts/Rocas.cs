using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocas : MonoBehaviour
{
    // Variables 
    public GameObject roca;
    
    void Start()
    {
        LanzarRayo();
    }

    // Update is called once per frame
    public void LanzarRayo()
    {
        RaycastHit hit;
        Ray rayo = new Ray(transform.position + Vector3.up * 3 - Vector3.forward, Vector3.down);
        
        if (Physics.Raycast(rayo, out hit))
        {
            if (hit.collider.CompareTag("agua"))
            {
                // Obtén la escala actual de la roca
                Vector3 escalaRoca = roca.transform.localScale;
                
                // Instancia la roca manteniendo la escala actual
                GameObject nuevaRoca = Instantiate(roca, transform.position - Vector3.forward, transform.rotation);
                
                // Aplica la escala a la nueva roca
                nuevaRoca.transform.localScale = escalaRoca;
            } 
            else if (hit.collider.CompareTag("obstaculo"))
            {
                Destroy(hit.transform.gameObject);
            }
        }
    }
}
