using Fsi.Ui.Dividers;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace Fsi.Ui.Progress
{
    [CustomEditor(typeof(ProgressWidget))]
    public class ProgressWidgetEditor : Editor
    {
        private const string InspectorTogglePref = "Progress.Inspector.Toggle";
        
        public override VisualElement CreateInspectorGUI()
        {
            VisualElement root = new();
            
            root.Add(new Divider());

            Foldout inspectorFoldout = new(){text = "Inspector", value = EditorPrefs.GetBool(InspectorTogglePref, false)};
            root.Add(inspectorFoldout);

            InspectorElement.FillDefaultInspector(inspectorFoldout, serializedObject, this);

            inspectorFoldout.RegisterValueChangedCallback(evt =>
                                                          {
                                                              EditorPrefs.SetBool(InspectorTogglePref, evt.newValue);
                                                          });

            return root;
        }

        private void OnInspectorFoldoutToggle()
        {
            
        }
    }
}