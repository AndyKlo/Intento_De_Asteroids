using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Player : MonoBehaviour
{

    public Bullet bulletPrefab;  //Llamo a la clase Bullet para inicializarla.
    public float velocidad = 1f;
    //public float velocidadRetroceso = 0.1f;
    public float velocidadRotacion = 0.1f;

    private Rigidbody2D rb;
    private bool puntero; //defino la variable puntero como bool, para agregarle posterior agregarle una condición.
    private float direccion; //Esta es la rotación de la nave, se le agrega valor dependiendo del botón presionado.
    private int lives = 1;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>(); 
    }
    private void Update()
    {
        puntero = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow); //Si se presiona "W" o "Flecha hacia arriba" se le asigna un valor "True" a puntero
        //Solo le doy valor positivo y negativo, aún no se ejerce una fuerza sobre el objeto.
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)){
            direccion = 1f; //si presiono "A" o "Flecha izquierda" le asigno una dirección positiva
        } else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)){ 
            direccion = -1f; //si presiono "D" o "Flecha derecha" le asigno una dirección negativa
        }else{
            direccion = 0f; //Si no presiono valor, se mantiene el valor en 0.
        }
        if(Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) 
        {
            Shoot(); //si se presiona la tecla "espacio" se ejecuta el método shoot.
        }
    }

    private void FixedUpdate()
    {
        if (puntero) //si la variable puntero está siendo presionada
        {
            rb.AddForce(transform.up * velocidad); //se le agrega una fuerza al RiggidBody en el espacio local"hacia arriba" multiplicado por la velocidad
        }

        if (direccion != 0f) //si la dirección(dirección de rotación) es desigual a 0
        {
            rb.AddTorque(direccion * velocidadRotacion); //se le aprega un torque(rotación) y se multiplica por la velocidad de rotación
        }
    }

    private void Shoot()
    {
        Bullet bullet = Instantiate(bulletPrefab, transform.position,transform.rotation); //instancio la clase Bullet
        bullet.Proyectil(transform.up); //recibo el método Proyectil() de la clase Bullet
    }
    private void OnCollisionEnter2D(Collision2D collision) //al colisionar
    {
        if (collision.gameObject.CompareTag("Asteroid"))// al colisionar con un asteroide
        {
            rb.velocity = Vector3.zero; //dejo velocidad en 0
            rb.angularVelocity = 0f; 
            gameObject.SetActive(false); //desactivo el objeto, cancelo todo lo que tenga que ver con el

            ScoreTracker.instance.Lives(lives);

            FindObjectOfType<GameManager>().PlayerDeath();
        }
        
    }
}
