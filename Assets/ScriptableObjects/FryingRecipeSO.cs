using UnityEngine;

namespace KitchenChaos.Data
{
    [CreateAssetMenu(menuName = "Data/FryingRecipe", fileName = "FryingRecipe", order = 1)]
    public class FryingRecipeSO: ScriptableObject
    {
        public KitchenObjectSO inputItem;
        public KitchenObjectSO outputItem;
        public float fryingTimerMax;
    }
}