using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Fsi.Ui.Dividers
{
    [CustomPropertyDrawer(typeof(DividerAttribute))]
    public class DividerAttributeDrawer : DecoratorDrawer
    {
        private DividerAttribute Attr => (DividerAttribute)attribute;

        public override VisualElement CreatePropertyGUI()
        {
            var container = new VisualElement();
            container.style.marginTop = 7.5f;
            container.style.marginRight = 5f;
            container.style.marginBottom = 7.5f;
            container.style.marginLeft = 5f;
            container.pickingMode = PickingMode.Ignore;

            var line = new VisualElement();
            line.style.height = Attr.Size;
            line.style.backgroundColor = Attr.Color;
            line.style.flexGrow = 1;
            line.pickingMode = PickingMode.Ignore;

            container.Add(line);
            return container;
        }

        public override float GetHeight()
        {
            return Attr.Size + 15f; // line + vertical margins
        }

        public override void OnGUI(Rect position)
        {
            float y = position.y + 7.5f;
            float height = Attr.Size;

            var lineRect = new Rect(
                                    position.x + 5f,
                                    y,
                                    position.width - 10f,
                                    height);

            EditorGUI.DrawRect(lineRect, Attr.Color);
        }
    }
}