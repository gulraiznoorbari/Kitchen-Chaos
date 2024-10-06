using KitchenChaos.UI.Interface;
using UnityEngine;
using UnityEngine.UI;

namespace KitchenChaos.UI
{
    public class ProgressBar : MonoBehaviour
    {
        [SerializeField] private GameObject _hasGameObjectProgressBar;
        [SerializeField] private Image _fillImage;
        private IProgressBar IProgressBar;
        
        private void Start()
        {
            IProgressBar = _hasGameObjectProgressBar.GetComponent<IProgressBar>();
            if (_hasGameObjectProgressBar == null)
            {
                return;
            }
            IProgressBar.OnProgressChanged += ProgressBar_OnProgressChanged;
            _fillImage.fillAmount = 0f;
            Hide();
        }

        private void ProgressBar_OnProgressChanged(object sender, IProgressBar.OnProgressChangedEventArgs e)
        {
            _fillImage.fillAmount = e.progressNormalized;
            if (e.progressNormalized == 0f || e.progressNormalized == 1f)
            {
                Hide();
            }
            else
            {
                Show();
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

