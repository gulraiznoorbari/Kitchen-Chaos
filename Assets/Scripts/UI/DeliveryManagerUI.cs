using System;
using KitchenChaos.Data;
using UnityEngine;

namespace KitchenChaos.Feature.Delivery
{
    public class DeliveryManagerUI : MonoBehaviour
    {
        [SerializeField] private Transform container;
        [SerializeField] private Transform recipeTemplate;

        private void Awake()
        {
            recipeTemplate.gameObject.SetActive(false);
        }

        private void Start()
        {
            DeliveryManager.Instance.OnRecipeSpawned += DeliveryManager_OnRecipeSpawned;
            DeliveryManager.Instance.OnRecipeCompleted += DeliveryManager_OnRecipeCompleted;
            UpdateVisual(); // so previous one$ dont show up ;)
        }

        private void DeliveryManager_OnRecipeCompleted(object sender, EventArgs e)
        {
            UpdateVisual();
        }

        private void DeliveryManager_OnRecipeSpawned(object sender, EventArgs e)
        {
            UpdateVisual();
        }

        private void UpdateVisual()
        {
            foreach (Transform child in container)
            {
                if (child == recipeTemplate)
                {
                    continue;
                }
                Destroy(child.gameObject);
            }

            foreach (RecipeSO recipeSO in DeliveryManager.Instance.GetWaitingRecipeSOList())
            {
                var recipeTransform = Instantiate(recipeTemplate, container);
                recipeTransform.gameObject.SetActive(true);
                recipeTransform.GetComponent<DeliveryManagerSingleUI>().SetRecipeSO(recipeSO);
            }
        }
    }
}

