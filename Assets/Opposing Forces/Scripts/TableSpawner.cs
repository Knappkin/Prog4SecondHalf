using UnityEngine;

public class TableSpawner : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject table;

    public GameObject player;
    
    private PlayerController playerScript;
   [SerializeField] private float scoreRange;
   [SerializeField] private float spawnRange;
    void Start()
    {
     playerScript = player.GetComponent<PlayerController>();   

        MoveTable();

    }

    // Update is called once per frame
    void Update()
    {
        float distFromGoal = (player.transform.position - table.transform.position).magnitude;

        if (distFromGoal < scoreRange)
        {
            Debug.Log("SCORE");
            MoveTable();
        }
        
    }

    private void MoveTable()
    {
        Vector3 randomSpawnLocation;
        randomSpawnLocation.x = Random.Range(-spawnRange, spawnRange);
        randomSpawnLocation.z = Random.Range(-spawnRange, spawnRange);
        randomSpawnLocation.y = 0;

        table.transform.position = randomSpawnLocation;
    }
}
