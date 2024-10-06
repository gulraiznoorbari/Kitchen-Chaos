using UnityEngine;

namespace KitchenChaos.Data
{
    [CreateAssetMenu(menuName = "Data/AudioClipRefs")]
    public class AudioClipSO: ScriptableObject
    {
        public AudioClip[] chop;
        public AudioClip[] deliveryFail;
        public AudioClip[] deliverySuccess;
        public AudioClip[] footstep;
        public AudioClip[] objectDrop;
        public AudioClip[] objectPickup;
        public AudioClip[] trash;
        public AudioClip[] warning;
        public AudioClip stoveSizzle;
    }
}