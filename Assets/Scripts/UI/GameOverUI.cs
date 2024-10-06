using System;
using KitchenChaos.Feature.Delivery;
using KitchenChaos.Manager.GameStates;
using TMPro;
using UnityEngine;

namespace KitchenChaos.UI
{
    public class GameOverUI: MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _recipesDeliveredCount;
        
        private void Start()
        {
            KitchenGameManager.Instance.OnGameStateChanged += Game_OnStateChanged;
            Hide();
        }

        private void Game_OnStateChanged(object sender, EventArgs e)
        {
            if (KitchenGameManager.Instance.IsGameOver())
            {
                Show();
                _recipesDeliveredCount.text = DeliveryManager.Instance.GetSuccessfulRecipesDeliveredCount().ToString();
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