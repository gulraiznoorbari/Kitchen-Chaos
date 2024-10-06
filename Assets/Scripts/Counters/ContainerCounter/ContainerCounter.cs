using System;
using KitchenChaos.Data;
using KitchenChaos.Feature.Player;
using UnityEngine;

namespace KitchenChaos.Feature.Interaction
{
    public class ContainerCounter : BaseCounter
    {
        [SerializeField] private KitchenObjectSO _kitchenObjectData;

        public event EventHandler OnPlayerGrabbedObject;

        public override void Interact(PlayerController player)
        {
            if (!player.HasKitchenObject())
            {
                KitchenObject.SpawnKitchenObject(_kitchenObjectData, player);
                OnPlayerGrabbedObject.Invoke(this, EventArgs.Empty);
            }
        }
    }
}