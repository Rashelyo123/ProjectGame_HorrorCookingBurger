using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script untuk piring
public class Plate : Interactable
{
    [Header("Plate Settings")]
    public Transform foodPlacementPoint; // Transform untuk posisi makanan di piring
    public int maxFoodItems = 3; // Maksimal item yang bisa ditaruh di piring

    private List<GameObject> foodsOnPlate = new List<GameObject>(); // List makanan di piring

    void Start()
    {
        // Jika tidak ada placement point, gunakan posisi piring
        if (foodPlacementPoint == null)
        {
            foodPlacementPoint = transform;
        }
    }

    public override void BaseInteract()
    {
        PlayerHand playerHand = FindObjectOfType<PlayerHand>();

        // Cek apakah player memegang sesuatu
        if (playerHand != null && playerHand.ObjectInHand != null)
        {
            // Cek apakah objek yang dipegang adalah FoodItem
            FoodItem foodItem = playerHand.ObjectInHand.GetComponent<FoodItem>();

            if (foodItem != null)
            {
                // Cek apakah piring masih bisa menampung makanan
                if (foodsOnPlate.Count < maxFoodItems)
                {
                    PlaceFoodOnPlate(foodItem, playerHand);
                }
                else
                {
                    Debug.Log("Piring sudah penuh!");
                }
            }
            else
            {
                Debug.Log("Objek ini bukan makanan!");
            }
        }
        else
        {
            Debug.Log("Tidak ada item di tangan!");
        }
    }

    private void PlaceFoodOnPlate(FoodItem foodItem, PlayerHand playerHand)
    {
        // Simpan referensi objek yang akan di-destroy
        GameObject rawFood = playerHand.ObjectInHand;

        // Lepaskan dari tangan player
        playerHand.ObjectInHand = null;

        // Spawn versi makanan yang tertata di piring
        if (foodItem.platedVersion != null)
        {
            // Hitung posisi dengan offset
            Vector3 spawnPosition = foodPlacementPoint.position + foodItem.plateOffset;

            // Spawn makanan tertata
            GameObject platedFood = Instantiate(
                foodItem.platedVersion,
                spawnPosition,
                foodPlacementPoint.rotation,
                foodPlacementPoint // Set parent ke placement point
            );

            // Tambahkan ke list
            foodsOnPlate.Add(platedFood);

            Debug.Log($"{foodItem.foodName} berhasil ditaruh di piring! ({foodsOnPlate.Count}/{maxFoodItems})");
        }
        else
        {
            Debug.LogWarning($"{foodItem.foodName} tidak memiliki plated version!");
        }

        // Destroy bahan mentah dari tangan
        Destroy(rawFood);
    }

    // Method untuk mengambil piring (opsional)
    public void TakePlate()
    {
        // Bisa digunakan jika player ingin mengambil piring beserta isinya
        Debug.Log("Piring diambil dengan " + foodsOnPlate.Count + " item");
    }

    // Method untuk reset piring (opsional)
    public void ClearPlate()
    {
        foreach (GameObject food in foodsOnPlate)
        {
            Destroy(food);
        }
        foodsOnPlate.Clear();
        Debug.Log("Piring dibersihkan");
    }
}