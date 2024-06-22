using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManager : MonoBehaviour
{
    public static DeliveryManager Instance { get; private set; }

    public event EventHandler OnOrderRecipeSpawned;
    public event EventHandler OnOrderRecipeCompleted;
    public event EventHandler OnOrderRecipeSuccess;
    public event EventHandler OnOrderRecipeFailed;

    [SerializeField] private RecipeListSO recipeListSO;


    private List<RecipeSO> ordersRecipeSOList;

    private float spawnRecipeTimer;
    private float spawnRecipeTimerMax = 4f;
    private int maxOrderRecipe = 6;
    private int orderDelivered;


    private void Awake()
    {
        Instance = this;

        ordersRecipeSOList = new List<RecipeSO>();
    }

    private void Update()
    {
        spawnRecipeTimer -= Time.deltaTime;
        if(spawnRecipeTimer <= 0)
        {
            spawnRecipeTimer = spawnRecipeTimerMax;

            if(ordersRecipeSOList.Count < maxOrderRecipe)
            {
                RecipeSO orderRecipeSO = recipeListSO.recipeSOList[UnityEngine.Random.Range(0, recipeListSO.recipeSOList.Count)];
                ordersRecipeSOList.Add(orderRecipeSO);

                OnOrderRecipeSpawned?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public void DeliverRecipe(PlateKitchenObject plateKitchenObject)
    {
        for(int i = 0; i < ordersRecipeSOList.Count; i++)
        {
            RecipeSO orderRecipeSO = ordersRecipeSOList[i];

            if(orderRecipeSO.kitchenObjectSOList.Count == plateKitchenObject.GetKitchenObjectSOList().Count)
            {
                bool itemInPlateAndRecipeMatched = true;
                foreach(KitchenObjectSO recipeKitchenObjectSO in orderRecipeSO.kitchenObjectSOList)
                {
                    bool ingredientMatched = false;
                    foreach(KitchenObjectSO plateKitchenObjectSO in plateKitchenObject.GetKitchenObjectSOList())
                    {
                        if (recipeKitchenObjectSO == plateKitchenObjectSO)
                        {
                            ingredientMatched = true;
                            break;
                        }
                    }
                    if (!ingredientMatched)
                    {
                        itemInPlateAndRecipeMatched = false;
                    }
                }
                if (itemInPlateAndRecipeMatched)
                {
                    orderDelivered++;
                    ordersRecipeSOList.RemoveAt(i);
                    OnOrderRecipeCompleted?.Invoke(this, EventArgs.Empty);
                    OnOrderRecipeSuccess?.Invoke(this, EventArgs.Empty);
                    return;
                }
            }
        }
        OnOrderRecipeFailed?.Invoke(this, EventArgs.Empty);
        //Debug.Log("Player delivered incorrect orders");
    }

    public List<RecipeSO> GetOrderRecipeSOList()
    {
        return ordersRecipeSOList;
    }

    public int GetOrderDelivered()
    {
        return orderDelivered;
    }
}
