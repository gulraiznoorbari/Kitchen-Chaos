using KitchenChaos.Manager.GameStates;
using UnityEngine;

namespace KitchenChaos.UI.Sound
{
    public class GameTimerSound: MonoBehaviour
    {
        private AudioSource _audioSource;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        private void Start()
        {
            KitchenGameManager.Instance.OnGameStateChanged += Sound_ClockTicking;
        }

        private void Sound_ClockTicking(object sender, KitchenGameManager.GameStateChangedEventArg e)
        {
            var playSound = e.gameState == KitchenGameManager.GameStates.GamePlaying;
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