using System;
using KitchenChaos.Manager.GameStates;
using TMPro;
using UnityEngine;

namespace KitchenChaos.UI
{
    public class GameStartCountdownUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _countdownTimerText;

        private void Start()
        {
            KitchenGameManager.Instance.OnGameStateChanged += Game_OnStateChanged;
            Hide();
        }

        private void Update()
        {
            _countdownTimerText.text = Mathf.Ceil(KitchenGameManager.Instance.GetCountdownTimerText()).ToString();
        }

        private void Game_OnStateChanged(object sender, EventArgs e)
        {
            if (KitchenGameManager.Instance.IsCountdownToStartActive())
            {
                Show();
            }
            else
            {
                Hide();
            }
        }

        private void Show()
        {
            gameObject.SetActive(true);
        }
        
        private void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}

