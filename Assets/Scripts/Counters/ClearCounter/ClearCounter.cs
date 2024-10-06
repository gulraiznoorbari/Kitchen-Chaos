using KitchenChaos.Feature.Player;

namespace KitchenChaos.Feature.Interaction
{
    public class ClearCounter : BaseCounter
    {
        public override void Interact(PlayerController player)
        {
            if (!HasKitchenObject())
            {
                // no kitchen   here
                if (player.HasKitchenObject())
                {
                    // player is carrying something
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                }
            }
            else
            {
                // Kitchen object here
                if (!player.HasKitchenObject())
                {
                    // player is not carrying anything
                    GetKitchenObject().SetKitchenObjectParent(player);
                }
                else
                {
                    // player is carrying something
                    if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject)) {
                        // player carrying plate
                        if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                        {
                            // add ingredient to the plate
                            GetKitchenObject().DestroyKitchenObject();
                        }
                    }
                    else
                    {
                        // player not carrying plate but something else 
                        if (GetKitchenObject().TryGetPlate(out PlateKitchenObject playerPlateKitchenObject))
                        {
                            // check for plate
                            if (playerPlateKitchenObject.TryAddIngredient(player.GetKitchenObject().GetKitchenObjectSO()))
                            {
                                // place the ingredient on the plate
                                player.GetKitchenObject().DestroyKitchenObject();
                            }
                        }
                    }
                }
            }
        }
    }
}