using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookAbleFood : MonoBehaviour
{
    [Header("Food Info")]
    public string foodName;

    [Header("Cooking Stage")]
    public GameObject rawVersion;
    public GameObject cookingVersion;
    public GameObject cookedVersion;

    [Header("Coocking Settings")]
    public float cookTime = 5f;
    [Header("Visual Setting")]
    public Vector3 panOffset = new Vector3(0, 0.1f, 0);
}
