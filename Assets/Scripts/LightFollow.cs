using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFollow : MonoBehaviour
{
    // Variables
    public GameObject thePlayer;
    //Dar efecto a la luz
    public bool titila = false; 
    public float timeDelay;

    // Update is called once per frame
    void Update()
    {
        
        transform.LookAt(thePlayer.transform);
        if (titila == false)
        {
            StartCoroutine(LuzQueTitila());
        }
    }
    IEnumerator LuzQueTitila ()
    {
        titila = true; 
        this.gameObject.GetComponent<Light>().enabled = false;
        timeDelay = Random.Range(0.5f, 1.0f);
        yield return new WaitForSeconds(timeDelay);
        this.gameObject.GetComponent<Light>().enabled = true;
        timeDelay = Random.Range(1.0f, 1.0f);
        yield return new WaitForSeconds(timeDelay);
        titila = false;

    }
}
