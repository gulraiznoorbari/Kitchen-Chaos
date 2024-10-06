using UnityEngine;

namespace KitchenChaos.Feature.Interaction
{
    public interface IKitchenObjectParent
    {
        public Transform GetCounterTopTransform();

        public void SetKitchenObject(KitchenObject kitchenObject);

        public KitchenObject GetKitchenObject();

        public void ClearKitchenObject();

        public bool HasKitchenObject();
    }
}