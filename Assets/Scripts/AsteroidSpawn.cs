using UnityEngine;

public class AsteroidSpawn : MonoBehaviour
{
    public Asteroid asteroidPrefab; //Llamo a mi preFab "Asteroid" para inicializarlo
    public static AsteroidSpawn Instance { get; private set; }

    private float TrayectoryVariation = 15f; 
    private float spawnDistance = 15f; //Inicializo la distancia de spawn de los asteroides, 15 espacios desde el centro    
    public float repeSpawn = 1f;
    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        InvokeRepeating(nameof(Spawn), 1f, 1.2f);//invoco repetidamente el método Spawn()
    }// invoco el método spawn durante cierto tiempo, y repetido cada ciertos segundos
    private void Spawn()
    {
        for (int i = 0; i < repeSpawn; i++)
        {
            //Genera una dirección de spawn aleatoria dentro de un círculo unitario y la normaliza
            //luego multiplica por 10 para obtener una dirección dentro de un círculo de radio 10 unidades.
            Vector2 spawnDirection = Random.insideUnitCircle.normalized * spawnDistance;

            //lo mueve a la dirección aleatoria de 'spawnDirection'
            Vector3 spawnPoint = transform.position + (Vector3)spawnDirection;

            //Genera una variación aleatoria en el ángulo de rotación dentro del rango
            float variance = Random.Range(-TrayectoryVariation, TrayectoryVariation);
            //Crea una rotación en el plano Z usando el ángulo de variación calculado.
            Quaternion rotation = Quaternion.Euler(0, 0, variance);

            Asteroid asteroid = Instantiate(asteroidPrefab, spawnPoint, rotation);
            asteroid.size = Random.Range(asteroid.sizeMin, asteroid.sizeMax);

            //determina la trayectoria y le da dirección de movimiento del asteroidecrea un vector
            //de trayectoria, asignado por la rotación y la dirección inversa al vector de spawn
            Vector3 trajectory = rotation * -spawnDirection;
            //envía el prefab para darle trayectoria, se aplica fuerza al rb multiplicado por la velocidad
            asteroid.SetTrajectory(trajectory);  
        }
    }
}
