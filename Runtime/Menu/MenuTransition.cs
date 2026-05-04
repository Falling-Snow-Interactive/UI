using UnityEngine;

namespace Fsi.Ui.Menu
{
    public class MenuTransition<T> : MonoBehaviour
    {
        [SerializeField]
        private FsiMenu<T> menu;
        private FsiMenu<T> Menu => menu ??= GetComponentInParent<FsiMenu<T>>();

        [SerializeField]
        private T target;

        private void Awake()
        {
            if (!menu)
            {
                menu = GetComponentInParent<FsiMenu<T>>();
            }
        }

        public void Transition()
        {
            Menu.GoToPage(target);
        }
    }
}