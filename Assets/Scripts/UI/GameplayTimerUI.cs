using KitchenChaos.Manager.GameStates;
using UnityEngine;
using UnityEngine.UI;

namespace KitchenChaos.UI
{
    public class GameplayTimerUI : MonoBehaviour
    {
        [SerializeField] private Image _timerImage;
        
        private void Update()
        {
            _timerImage.fillAmount = KitchenGameManager.Instance.GetGameplayTimerNormalized();
        }
    }
}

