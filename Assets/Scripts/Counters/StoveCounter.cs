using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounter : BaseCounter, IProgress
{
    public event EventHandler<OnStateChangedEventArgs> OnStateChanged;
    public event EventHandler<IProgress.ProgressChangedEventArgs> OnProgressChanged;

    public class OnStateChangedEventArgs : EventArgs
    {
        public State state;
    }
    public enum State
    {
        Idle,
        Frying,
        Fried,
        Burned
    }

    [SerializeField] private FryingRecipeSO[] fryingRecipeSOArray;
    private State state;
    private float timer=0;
    private float buringTimer =0;
    private FryingRecipeSO fryingRecipeSO;

    private void Start()
    {
        this.state = State.Idle;
    }
    private void Update()
    {
        //Use a state machine 
        if (HasKitchenObject())
        {
            Debug.Log(state.ToString());
            switch (state)
            {
                case State.Idle:
                    break; 

                case State.Frying:
                    timer += Time.deltaTime;
                    if (fryingRecipeSO != null)
                    {
                        //if the recipe can be fried
                        OnProgressChanged?.Invoke(this, new IProgress.ProgressChangedEventArgs { 
                            progress = timer / fryingRecipeSO.fryingTimerMax 
                        });
                        if (fryingRecipeSO.fryingTimerMax <= timer)
                        {
                            //if the fried time is reached
                            kitchenObject.DestroySelf();
                            KitchenObject.SpawnKitchenObject(fryingRecipeSO.output.prefab, this);
                            buringTimer = 0;
                            state = State.Fried;
                            OnStateChanged?.Invoke(this, new OnStateChangedEventArgs { state = state });
                           
                        }
                    }
                    break;

                case State.Fried:
                    buringTimer += Time.deltaTime;
                    if (fryingRecipeSO != null)
                    {
                        //if the recipe can be fried
                        OnProgressChanged?.Invoke(this, new IProgress.ProgressChangedEventArgs { 
                            progress = buringTimer / fryingRecipeSO.burningTimerMax 
                        });
                        if (fryingRecipeSO.burningTimerMax <= buringTimer)
                        {
                            //if the fried time is reached
                            kitchenObject.DestroySelf();
                            KitchenObject.SpawnKitchenObject(fryingRecipeSO.burned.prefab, this);
                            timer = 0;
                            state = State.Burned;
                            OnStateChanged?.Invoke(this, new OnStateChangedEventArgs { state = state });
                           
                        }
                    }
                    break;

                case State.Burned:
                    break;
            }
            
        }
    }
    public override void Interact(Player player)
    {

        if (!HasKitchenObject())
        {
            //doesn't have any kitchen object yet
            if (player.HasKitchenObject())
            {
                //player has kitchen object
                //kitchen object can be fried
                if (HasRecipeKitchenObject(player.GetKitchenObject().GetKitchenObjectSO()))
                {
                    player.GetKitchenObject().SetParent(this);
                    fryingRecipeSO = GetResultRecipe(GetKitchenObject().GetKitchenObjectSO());
                    state = State.Frying;
                    timer = 0;
                    OnStateChanged?.Invoke(this, new OnStateChangedEventArgs { state = state });
                    OnProgressChanged?.Invoke(this, new IProgress.ProgressChangedEventArgs { 
                        progress = timer / fryingRecipeSO.fryingTimerMax 
                    });
                }

            }
            else
            {
                //player has nothing 
            }
        }
        else
        {
            //already has a kitchen object
            if (!player.HasKitchenObject())
            {
                //player doesn't have any kitchen object -> pick up something
                this.kitchenObject.SetParent(player);
                state = State.Idle; 
                timer = 0;
                OnStateChanged?.Invoke(this, new OnStateChangedEventArgs { state = state });
                OnProgressChanged?.Invoke(this, new IProgress.ProgressChangedEventArgs { progress = 0 });
            }
            else
            {
                //player already had a kitchen object
            }

        }
    }

    public override void InteractAlternate(Player player)
    {
        
    }

    private KitchenObjectSO GetResultKitchenObjectFromRecipe(KitchenObjectSO inputKitchenObjectSO)
    {
        var result = GetResultRecipe(inputKitchenObjectSO);
        return result ? result.output : null;
    }

    private bool HasRecipeKitchenObject(KitchenObjectSO inputKitchenObject)
    {
        var tmp = GetResultRecipe(inputKitchenObject);
        return tmp != null;
    }

    private FryingRecipeSO GetResultRecipe(KitchenObjectSO input)
    {
        foreach (FryingRecipeSO recipe in fryingRecipeSOArray)
        {
            if (recipe.input == input)
            {
                return recipe;
            }
        }
        return null;
    }
}
