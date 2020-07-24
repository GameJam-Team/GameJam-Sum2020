using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;
using Image = UnityEngine.UI.Image;

public class ItemPresenter : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public Image Icon;
    private Item _currentItem = null;
    private Transform _transform;
    private RectTransform _holdParent;
    private RectTransform _dragParent;
    private RectTransform _enabledItemCell;
    private GameObject player;
    private GameObject enabledItem = null;

    private void Awake()
    {
        _transform = GetComponent<Transform>();
        player = GameObject.FindGameObjectWithTag("Player");
        _enabledItemCell = _transform.parent.parent.GetChild(1).GetComponent<RectTransform>();
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
        if (!RectTransformUtility.RectangleContainsScreenPoint(_dragParent.GetChild(0).GetComponent<RectTransform>(), Input.mousePosition))
        {
            if (!RectTransformUtility.RectangleContainsScreenPoint(_enabledItemCell, Input.mousePosition))
            {
                Vector3 itemPos = Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 10);
                Instantiate(_currentItem.View, itemPos, Quaternion.identity);
                Destroy(gameObject);
            } 
            else if (_enabledItemCell.childCount < 1)
            {
                _transform.SetParent(_enabledItemCell);
                _holdParent = _enabledItemCell;
                EnableItem();
            }
        }
        else if (_holdParent == _enabledItemCell)
        {
            _holdParent = _dragParent.GetChild(0).GetComponent<RectTransform>();
            _transform.SetParent(_holdParent);
            UnableItem();
        }
    }
    private void EnableItem()
    {
        var mnoj = +1;
        if (player.transform.rotation.eulerAngles == new Vector3(0, 180, 0)) mnoj = -1;
        enabledItem = Instantiate(_currentItem.View, player.transform.position + new Vector3(-0.33f * mnoj, -0.35f, 0), player.transform.rotation, player.transform);
        enabledItem.transform.localScale = new Vector3(0.75f, 0.75f, 1);
        enabledItem.transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = 1;
        enabledItem.transform.GetChild(1).GetComponent<SpriteRenderer>().sortingOrder = 1;
    }
    private void UnableItem()
    {
        Destroy(enabledItem);
        enabledItem = null;
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
