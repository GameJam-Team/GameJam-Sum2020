using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<Item> PickedItems;
    public GameObject CellPrefab;
    private void Start()
    {
        RectTransform rt = GetComponent<RectTransform>();
        RectTransform rtParent = rt.parent.GetComponent<RectTransform>();
        foreach (Item item in PickedItems)
        {
            GameObject gObj = Instantiate(CellPrefab, transform);
            gObj.GetComponent<ItemPresenter>().Present(item, rt, rtParent);
        }
    }
}
