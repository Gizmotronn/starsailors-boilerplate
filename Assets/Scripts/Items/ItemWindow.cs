/*
 *  A window that holds a list of items in the ui
 */


using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemWindow : MonoBehaviour
{
    public int items_per_row;
    public int items_per_column;
    public bool horizontal = false;

    public GameObject parent;

    public Scrollbar scrollbar;

    public ItemBox itembox;

    List<ItemBox> boxes;

    Inventory inventory;

    public void Init(Inventory _inventory)
    {
        inventory = _inventory;

        // end

        boxes = new List<ItemBox>();

        for (int i = 0; i < items_per_row * items_per_column; i++) // go to 28 which is how many will fit in the window
        {
            var copy = Instantiate(itembox.gameObject); // make a copy
            copy.SetActive(true); // set it active
            copy.name = "item " + (i + 1); // name the object
            copy.transform.SetParent(gameObject.transform); // set parent to this

            var box = copy.GetComponent<ItemBox>();
            box.Init(this);

            boxes.Add(box); // add it to the list
        }

        scrollbar.value = 0; // set up the window to be at the beginning of the list
        ScrollBar(); // call scrollbar to refresh the items
    }

    public void AddItem(Item item)
    {
        inventory.AddItem(item.type, item.number);
    }

    public (int, int) PutItemAt(Item.Type type, int number, int index)
    {
        return inventory.PutItemAt(type, number, index);
    }

    public void ScrollBar()
    {
        RefreshItems(scrollbar.value);
    }

    public void RefreshItems(float percent = -1)
    {
        if (percent < 0) percent = scrollbar.value;

        int columns;
        int top;

        if (!horizontal)
        {
            columns = inventory.CountItems() / items_per_row + (inventory.CountItems() % items_per_row > 0 ? 1 : 0); // get total number of columns

            top = (int)(percent * columns);

            if (top > columns - items_per_column) top = columns - items_per_column;
            if (top < 0) top = 0;
        }
        else
        {
            columns = inventory.CountItems() / items_per_column + (inventory.CountItems() % items_per_column > 0 ? 1 : 0); // get total number of columns

            top = (int)(percent * columns);

            if (top > columns - items_per_row) top = columns - items_per_row;
            if (top < 0) top = 0;
        }

        for (int i = 0; i < items_per_row * items_per_column; i++)
        {
            int itm = i + top * items_per_row; // get a reference to the right item

            if (horizontal) itm = i + top * items_per_column;

            boxes[i].Fill(itm, inventory.GetItemAt(itm)); // put the item in the box
        }
    }
}
