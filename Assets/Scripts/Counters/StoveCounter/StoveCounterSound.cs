using UnityEngine;

namespace KitchenChaos.Feature.Interaction.Sound
{
    public class StoveCounterSound : MonoBehaviour
    {
        [SerializeField] private StoveCounter _stoveCounter;
        private AudioSource _audioSource;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        private void Start()
        {
            _stoveCounter.OnFryingStateChanged += Stove_OnStateChanged;
        }

        private void Stove_OnStateChanged(object sender, StoveCounter.FryingStateChangedEventArg e)
        {
            var playSound = e.fryingState == StoveCounter.FryingState.Frying || e.fryingState == StoveCounter.FryingState.Fried;
            if (playSound)
            {
                _audioSource.Play();
            }
            else
            {
                _audioSource.Pause();
            }
        }
        
    }
}

