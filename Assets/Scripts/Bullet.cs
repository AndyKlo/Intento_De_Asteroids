using System.Drawing;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float velocidad = 500f;  //velocidad de proyectil
    //public float maxLifeTime = 5.0f;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Proyectil(Vector2 direccion)  //creo un método proyctil que recibe un Vector2 de parámetro. Se lo paso a Player.
    {
        rb.AddForce(direccion * velocidad);
        //Destroy(gameObject, maxLifeTime);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Asteroid" || collision.gameObject.tag == "Perimetro")
        {
            Destroy(gameObject); //si colisiona con un asteroide o el perímetro, la bala se destruye.
        }
    }

}
