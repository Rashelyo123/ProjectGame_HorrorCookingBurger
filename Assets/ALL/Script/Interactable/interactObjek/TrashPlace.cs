using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashPlace : Interactable
{
    public override void BaseInteract()
    {
        PlayerHand playerHand = FindObjectOfType<PlayerHand>();


        if (playerHand != null && playerHand.ObjectInHand != null)
        {

            GameObject objectToTrash = playerHand.ObjectInHand;


            playerHand.ObjectInHand = null;


            Destroy(objectToTrash);


        }
        else
        {
            Debug.LogWarning("No object in hand to trash.");
        }
    }
}