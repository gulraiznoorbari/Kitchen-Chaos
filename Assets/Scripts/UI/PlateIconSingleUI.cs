using System.Collections;
using System.Collections.Generic;
using KitchenChaos.Data;
using UnityEngine;
using UnityEngine.UI;

namespace KitchenChaos.UI
{
    public class PlateIconSingleUI : MonoBehaviour
    {
        [SerializeField] private Image _image;

        public void SetKitchenObjectSO(KitchenObjectSO kitchenObjectSO)
        {
            _image.sprite = kitchenObjectSO.sprite;
        }
    }
}

