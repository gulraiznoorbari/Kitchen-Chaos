using System;
using System.Collections.Generic;
using UnityEngine;

namespace KitchenChaos.Feature.Interaction
{
    public class PlatesCounterVisual : MonoBehaviour
    {
        [SerializeField] private PlatesCounter _platesCounter;
        [SerializeField] private Transform _counterTopPoint;
        [SerializeField] private GameObject _plateVisual;
        private List<GameObject> plateVisualGameObjectsList;

        private void Awake()
        {
            plateVisualGameObjectsList = new List<GameObject>();
        }

        private void Start()
        {
            _platesCounter.OnPlateSpawned += PlatesCounter_OnPlateSpawned;
            _platesCounter.OnPlateRemoved += PlatesCounter_OnPlateRemoved;
        }

        private void PlatesCounter_OnPlateSpawned(object sender, EventArgs e)
        {
            var plateVisual = Instantiate(_plateVisual, _counterTopPoint);
            var plateOffsetY = 0.1f;
            plateVisual.transform.localPosition = new Vector3(0, plateOffsetY * plateVisualGameObjectsList.Count, 0);
            plateVisualGameObjectsList.Add(plateVisual.gameObject);
        }
        
        private void PlatesCounter_OnPlateRemoved(object sender, EventArgs e)
        {
            var lastIndex = plateVisualGameObjectsList.Count - 1;
            var plate = plateVisualGameObjectsList[lastIndex];
            plateVisualGameObjectsList.Remove(plate);
            Destroy(plate);
        }
    }
}

