using UnityEngine;

namespace KitchenChaos.Feature.Player
{
    public class PlayerAnimator : MonoBehaviour
    {
        [SerializeField] private PlayerController _player;
    
        private Animator _animator;
    
        private const string IS_WALKING = "IsWalking";

        private void Awake()
        {
            _animator = gameObject.GetComponent<Animator>();
        }

        private void Update()
        {
            _animator.SetBool(IS_WALKING, _player.IsWalking());
        }
    }
}

