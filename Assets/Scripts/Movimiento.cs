using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

public class Movimiento : MonoBehaviour
{
    //Definicion de variables
    public int carril;
    public int lateral;
    public Vector3 posObjetivo; //variable que me indica la poscion a la que debo de ir 
    public float velocidad;
    //Referencia del objeto del script mundo
    public Mundo mundo; 
    int posicionZ;
    void Start()
    {
        posicionZ = 0;
    }

    // Update is called once per frame
    void Update()
    {   
        ActualizarPosicion();
        
        if (Input.GetKeyDown(KeyCode.W)) //definimos la tecla para avanzar
        {
            Avanzar();
        } 
        else if (Input.GetKeyDown(KeyCode.S)) //tecla para retroceder
        {
            Retroceder();
        }  
        else if (Input.GetKeyDown(KeyCode.D)) //tecla para el lado derecho
        {
            MoverLados(1);
        }   
        else if (Input.GetKeyDown(KeyCode.A)) //tecla para el lado izquierdo
        {  
            MoverLados(-1);
        }  
    }

    //Metodo que me sirve para actualizar la posicion del personaje
    public void ActualizarPosicion()
    {
        posObjetivo = new Vector3 (lateral, 0, posicionZ); //para que no se pierda de carril
        //transform.position = posObjetivo;
        transform.position = Vector3.Lerp(transform.position, posObjetivo, velocidad * Time.deltaTime); //Agregamos velocidad
    }
    //Metodo que me sirve para avanzar el personaje
    public void Avanzar()
    {
        posicionZ++;

        if (posicionZ > carril) //inversa
        {
           carril = posicionZ; //movimiento 
           mundo.CrearPiso(); //Creara piso segun movimiento
        }
    }

    //Metodo que me sirve para retroceder el personaje
    public void Retroceder()
    {
        if (posicionZ > carril -3)
        {
            posicionZ--; //ya no se va a ejecutar 
        }
    }
    
    //Metodo para que el personaje se mueva a los lados
    public void MoverLados(int cuanto)
    {
        lateral += cuanto; //mas uno o menos uno
        lateral = Mathf.Clamp(lateral, -4, 4); //Poner un minimo y un maximo
    }
}
