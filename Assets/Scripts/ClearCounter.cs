using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ClearCounter : BaseCounter
{

    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            //no kitchen object in counter
            if (player.HasKitchenObject())
            {
                //player holding kitchen object
                player.GetKitchenObject().SetKitchenObjectParent(this);
            }
            else
            {
                //player isn't holding kitchen object
            }
        }else
        {
            //there's kitchen object in counter
            if(player.HasKitchenObject())
            {
                //player holding kitchen object
                
            }
            else
            {
                //player isn't holding kitchen object
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }

}

