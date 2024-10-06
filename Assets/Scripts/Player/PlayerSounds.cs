using KitchenChaos.Manager.Audio;
using UnityEngine;

namespace KitchenChaos.Feature.Player.Sound
{
    public class PlayerSounds : MonoBehaviour
    {
        private PlayerController _player;
        private float _footstepTimer;
        private float _footstepTimerMax = 0.1f;

        private void Awake()
        {
            _player = GetComponent<PlayerController>();
        }

        private void Update()
        {
            _footstepTimer -= Time.deltaTime;
            if (_footstepTimer < 0f)
            {
                _footstepTimer = _footstepTimerMax;
                if (_player.IsWalking())
                {
                    var volume = 1.0f;
                    SoundManager.Instance.PlayFootstepSound(_player.transform.position, volume);
                }
            }
        }
    }
}

