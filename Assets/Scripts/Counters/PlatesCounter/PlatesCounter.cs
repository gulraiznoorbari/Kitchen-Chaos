using System;
using KitchenChaos.Data;
using KitchenChaos.Feature.Player;
using UnityEngine;

namespace KitchenChaos.Feature.Interaction
{
    public class PlatesCounter : BaseCounter
    {
        [SerializeField] private KitchenObjectSO _plateObject;
        private float _spawnPlateTimer;
        private float _spawnPlateTimerMax = 4.0f;
        private int _plateSpawnedCounter;
        private int _plateSpawnedCounterMax = 4;

        public event EventHandler OnPlateSpawned;
        public event EventHandler OnPlateRemoved;

        private void Update()
        {
            _spawnPlateTimer += Time.deltaTime;
            if (_spawnPlateTimer > _spawnPlateTimerMax)
            {
                _spawnPlateTimer = 0f;
                if (_plateSpawnedCounter < _plateSpawnedCounterMax)
                {
                    _plateSpawnedCounter++;
                    OnPlateSpawned?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        public override void Interact(PlayerController playerController)
        {
            if (!playerController.HasKitchenObject())
            {
                if (_plateSpawnedCounter > 0)
                {
                    _plateSpawnedCounter--;
                    KitchenObject.SpawnKitchenObject(_plateObject, playerController);
                    OnPlateRemoved?.Invoke(this, EventArgs.Empty);
                }
            }
        }
    }
}

