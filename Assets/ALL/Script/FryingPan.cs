using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public enum CookingState
{
    Empty,
    Cooking,
    Done,
}
public class FryingPan : Interactable
{
    [Header("Frying Pan Settings")]
    public Transform cookingPoint;

    [Header("Current State")]
    public CookingState currentState = CookingState.Empty;

    private GameObject currentFoodObject;
    private CookAbleFood currentFoodData;
    private float cookingTimer = 0f;
    private bool isCooking = false;

    void Start()
    {
        if (cookingPoint == null)
        {
            cookingPoint = transform;
        }
    }
    void Update()
    {
        if (isCooking && currentState == CookingState.Cooking)
        {
            cookingTimer += Time.deltaTime;
            if (cookingTimer >= currentFoodData.cookTime)
            {
                FinishCooking();
            }
        }



    }
    public override void BaseInteract()
    {
        PlayerHand playerHand = FindObjectOfType<PlayerHand>();
        if (playerHand == null) return;

        if (playerHand.ObjectInHand != null && currentState == CookingState.Empty)
        {
            CookAbleFood food = playerHand.ObjectInHand.GetComponent<CookAbleFood>();
            if (food != null)
            {
                StartCooking(food, playerHand);

            }
            else
            {
                //// Bukan makanan yang bisa dimasak
            }
        }
        else if (playerHand.ObjectInHand == null && currentState == CookingState.Done)
        {
            TakeFood(playerHand);
        }
        else if (currentState == CookingState.Cooking)
        {
            float remainingTime = currentFoodData.cookTime - cookingTimer;
            Debug.Log("Masih memasak, sisa waktu: " + remainingTime.ToString("F2") + " detik.");
        }
    }

    void StartCooking(CookAbleFood food, PlayerHand playerHand)
    {
        currentFoodData = food;
        GameObject rawFood = playerHand.ObjectInHand;
        playerHand.ObjectInHand = null;
        Destroy(rawFood);


        Vector3 spawnPosition = cookingPoint.position + food.panOffset;
        currentFoodObject = Instantiate(food.cookingVersion, spawnPosition, cookingPoint.rotation, cookingPoint);

        currentState = CookingState.Cooking;
        isCooking = true;
        cookingTimer = 0f;
    }
    void TakeFood(PlayerHand playerHand)
    {
        currentFoodObject.transform.SetParent(null);
        playerHand.HoldObject(currentFoodObject);

        currentState = CookingState.Empty;
        currentFoodObject = null;
        currentFoodData = null;
        cookingTimer = 0f;
    }

    private void FinishCooking()
    {
        isCooking = false;

        Vector3 currentPosition = currentFoodObject.transform.position;
        Quaternion currentRotation = currentFoodObject.transform.rotation;
        Destroy(currentFoodObject);

        currentFoodObject = Instantiate(currentFoodData.cookedVersion, currentPosition, currentRotation, cookingPoint);
        currentState = CookingState.Done;
    }
    void OawGizmos()
    {
        if (cookingPoint != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(cookingPoint.position, 0.1f);
        }
    }
}
