
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Fsi.Ui
{
    public static class Create
    {
        [MenuItem("GameObject/UI (Canvas)/Background", false, 10)]
        public static void CreateBackground(MenuCommand menuCommand)
        {
            GameObject obj = new("Background");
            obj.transform.SetParent(Selection.activeTransform);
            
            RectTransform rectTransform = obj.AddComponent<RectTransform>();
            CanvasRenderer canvasRenderer = obj.AddComponent<CanvasRenderer>();
            Image image = obj.AddComponent<Image>();
            LayoutElement layoutElement = obj.AddComponent<LayoutElement>();
            
            layoutElement.ignoreLayout = true;

            rectTransform.anchorMin = Vector2.zero;
            rectTransform.anchorMax = Vector2.one;
            rectTransform.offsetMin = Vector2.zero;
            rectTransform.offsetMax = Vector2.zero;

            rectTransform.localScale = Vector2.one;
            
            Selection.activeGameObject = obj;
        }
    }
}
