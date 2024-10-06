using System.Collections.Generic;
using UnityEngine;

namespace KitchenChaos.Data
{
    [CreateAssetMenu(menuName = "Data/Recipe", fileName = "Recipe", order = 1)]
    public class RecipeSO: ScriptableObject
    {
        public List<KitchenObjectSO> kitchenObjectSOList;
        public string recipeName;
    }
}