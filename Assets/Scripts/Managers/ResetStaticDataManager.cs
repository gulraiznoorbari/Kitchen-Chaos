using KitchenChaos.Feature.Interaction;
using UnityEngine;

namespace KitchenChaos.Manager.StaticData
{
    public class ResetStaticDataManager: MonoBehaviour
    {
        private void Awake()
        {
            CuttingCounter.ResetStaticData();
            TrashCounter.ResetStaticData();
            BaseCounter.ResetStaticData();
        }
    }
}