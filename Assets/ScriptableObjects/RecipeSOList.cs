using System.Collections.Generic;
using UnityEngine;

namespace KitchenChaos.Data
{
    // [CreateAssetMenu(menuName = "Data/Recipe SO List", fileName = "Recipe SO List", order = 1)]
    public class RecipeSOList: ScriptableObject
    {
        public List<RecipeSO> recipeSOList;
    }
}