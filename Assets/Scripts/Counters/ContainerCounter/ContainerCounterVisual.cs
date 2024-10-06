using System;
using UnityEngine;

namespace KitchenChaos.Feature.Interaction
{
    public class ContainerCounterVisual: MonoBehaviour
    {
        [SerializeField] private ContainerCounter _containerCounter;
        [SerializeField] private Animator _containerCounterVisualAnimator;

        private readonly int OPEN_CLOSE = Animator.StringToHash("OpenClose");

        private void Start()
        {
            _containerCounter.OnPlayerGrabbedObject += ContainerCounter_OnPlayerGrabbedObject;
        }

        private void ContainerCounter_OnPlayerGrabbedObject(object sender, EventArgs e)
        {
            _containerCounterVisualAnimator.SetTrigger(OPEN_CLOSE);
        }
    }
}