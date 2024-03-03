using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using Unity.VisualScripting;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;
using UnityEngine.Networking;

public class Movimiento : MonoBehaviour
{
    //Definicion de variables
    public int carril;
    public int lateral;
    public Vector3 posObjetivo; //variable que me indica la poscion a la que debo de ir 
    public float velocidad;
    //Referencia del objeto del script mundo
    public Mundo mundo;
    public Transform grafico;  //para que gire el personaje 
    public LayerMask capaObstaculos; //cual es la capa donde buscara los obstaculos
    public float distanciaVista = 1; //la que se utiliza para el rayo en vez de 1.3f
    public bool vivo = true;//para que el personaje pueda morir
     public LayerMask capaAgua;//capa de agua 
    public int posicionZ;
    public Animator animaciones;//animaciones
    public AnimationCurve curva;//curva de animacion (SALTO PROGRAMADO)
    bool bloqueo = false; // para bloquear el salto 
    public float tazaIncremento; //para aumentar el nivel de dificultad
    public float escalaInicial = 0.8f; //inicializamos de una vez con la velocidad en la que empiece
    void Start()
    {
        Time.timeScale = escalaInicial; //el inicio del juego empieze mas lento
        //Se ejecuta cada medio segundo NO cada frame 
        InvokeRepeating("MirarAgua", 1, 0.5f); //sirve para poner el metodo entre comillas y entre cuantos segudos se va a reializar y luego en cuantos segundos lo va a seguir realizando
        posicionZ = 0;
    }

    // Update is called once per frame
    void Update()
    {   
        ActualizarPosicion();
        
        
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space)) //definimos la tecla para avanzar
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

    //ejecutarse siempre que deba ejecutarse una raya
    private void OnDrawGizmos() { //dibujamos la raya para verla 
        
        Gizmos.color = Color.green;
        Gizmos.DrawLine(grafico.position + Vector3.up * 0.5f, grafico.position + Vector3.up * 0.5f + grafico.forward * distanciaVista);
    }

    //Metodo que me sirve para actualizar la posicion del personaje
    public void ActualizarPosicion()
    {
        //actualizacion de posicion si se lo come el depredador 
        if (!vivo)
        {
            return; //sino esta vivo no puede seguir haciendo nada
        }
        //posObjetivo = new Vector3 (lateral, 0, posicionZ); //para que no se pierda de carril
        //transform.position = posObjetivo;
        //transform.position = Vector3.Lerp(transform.position, posObjetivo, velocidad * Time.deltaTime); //Agregamos velocidad
    }
    //Realizar animaciones con codigo para que se vea mejor 
    public IEnumerator CambiarPosicion()
    {
        bloqueo = true;
        posObjetivo = new Vector3 (lateral, 0, posicionZ); //para que no se pierda de carril
        Vector3 posActual = transform.position;

        for (int i = 0; i < 10; i++)
        {
            transform.position = Vector3.Lerp(posActual, posObjetivo, i * 0.1f) + Vector3.up *curva.Evaluate(i * 0.1f); //me deja elegir la curva para que el movimiento se vea vertical 
            yield return new WaitForSeconds(1f/velocidad); //mientras mas alta la velocidad mas pequeño el numero de la pausa
        }
        //transform.position = Vector3.Lerp(transform.position, posObjetivo, velocidad * Time.deltaTime); //Agregamos velocidad
        bloqueo = false;
    }

    //Metodo que me sirve para avanzar el personaje
    public void Avanzar()
    {
        if (!vivo || bloqueo )
        {
            return; //sino esta vivo no puede seguir haciendo nada
        }
        grafico.eulerAngles = Vector3.zero; //para hacer las rotaciones en cero de todos los lados (mira hacia enfrente) //AQUI
        if (MirarAdelante()) //si es TRUE que no avance porque hay algo (obstaculo) 
        {
            print ("Obstaculo de frente");
            return;
        }

        posicionZ++;
        //animaciones.SetTrigger("saltar");//agrego animacion tambien de salto por paso
        if (posicionZ > carril) //inversa
        {
           carril = posicionZ; //movimiento 
           mundo.CrearPiso(); //Creara piso segun movimiento
           Time.timeScale = escalaInicial + tazaIncremento * carril; //va a ir en aumento la dificultad por carril 
        }
        //llamamos al metood que nos ayuda con la composicion de la animacion 
        StartCoroutine(CambiarPosicion());
    }

    //Metodo que me sirve para retroceder el personaje
    public void Retroceder()
    {
        if (!vivo|| bloqueo ) //aseguramos con el bloqueo que no habran cambios en la poscion cuando no ha terminado el cambio 
        {
            return; //sino esta vivo no puede seguir haciendo nada
        }
        grafico.eulerAngles = new Vector3(0, 180,0);//gira su cuerpo al retroceder (mira hacia atras) //AQUI
        if (MirarAdelante()) //si es TRUE que no avance porque hay algo (obstaculo) 
        {
            print ("Obstaculo Atras");
            return;
        }

        if (posicionZ > carril -3) //que solo pueda retrocede maximo 3 veces
        {
            posicionZ--; //ya no se va a ejecutar 
            //animaciones.SetTrigger("saltar");//agrego animacion tambien de salto por paso
        }
        //llamamos al metood que nos ayuda con la composicion de la animacion 
        StartCoroutine(CambiarPosicion());
    }
    
    //Metodo para que el personaje se mueva a los lados
    public void MoverLados(int cuanto)
    {
        if (!vivo || bloqueo )
        {
            return; //sino esta vivo no puede seguir haciendo nada
        }
        grafico.eulerAngles = new Vector3(0, 90 * cuanto, 0); //cambio del personaje al estar al tomar movimientos laterales //AQUI
        if (MirarAdelante()) //si es TRUE que no avance porque hay algo (obstaculo) 
        {
            print ("Obstaculo de lado");
            return;
        }

        lateral += cuanto; //mas uno o menos uno
        //animaciones.SetTrigger("saltar");//agrego animacion tambien de salto por paso
        lateral = Mathf.Clamp(lateral, -4, 4); //Poner un minimo y un maximo.
        //llamamos al metood que nos ayuda con la composicion de la animacion 
        StartCoroutine(CambiarPosicion());
    }

    public bool MirarAdelante()
    {

        RaycastHit hit;
        Ray rayo = new Ray(grafico.position + Vector3.up * 0.5f, grafico.forward); //la posicion del personaje para detectar si hay algo enfrente (grafico.forward = donde esta mirando)

        if (Physics.Raycast(rayo, out hit, distanciaVista , capaObstaculos)) //forma para lanzar un rayo
        {
            return true; //si choca con cualquier cosa que no este en la capa la va a ignorar
        }
        return false; 
    }

    //Metodo para choque con el depredador
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("depredador"))
        {
            animaciones.SetTrigger("morir");
            vivo = false;
        }
    }
    //Metodo para que se muera el personaje con el carril de agua (tiramos rayo)
    public void MirarAgua()
    {
        RaycastHit hit;
        Ray rayo = new Ray(transform.position + Vector3.up, Vector3.down);

        if (Physics.Raycast(rayo, out hit, 3, capaAgua)) //tiramos el rayo para abajo 
        {
            if (hit.collider.CompareTag("agua"))//si el tag es agua se muere
            {
                //agregamos las animaciones de muerte en el agua
                animaciones.SetTrigger("hundir");
                vivo = false;
            }
        }
    }
}
