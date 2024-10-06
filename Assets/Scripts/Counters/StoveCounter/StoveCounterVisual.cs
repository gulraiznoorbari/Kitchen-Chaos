using UnityEngine;

namespace KitchenChaos.Feature.Interaction
{
    public class StoveCounterVisual : MonoBehaviour
    {
        [SerializeField] private StoveCounter _stoveCounter;
        [SerializeField] private GameObject _stoveOnGameObject;
        [SerializeField] private GameObject _stoveOnParticles;

        private void Start()
        {
            _stoveCounter.OnFryingStateChanged += StoveCounter_FryingStateChanged;
        }

        private void StoveCounter_FryingStateChanged(object sender, StoveCounter.FryingStateChangedEventArg e)
        {
            var showVisual = e.fryingState == StoveCounter.FryingState.Frying || e.fryingState == StoveCounter.FryingState.Fried;
            _stoveOnGameObject.SetActive(showVisual);
            _stoveOnParticles.SetActive(showVisual);
        }
    }
}