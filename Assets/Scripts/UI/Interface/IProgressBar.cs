using System;

namespace KitchenChaos.UI.Interface
{
    public interface IProgressBar
    {
        public event EventHandler<OnProgressChangedEventArgs> OnProgressChanged;
        public class OnProgressChangedEventArgs : EventArgs
        {
            public float progressNormalized;
        }
    }
}