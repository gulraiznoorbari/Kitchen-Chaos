using System;
using KitchenChaos.Data;
using KitchenChaos.Feature.Interaction;
using KitchenChaos.Feature.Player;
using KitchenChaos.UI.Interface;
using UnityEngine;

public class CuttingCounter : BaseCounter, IProgressBar
{
    [SerializeField] private CuttingRecipeSO[] _slicedKitchenObjectsList;
    private int _cuttingProgress;

    public event EventHandler<IProgressBar.OnProgressChangedEventArgs> OnProgressChanged;
    public event EventHandler OnCut;
    public static event EventHandler OnAnyCut;

    public new static void ResetStaticData()
    {
        OnAnyCut = null;
    }

    public override void Interact(PlayerController player)
    {
        if (!HasKitchenObject())
        {
            if (player.HasKitchenObject())
            {
                if (HasRecipeWithUnSlicedKitchenObject(player.GetKitchenObject().GetKitchenObjectSO()))
                {
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                    _cuttingProgress = 0;
                    var cuttingRecipeSO = GetUnSlicedCuttingRecipeSO(GetKitchenObject().GetKitchenObjectSO());
                    OnProgressChanged?.Invoke(this, new IProgressBar.OnProgressChangedEventArgs
                    {
                        progressNormalized = (float)_cuttingProgress / cuttingRecipeSO.maxCuttingProgress
                    });
                }
            }
        }
        else
        {
            if (!player.HasKitchenObject())
            {
                GetKitchenObject().SetKitchenObjectParent(player);
            }
            else
            {
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                {
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                    {
                        GetKitchenObject().DestroyKitchenObject();
                    }
                }
            }
        }
    }

    public override void CuttingInteractInput()
    {
        if (HasKitchenObject() && HasRecipeWithUnSlicedKitchenObject(GetKitchenObject().GetKitchenObjectSO()))
        {
            var slicedKitchenObjectSO = GetSlicedKitchenObject(GetKitchenObject().GetKitchenObjectSO());
            if (slicedKitchenObjectSO != null)
            {
                _cuttingProgress++;
                OnCut?.Invoke(this, EventArgs.Empty);
                OnAnyCut?.Invoke(this, EventArgs.Empty);
                var cuttingRecipeSO = GetUnSlicedCuttingRecipeSO(GetKitchenObject().GetKitchenObjectSO());
                OnProgressChanged?.Invoke(this, new IProgressBar.OnProgressChangedEventArgs
                {
                    progressNormalized = (float)_cuttingProgress / cuttingRecipeSO.maxCuttingProgress
                });
                if (_cuttingProgress >= cuttingRecipeSO.maxCuttingProgress)
                {
                    GetKitchenObject().DestroyKitchenObject();
                    KitchenObject.SpawnKitchenObject(slicedKitchenObjectSO, this);
                } 
            }
        }
    }

    private bool HasRecipeWithUnSlicedKitchenObject(KitchenObjectSO unslicedKitchenObjectSO)
    {
        var unSlicedCuttingRecipeSO = GetUnSlicedCuttingRecipeSO(unslicedKitchenObjectSO);
        return unSlicedCuttingRecipeSO != null;
    }
    
    private KitchenObjectSO GetSlicedKitchenObject(KitchenObjectSO unslicedKitchenObjectSO)
    {
        var cuttingRecipeSO = GetUnSlicedCuttingRecipeSO(unslicedKitchenObjectSO);
        if (cuttingRecipeSO != null)
        {
            return cuttingRecipeSO.sliced;
        }
        else
        {
            return null;
        }
    }

    private CuttingRecipeSO GetUnSlicedCuttingRecipeSO(KitchenObjectSO unslicedKitchenObjectSO)
    {
        foreach (var cuttingRecipeSO in _slicedKitchenObjectsList)
        {
            if (cuttingRecipeSO.unsliced == unslicedKitchenObjectSO)
            {
                return cuttingRecipeSO;
            }
        }

        return null;
    }
}