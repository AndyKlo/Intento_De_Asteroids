using System;
using System.Drawing;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using Color = UnityEngine.Color;
public class Asteroid : MonoBehaviour
{
    public float size = 3f;
    public float sizeMin = 0.5f;
    public float sizeMax = 4f;
    public float speed = 7f;
    public float maxLife = 100f;
    private int score = 0;
    public bool isIndestructible = false;
    //public bool isKamikaze = false;

    [SerializeField] public Sprite[] sprites;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = rb.GetComponent<SpriteRenderer>();
    }
    private void Start()
    {
        spriteRenderer.sprite = sprites[UnityEngine.Random.Range(0, sprites.Length)];

        this.transform.eulerAngles = new Vector3(0f, 0f, UnityEngine.Random.value * 360f);
        this.transform.localScale = Vector3.one * this.size;

        rb.mass = this.size;
        Destroy(this.gameObject, maxLife);

        isIndestructible = UnityEngine.Random.Range(0f, 1f) < 0.2f;
        if (this.isIndestructible)
        {
            //this.speed = 10f;
            this.gameObject.layer = LayerMask.NameToLayer("Asteroide_Rojo");
            rb.mass *= 3f;
            spriteRenderer.color = UnityEngine.Color.red;

        }/*else if(!isKamikaze)
        {

            isKamikaze = UnityEngine.Random.Range(0f, 1f) < 0.2f;
            this.size = 0.5f;
            spriteRenderer.color = UnityEngine.Color.blue;
        }*/
        
    }

    public void SetTrajectory(Vector2 direction)
    {
        if (spriteRenderer.color == Color.red)
        {
            rb.AddForce(this.speed * direction * 20f);
            Debug.Log("es rojoooo");
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
        if (collision.gameObject.CompareTag("Bullet"))
        {

            if (!isIndestructible) // Verifica si el asteroide no es indestructible
            {
                if ((this.size * 0.5f) >= sizeMin)
                {
                    CreateSplit();
                    CreateSplit();
                }
                if (this.size < 0.7f)
                {
                    score = 100;
                }
                else if (this.size < 1.4f)
                {
                    score = 50;
                }
                else
                {
                    score = 25;
                }

                FindObjectOfType<GameManager>().AsteroidDeath(this);
                ScoreTracker.instance.AddScore(score);
                Destroy(this.gameObject);
            }
            else
            {
                ScoreTracker.instance.AddScore(-30);
            }
        }
    }

    private Asteroid CreateSplit()
    {
        Vector2 position = this.transform.position;
        position += UnityEngine.Random.insideUnitCircle * 0.5f;

        Asteroid half = Instantiate(this, position, this.transform.rotation);
        half.size = this.size * 0.5f;
        if (half.isIndestructible == true)
        {
            half.SetTrajectory(UnityEngine.Random.Range(40, 50) * this.speed * UnityEngine.Random.insideUnitCircle.normalized);
        }
        half.SetTrajectory(UnityEngine.Random.Range(1, 7) * this.speed * UnityEngine.Random.insideUnitCircle.normalized);

        return half;
    }

}
