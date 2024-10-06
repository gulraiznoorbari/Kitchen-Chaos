using System;
using KitchenChaos.Feature.Delivery;
using KitchenChaos.Feature.Player;

namespace KitchenChaos.Feature.Interaction
{
    public class DeliveryCounter : BaseCounter
    {
        public static DeliveryCounter Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
        }

        public override void Interact(PlayerController playerController)
        {
            if (playerController.HasKitchenObject())
            {
                if (playerController.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                {
                    // Only accept w/ plate
                    DeliveryManager.Instance.DeliveryRecipe(plateKitchenObject);
                    playerController.GetKitchenObject().DestroyKitchenObject();
                }
            }
        }
    }
}
