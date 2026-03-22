using UnityEngine;

public class BuildingScript : MonoBehaviour
{
    BuildStats instanceBuildStats;
    float iBuildTime;
    float iBuildHeight;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.localScale = new Vector3(instanceBuildStats.buildWidth, instanceBuildStats.buildLength, instanceBuildStats.buildHeight);
    }

}
