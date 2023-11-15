using System;
using System.Drawing;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UIElements;
using UnityEngine.Video;
using Color = UnityEngine.Color;
public class Asteroid : MonoBehaviour
{
    public float size = 3f;
    public float sizeMin = 0.5f;
    public float sizeMax = 5.5f;
    public float speed = 7f;
    public float maxLife = 100f;
    private int score = 0;
    public bool isIndestructible = false;
    public bool vidaExtra = false;

    [SerializeField] public Sprite[] sprites;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = rb.GetComponent<SpriteRenderer>();
    }
    private void Start()
    {   //Asigna un sprite aleatorio de la lista de sprites del prefab
        spriteRenderer.sprite = sprites[UnityEngine.Random.Range(0, sprites.Length)];

        //Asigna una rotación aleatoria en el eje Z y un tamaño aleatorio
        this.transform.eulerAngles = new Vector3(0f, 0f, UnityEngine.Random.value * 360f);
        this.transform.localScale = Vector3.one * this.size;

        rb.mass = this.size;
        Destroy(this.gameObject, maxLife);//destruye el objeto despues de cierto tiempo.
        
        //Asigna una probabilidad del 30% para vida extra
        vidaExtra = UnityEngine.Random.Range(0f, 1f) < 0.3f;
        //asigna una probabilidad del 10% para un asteroide indestructible (rojo)
        isIndestructible = UnityEngine.Random.Range(0f, 1f) < 0.1f;

        if (this.isIndestructible)
        {//si es indestructible cambio el layer, le multiplico la masa y le cambio el color
            this.gameObject.layer = LayerMask.NameToLayer("Asteroide_Rojo");
            rb.mass *= 5f;
            spriteRenderer.color = Color.red;
        }
        
    }

    public void SetTrajectory(Vector2 direction)
    {
        if (spriteRenderer.color == Color.red)
        {
            rb.AddForce(this.speed * direction * 20f);
        }
        if (this.isIndestructible)
        {
            rb.AddForce(this.speed * direction * 20f);
        }
        else
        {
            rb.AddForce(direction * this.speed);
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 position = this.transform.position;
        if (collision.gameObject.CompareTag("Bullet"))//al chocar con la bala
        {
            if (!isIndestructible) // Verifica si el asteroide no es indestructible
            {
                //si vidaExtra es true y el tamaño menor a 1 instancia una vida extra.
                if (vidaExtra && this.size <= 1f)
                {
                    GameManager.Instance.VidaEx(position);
                }
                // Verifica si el asteroide puede dividirse en dos partes más pequeñas
                if ((this.size * 0.5f) >= sizeMin)
                {
                    CreateSplit();
                    CreateSplit();
                }

                //asigna puntaje en función del tamaño del asteroide
                if (this.size < 0.7f)
                {
                    score = 100;
                }
                else if (this.size < 1.4f)
                {
                    score = 50;
                }
                else{score = 25;}

                GameManager.Instance.AsteroidDeath(this);
                ScoreTracker.instance.AddScore(score);
                Destroy(this.gameObject);
            }
            else 
            {
                //Si el asteroide es indestructible, resta puntaje
                ScoreTracker.instance.AddScore(-30);
            }
        }
    }

    private Asteroid CreateSplit()
    {
        //obtiene la posicion actual del asteroide y la desplaza ligeramente para evitar que estén superpuestas
        Vector2 position = this.transform.position;
        position += UnityEngine.Random.insideUnitCircle * 0.5f;

        //Instancio un asteroide nuevo en "position" y le asigno el tamaño a la mitad del original
        Asteroid half = Instantiate(this, position, this.transform.rotation);
        half.size = this.size * 0.5f;

        //si el asteroide es indestructible, aumenta la velocidad de trayectoria.
        if (half.isIndestructible == true)
        {
            half.SetTrajectory(UnityEngine.Random.Range(20,25) * this.speed * UnityEngine.Random.insideUnitCircle.normalized);
        }
        half.SetTrajectory(UnityEngine.Random.Range(1, 7) * this.speed * UnityEngine.Random.insideUnitCircle.normalized);
        //retorna la mitad del asteroide
        return half;
    }
}
