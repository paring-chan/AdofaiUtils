using System;
using UnityEngine;
using UnityEngine.UI;

namespace AdofaiUtils2.Unity
{
    [ExecuteInEditMode]
    public class ItemFitter : MonoBehaviour
    {
        public LayoutElement layoutElement;
        public RectTransform childTransform;
    
        private void Awake()
        {
            layoutElement = GetComponent<LayoutElement>();
            childTransform = transform.GetChild(0) as RectTransform;
        }

        void Update()
        {
            layoutElement.preferredHeight = childTransform.rect.height;
        }
    }
}