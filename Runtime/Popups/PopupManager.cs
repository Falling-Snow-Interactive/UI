using System.Collections.Generic;
using Fsi.General;
using UnityEngine;

namespace Fsi.Ui.Popups
{
    public class PopupManager : MbSingleton<PopupManager>
    {
        // active
        private List<Popup> popups;
        private Popup activePopup;

        [Header("Properties")]

        [SerializeField]
        private Transform popupContainer;

        protected override void Awake()
        {
            base.Awake();
            popups = new List<Popup>();
        }
        
        // ReSharper disable Unity.PerformanceAnalysis
        public T OpenPopup<T>(T popup) where T : Popup
        {
            Debug.Log($"Popup | Opening popup: {popup.name}");
			
            if (activePopup)
            {
                activePopup.Unfocus();
            }

            T p = Instantiate(popup, popupContainer.transform);

            p.Open();
            p.Focus();
            activePopup = p;
            popups.Add(p);

            return p;
        }

        public void ClosePopup()
        {
            if (activePopup)
            {
                activePopup.Unfocus();
                activePopup.Close();
            }

            if (popups.Count > 0)
            {
                activePopup = popups[^1];
                popups.Remove(activePopup);
            }
        }
        
        public void ClosePopup(Popup popup)
        {
            if (popup == null) return;
			
            if (popups.Contains(popup))
            {
                popups.Remove(popup);
            }
			
            if (activePopup == popup)
            {
                activePopup = null;
                activePopup = popups.Count > 0 ? popups[^1] : null;
            }
			
            popup.Unfocus();
            activePopup?.Focus();
        }

        /// <summary>
        /// Closes and removes all active popups.
        /// </summary>
        public void CloseAll()
        {
            while (popups.Count > 0)
            {
                popups[^1].Close();
            }

            activePopup = null;
        }
    }
}
