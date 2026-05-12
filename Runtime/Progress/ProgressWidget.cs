using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Fsi.Ui.Progress
{
    public class ProgressWidget : MonoBehaviour, ISerializationCallbackReceiver
    {
        [SerializeField]
        private float min = 0;
        public float Min
        {
            get => min;
            set => min = value;
        }

        [SerializeField]
        private float max = 1;
        public float Max
        {
            get => max;
            set => max = value;
        }
        
        [SerializeField]
        private float value = 0.7f;
        public float Value
        {
            get => value;
            set
            {
                this.value = value;
                labelTmp?.SetText($"{this.value}/{Max}");
                if (fillImg)
                {
                    if (fillImg.transform is RectTransform fillRect)
                    {
                        Vector2 anchorMax = fillRect.anchorMax;
                        anchorMax.x = Normalized;
                        fillRect.anchorMax = anchorMax;
                    }
                }
            }
        }

        public float Normalized
        {
            get
            {
                if (Mathf.Approximately(value - min, 0) || Mathf.Approximately(max - min, 0))
                {
                    return 0;
                }

                return (value - min) / (max - min);
            }
        }
        
        // References

        [SerializeField]
        private Image bgImg;

        [SerializeField]
        private Image fillImg;

        [SerializeField]
        private TMP_Text labelTmp;

        #region Serialization

        public void OnBeforeSerialize()
        {
            Value = value;
        }

        public void OnAfterDeserialize() { }
        
        #endregion
    }
}
