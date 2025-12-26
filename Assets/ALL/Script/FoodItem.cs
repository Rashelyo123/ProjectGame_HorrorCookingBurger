using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script untuk bahan makanan individual
public class FoodItem : MonoBehaviour
{
    [Header("Food Settings")]
    public string foodName;
    public GameObject platedVersion;
    public Vector3 plateOffset = Vector3.zero;
}