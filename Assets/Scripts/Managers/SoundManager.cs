using System;
using KitchenChaos.Data;
using KitchenChaos.Feature.Delivery;
using KitchenChaos.Feature.Interaction;
using KitchenChaos.Feature.Player;
using UnityEngine;
using Random = UnityEngine.Random;

namespace KitchenChaos.Manager.Audio
{
    public class SoundManager : MonoBehaviour
    {
        public static SoundManager Instance { get; private set; }
        
        [SerializeField] private AudioClipSO _audioClipSO;
        private float _volume = 0.1f;
        private const string SOUND_EFFECTS_VOLUME = "SoundEffectsVolume";

        private void Awake()
        {
            Instance = this;
            _volume = PlayerPrefs.GetFloat(SOUND_EFFECTS_VOLUME, 1f);
        }

        private void Start()
        {
            DeliveryManager.Instance.OnRecipeSuccess += Sound_OnRecipeSuccess;
            DeliveryManager.Instance.OnRecipeFail += Sound_OnRecipeFail;
            PlayerController.Instance.OnPickedSomething += Sound_PickedSomething;
            CuttingCounter.OnAnyCut += Sound_CuttingCounter;
            BaseCounter.OnAnyObjectPlacedHere += Sound_AnyObjectPlacedHere;
            TrashCounter.OnObjectTrashed += Sound_ObjectInTrash;
        }

        private void Sound_ObjectInTrash(object sender, EventArgs e)
        {
            var trashCounter = sender as TrashCounter;
            PlayRandomSound(_audioClipSO.trash, trashCounter.transform.position);
        }

        private void Sound_AnyObjectPlacedHere(object sender, EventArgs e)
        {
            var baseCounter = sender as BaseCounter;
            PlayRandomSound(_audioClipSO.objectDrop, baseCounter.transform.position);
        }

        private void Sound_PickedSomething(object sender, EventArgs e)
        {
            PlayRandomSound(_audioClipSO.objectPickup, PlayerController.Instance.transform.position);
        }

        private void Sound_CuttingCounter(object sender, EventArgs e)
        {
            var cuttingCounter = sender as CuttingCounter;
            PlayRandomSound(_audioClipSO.chop, cuttingCounter.transform.position);
        }

        private void Sound_OnRecipeSuccess(object sender, EventArgs e)
        {
            var deliveryCounter = DeliveryCounter.Instance;
            PlayRandomSound(_audioClipSO.deliverySuccess, deliveryCounter.transform.position);
        }
        
        private void Sound_OnRecipeFail(object sender, EventArgs e)
        {
            var deliveryCounter = DeliveryCounter.Instance;
            PlayRandomSound(_audioClipSO.deliveryFail, deliveryCounter.transform.position);
        }

        private void PlayRandomSound(AudioClip[] audioClips, Vector3 position, float volume = 1.0f)
        {
            PlaySound(audioClips[Random.Range(0, audioClips.Length)], position, volume);
        }

        private void PlaySound(AudioClip audioClip, Vector3 position, float volumeMultiplier = 1.0f)
        {
            AudioSource.PlayClipAtPoint(audioClip, position, volumeMultiplier * _volume);
        }

        public void PlayFootstepSound(Vector3 position, float volume)
        {
            PlayRandomSound(_audioClipSO.footstep, position, volume);
        }

        public void ChangeVolume()
        {
            _volume += 0.1f;
            if (_volume > 1f)
            {
                _volume = 0f;
            }
            PlayerPrefs.SetFloat(SOUND_EFFECTS_VOLUME, _volume);
            PlayerPrefs.Save();
        }

        public float GetVolume()
        {
            return _volume;
        }
    }
}

