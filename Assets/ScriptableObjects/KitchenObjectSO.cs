using UnityEngine;

namespace KitchenChaos.Data
{
    [CreateAssetMenu(menuName = "Data/KitchenObject", fileName = "KitchenObject", order = 1)]
    public class KitchenObjectSO: ScriptableObject
    {
        public GameObject prefab;
        public Sprite sprite;
        public string objectName;
    }
}