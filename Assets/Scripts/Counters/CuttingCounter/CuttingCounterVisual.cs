using System;
using UnityEngine;

namespace KitchenChaos.Feature.Interaction
{
    public class CuttingCounterVisual: MonoBehaviour
    {
        [SerializeField] private CuttingCounter _cuttingCounter;
        [SerializeField] private Animator _containerCounterVisualAnimator;

        private readonly int CUT = Animator.StringToHash("Cut");

        private void Start()
        {
            _cuttingCounter.OnCut += CuttingCounter_OnCut;
        }

        private void CuttingCounter_OnCut(object sender, EventArgs e)
        {
            _containerCounterVisualAnimator.SetTrigger(CUT);
        }
    }
}