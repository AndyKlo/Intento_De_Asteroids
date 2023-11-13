using UnityEngine;

public class AsteroidSpawn : MonoBehaviour
{
    public Asteroid asteroidPrefab; //Llamo a mi preFab "Asteroid" para inicializarlo

    public float TrayectoryVariation = 15f;
    public int spawnRate = 3;
    public int spawnCount = 1;
    public float spawnDistance = 15f; //Inicializo la distancia de spawn de los asteroides, 15 espacios desde el centro    
    private void Start()
    {
        InvokeRepeating(nameof(Spawn), spawnCount, spawnRate); //invoco repetidamente el método Spawn()
    }
    private void Spawn()
    {
        for (int i = 0; i < spawnCount; i++)
        {
            Vector2 spawnDirection = Random.insideUnitCircle.normalized * spawnDistance;
            Vector3 spawnPoint = transform.position + (Vector3)spawnDirection;
            
            float variance = Random.Range(-TrayectoryVariation, TrayectoryVariation);
            Quaternion rotation = Quaternion.Euler(0, 0, variance);

            Asteroid asteroid = Instantiate(asteroidPrefab, spawnPoint, rotation);
            asteroid.size = Random.Range(asteroid.sizeMin, asteroid.sizeMax);
            Vector3 trajectory = rotation * -spawnDirection;

            if (asteroid.isIndestructible == true)
            {
                Debug.Log("El asteroide es indestructible.");
            }
            asteroid.SetTrajectory(trajectory);  
            
        }
    }
}
