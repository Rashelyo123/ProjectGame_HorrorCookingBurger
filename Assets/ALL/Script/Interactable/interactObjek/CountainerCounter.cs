using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountainerCounter : Interactable
{
    public GameObject objectToSpawn;
    public override void BaseInteract()
    {
        PlayerHand playerHand = FindObjectOfType<PlayerHand>();
        if (playerHand.ObjectInHand == null && CanInteract)
        {
            GameObject spawnedObject = Instantiate(objectToSpawn);
            playerHand.HoldObject(spawnedObject);
            Debug.Log("Object Spawned and Held");
        }
    }
}
