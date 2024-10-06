using UnityEngine;

namespace KitchenChaos.Data
{
    [CreateAssetMenu(menuName = "Data/BurningRecipe", fileName = "BurningRecipe", order = 1)]
    public class BurningRecipeSO: ScriptableObject
    {
        public KitchenObjectSO inputItem;
        public KitchenObjectSO burnedItem;
        public float burningTimer;
    }
}