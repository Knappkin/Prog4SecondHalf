using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class BuilderScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public BuildStats selectedBuildStats;

    public List<BuildStats> buildStatsList;
    public BuildStats houseStats; //0
    public BuildStats hospStats; //1
    public BuildStats storeStats; //2
    public BuildStats parkStats;  //3
    public BuildStats townHallStats; //4
    void Start()
    {
        buildStatsList = new List<BuildStats>();
        buildStatsList.Add(houseStats);
        buildStatsList.Add(hospStats);
        buildStatsList.Add(storeStats);
        buildStatsList.Add(parkStats);
        buildStatsList.Add(townHallStats);
    }

    public void ChangeSelectedBuilding(int selection)
    {
        selectedBuildStats = buildStatsList[selection];
    }

    public GameObject InstantiateBuilding(GameObject buildingPrefab, Transform buildLocation)
    {
        GameObject buildInstance = Instantiate(buildingPrefab);
        buildInstance.transform.localScale = new Vector3(selectedBuildStats.buildWidth, selectedBuildStats.buildHeight, selectedBuildStats.buildLength);
        buildInstance.transform.position = buildLocation.position;
        return buildInstance;
    }
}
