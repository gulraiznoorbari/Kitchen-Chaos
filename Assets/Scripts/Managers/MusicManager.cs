using UnityEngine;

namespace KitchenChaos.Manager.Audio
{
    public class MusicManager : MonoBehaviour
    {
        public static MusicManager Instance { get; private set; }
        
        private AudioSource _audioSource;
        private float _volume = 0.3f;
        private const string MUSIC_VOLUME = "MusicVolume";

        private void Awake()
        {
            Instance = this;
            _audioSource = GetComponent<AudioSource>();
            _volume = PlayerPrefs.GetFloat(MUSIC_VOLUME, 0.3f);
            _audioSource.volume = _volume;
        }
        
        public void ChangeVolume()
        {
            _volume += 0.1f;
            if (_volume > 1f)
            {
                _volume = 0f;
            }
            _audioSource.volume = _volume;
            PlayerPrefs.SetFloat(MUSIC_VOLUME, _volume);
            PlayerPrefs.Save();
        }

        public float GetVolume()
        {
            return _volume;
        }
    }
}
