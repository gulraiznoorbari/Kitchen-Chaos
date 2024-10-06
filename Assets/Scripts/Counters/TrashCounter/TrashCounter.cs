using System;
using KitchenChaos.Feature.Player;

namespace KitchenChaos.Feature.Interaction
{
    public class TrashCounter : BaseCounter
    {
        public static event EventHandler OnObjectTrashed;
        
        public new static void ResetStaticData()
        {
            OnObjectTrashed = null;
        }
        
        public override void Interact(PlayerController playerController)
        {
            if (playerController.HasKitchenObject())
            {
                playerController.GetKitchenObject().DestroyKitchenObject();
                OnObjectTrashed?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}

