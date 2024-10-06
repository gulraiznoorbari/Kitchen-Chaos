using UnityEngine;

namespace KitchenChaos.Data
{
    [CreateAssetMenu(menuName = "Data/CuttingRecipe", fileName = "CuttingRecipe", order = 1)]
    public class CuttingRecipeSO: ScriptableObject
    {
        public KitchenObjectSO unsliced;
        public KitchenObjectSO sliced;
        public int maxCuttingProgress;
    }
}