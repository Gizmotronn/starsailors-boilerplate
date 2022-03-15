using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;

public class ItemBox : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerDownHandler, IPointerUpHandler
{
    public GameObject inventory;
    public List<Sprite> images; // temp to test things

    public Image graphic;
    public TMPro.TextMeshProUGUI number;

    Item item = new Item();
    int index = 0;

    public ItemWindow parent { get; private set; } // a reference to the parent ItemWindow

    public void Init(ItemWindow _parent)
    {
        parent = _parent;
    }

    public bool IsValid() { return item != null && item.IsValid(); }

    public void Fill(int _index, Item _item)
    {
        item = _item;
        index = _index;

        Refresh_Icon();
    }

    void Refresh_Icon()
    {
        graphic.sprite = images[IsValid() ? (int)item.type : 0];
        number.text = IsValid() ? item.number.ToString() : "";
    }

    public void OnPointerDown(PointerEventData pointerEventData)
    {
        //Debug.Log("OnPointerDown: " + Debug_Get_Box_Info());
    }

    public void OnPointerUp(PointerEventData pointerEventData)
    {
        //Debug.Log("OnPointerUp: " + Debug_Get_Box_Info());
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (item == null || item.number < 1) return;

        graphic.transform.SetParent(inventory.transform);

        //Debug.Log("OnBeginDrag: " + Debug_Get_Box_Info());
    }

    public void OnDrag(PointerEventData eventData)
    {
        graphic.transform.position = eventData.position;

        //Debug.Log("OnDrag: " + Debug_Get_Box_Info());
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        graphic.transform.SetParent(transform);
        graphic.transform.localPosition = new Vector3(0, 0, 0);

        Debug.Log("OnEndDrag: " + Debug_Get_Box_Info());

        // see if we've dropped this box on a different box

        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = eventData.position;
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);

        foreach (var res in results)
        {
            if (res.gameObject.TryGetComponent(out ItemBox box))
            {
                if (this == box) continue;// if we're putting into the same box, don't go further

                var result = box.parent.PutItemAt(item.type, item.number, box.index);
                if (result.Item1 == 1)
                {
                    if (result.Item2 == 0)
                        item.Clear();
                    else
                        item.Set(result.Item2);
                }
                else if (result.Item1 == 0) // do a swap
                {
                    var oldtype = item.type;
                    var oldnum = item.number;

                    item.Set(box.item.type, box.item.number);
                    box.item.Set(oldtype, oldnum);
                }

                box.parent.RefreshItems();
                parent.RefreshItems();
            }
        }
    }

    public string Debug_Get_Box_Info()
    {
        return "Index: " + index + " " + name + " Item: " + (item != null ? item.type.ToString() : " empty ");
    }
}
