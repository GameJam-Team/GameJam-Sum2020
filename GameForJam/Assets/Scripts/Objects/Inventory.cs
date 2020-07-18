using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<Item> PickedItems;
    public GameObject CellPrefab;
    private void Start()
    {
        var rt = GetComponent<RectTransform>();
        foreach (var item in PickedItems)
        {
            var gObj = Instantiate(CellPrefab, transform);
            gObj.GetComponent<ItemPresenter>().Present(item, rt, rt.parent.GetComponent<RectTransform>());
        }
    }
}
