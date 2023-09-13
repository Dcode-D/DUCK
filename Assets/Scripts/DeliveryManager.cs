using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DeliveryManager : MonoBehaviour
{
    [SerializeField] private RecipeListSO recipeListSO;
    private List<RecipeSO> waitingRecipeList;
    [SerializeField]private float recipeSpwanTimer = 4f;
    [SerializeField] private int maximunRecipe = 5;

    public static DeliveryManager instance;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        waitingRecipeList = new List<RecipeSO>();
    }

    // Update is called once per frame
    void Update()
    {
        //Update recipe spawn timer
        recipeSpwanTimer -= Time.deltaTime;
        if(recipeSpwanTimer <=0 )
        {
            //time hit spawn rate
            recipeSpwanTimer = 4f;
            if(waitingRecipeList.Count < maximunRecipe)
            {
            RecipeSO chosenRecipe = recipeListSO.recipeList[Random.Range(0,recipeListSO.recipeList.Count)];
            waitingRecipeList.Add(chosenRecipe);
            Debug.Log(chosenRecipe.name);
            }
            
        }
    }

    public void Delivery(PlateKitchenObject plateKitchenObject)
    {
        bool containsAll = false;
        List<KitchenObjectSO> listIngredientsOfDish = plateKitchenObject.GetListIngredients();
        foreach(var recipe in waitingRecipeList)
        {
            //check among waiting recipe
            //if the player is deliering among the waiting recipe
            containsAll = recipe.listRecipe.All(ingre => listIngredientsOfDish.Any(ingre2=>ingre == ingre2))
                               && recipe.listRecipe.Count == listIngredientsOfDish.Count;
            if (containsAll)
                break;
        }
        if (containsAll)
        {
            //player delivered the corretc recipe
            Debug.Log("Player delivered a correct recipe");
        }
        else
        {
            //player not delivering the correct one
            Debug.Log("Wrong recipe delivered");
        }
    }
}
