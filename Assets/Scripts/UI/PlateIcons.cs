using System;
using KitchenChaos.Feature.Interaction;
using UnityEngine;

namespace KitchenChaos.UI
{
    public class PlateIcons : MonoBehaviour
    {
        [SerializeField] private PlateKitchenObject _plateKitchenObject;
        [SerializeField] private Transform _iconTemplate;

        private void Awake()
        {
            _iconTemplate.gameObject.SetActive(false);
        }

        private void Start()
        {
            _plateKitchenObject.OnIngredientAdded += PlateKitchenObject_OnIngredientAdded;
        }

        private void PlateKitchenObject_OnIngredientAdded(object sender, PlateKitchenObject.OnIngredientAddedEventArg e)
        {
            UpdateIcons();
        }

        private void UpdateIcons()
        {
            // to remove previous icons:
            foreach (Transform child in transform) 
            {
                if (child == _iconTemplate) continue;
                Destroy(child.gameObject);
            }
            
            foreach (var kitchenObjectSO in _plateKitchenObject.GetKitchenObjectSOList())
            {
                var iconTransform = Instantiate(_iconTemplate, transform);
                iconTransform.gameObject.SetActive(true);
                iconTransform.GetComponent<PlateIconSingleUI>().SetKitchenObjectSO(kitchenObjectSO);
            }
        }
    }
}
