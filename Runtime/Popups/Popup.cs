using UnityEngine;
using UnityEngine.UI;

namespace Fsi.Ui.Popups
{
    public abstract class Popup : MonoBehaviour
    {
        [Header("Popup")]

        [SerializeField]
        private RectTransform root;
        public RectTransform Root => root;

        [SerializeField]
        private Button closeButton;
        public Button CloseButton => closeButton;

        protected virtual void Start()
        {
            closeButton?.onClick.AddListener(Close);
        }
	    
        public abstract void Open();
	    
        public virtual void Close()
        {
            PopupManager.Instance.ClosePopup(this);
            Destroy(gameObject);
        }
	    
        public abstract void Focus();

        public abstract void Unfocus();
    }
}