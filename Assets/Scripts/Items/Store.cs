/*
 * This is a temp class for a generic item screen so I can test the item code : Chris
 */

using UnityEngine;

public class Store : InstanceMonoBehaviour<Store> // store is temp, this is just to test the item windows
{
    public ItemWindow window_1;
    public ItemWindow window_2;

    protected override void Awake()
    {
        base.Awake();

        window_1.Init(Player.Instance.backpack_1, 8, 3, false);
        window_2.Init(Player.Instance.backpack_2, 8, 3, false);
    }

    private void Start()
    {
        window_1.RefreshItems(0);
        window_2.RefreshItems(0);
    }

    public void Test_Gather_Widgets()
    {
        Item.Type t = Item.Type.none;

        while (t != Item.Type.puzzle && t != Item.Type.rabbit && t != Item.Type.spade)
            t = Item.GetRandomType();

        Item item = new Item(t, Random.Range(1, 3));

        window_1.AddItem(item);
        window_1.RefreshItems(0);

        for (int n = 0; n < item.number; n++)
            Lootlocker.Instance.AddItem(item);

        Debug.Log("Test_Gather_Widgets + " + item.type + " " + item.number);
    }
}
