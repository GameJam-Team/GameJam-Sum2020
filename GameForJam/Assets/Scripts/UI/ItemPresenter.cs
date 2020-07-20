using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemPresenter : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public Image Icon;
    private Item _currentItem = null;
    private Transform _transform;
    private RectTransform _holdParent;
    private RectTransform _dragParent;
    private void Awake()
    {
        _transform = GetComponent<Transform>();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        _transform.SetParent(_dragParent.transform);
    }
    public void OnDrag(PointerEventData eventData)
    {
        _transform.position = Input.mousePosition;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        _transform.SetParent(_holdParent.transform);
        if (!RectTransformUtility.RectangleContainsScreenPoint(_holdParent, Input.mousePosition))
        {
            Vector3 itemPos = Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 10);
            Instantiate(_currentItem.View, itemPos, Quaternion.identity);
            Destroy(gameObject);
        }
    }
    public void Present(Item item, RectTransform holdParent, RectTransform dragParent)
    {
        Icon.sprite = item.Icon;
        _currentItem = item;
        _transform = GetComponent<Transform>();
        _holdParent = holdParent;
        _dragParent = dragParent;
    }
}
