using System;
using UnityEngine;

[CreateAssetMenu(fileName = "BuildStats", menuName = "Scriptable Objects/BuildStats")]
public class BuildStats : ScriptableObject
{

    public float buildWidth;
    public float buildLength;
    public float buildHeight;
    public float buildTime;
    public bool walkable;

}
