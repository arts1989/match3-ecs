using UnityEngine;
using UnityEngine.EventSystems;
using Leopotam.Ecs;

namespace Match3
{
    public class DragAndDropable : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
    {
        private CanvasGroup _CanvasGroup;
        private Canvas _mainCanvas;
        private RectTransform _rectTransform;

        private Vector2 _startPosition;

        private void Start()
        {
            _rectTransform = GetComponent<RectTransform>();
            _mainCanvas = GetComponentInParent<Canvas>();
            _CanvasGroup = GetComponentInParent<CanvasGroup>();
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            _startPosition = transform.position;
            _CanvasGroup.blocksRaycasts = false;
        }

        public void OnDrag(PointerEventData eventData)
        {
            _rectTransform.anchoredPosition += eventData.delta / _mainCanvas.scaleFactor; 
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            transform.position = _startPosition; // Vector3.zero;
            _CanvasGroup.blocksRaycasts = true;
        }
    }
}