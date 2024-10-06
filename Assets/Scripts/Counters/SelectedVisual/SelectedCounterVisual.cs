using KitchenChaos.Feature.Player;
using UnityEngine;

namespace KitchenChaos.Feature.Interaction
{
    public class SelectedCounterVisual : MonoBehaviour
    {
        [SerializeField] private BaseCounter _baseCounter;
        [SerializeField] private GameObject[] _selectedVisualList;

        private void Start()
        {
            PlayerController.Instance.OnSelectedCounterChanged += Player_OnSelectedCounterChanged;
        }

        private void Player_OnSelectedCounterChanged(object sender, PlayerController.OnSelectedCounterChangedEventSArgs obj)
        {
            if (obj.selectedCounter == _baseCounter)
            {
                ShowSelection();
            }
            else
            {
                HideSelection();
            }
        }

        private void ShowSelection()
        {
            foreach (var selectedVisualGameObject in _selectedVisualList)
            {
                selectedVisualGameObject.SetActive(true);
            }
            
        }

        private void HideSelection()
        {
            foreach (var selectedVisualGameObject in _selectedVisualList)
            {
                selectedVisualGameObject.SetActive(false);
            }
        }
    }
}