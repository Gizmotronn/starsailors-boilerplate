/*
 *  A item window for the player containg the list of items in the player backpack
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBackpack : InstanceMonoBehaviour<PlayerBackpack> // player item menu, bringing this up pauses the game
{
    public bool isOpen { get; private set; } = false;
    ItemWindow window;

    protected override void Awake()
    {
        base.Awake();

        foreach (ItemWindow child in gameObject.transform.GetComponentsInChildren<ItemWindow>(true))
        {
            window = child;
            window.Init(Player.Instance.backpack);
            break;
        }

        Close(true);
    }

    void Update()
    {
        if (!isOpen) return;

        // check for key commands

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Close(false);
        }
    }

    public void Open(bool instant)
    {
        //gameObject.SetActive(true);
        isOpen = true;
        window.RefreshItems(0);
        Cursor.lockState = CursorLockMode.None;

        Vector3 openpos = new Vector3(0, 0, 0f);

        if (instant)
            gameObject.transform.localPosition = openpos;
        else
            LeanTween.move(gameObject.GetComponent<RectTransform>(), openpos, 0.3f).setDelay(0.1f);
    }

    void Close(bool instant)
    {
        //gameObject.SetActive(false);
        isOpen = false;
        Cursor.lockState = CursorLockMode.Locked;

        Vector3 closedpos = new Vector3(-1000, 0, 0f);

        if (instant)
            gameObject.transform.localPosition = closedpos;
        else
            LeanTween.move(gameObject.GetComponent<RectTransform>(), closedpos, 0.3f).setDelay(0.1f);
    }

    //void Test_Gather_Widgets()
    //{
    //    Item.Type t = Item.Type.none;

    //    while (t != Item.Type.puzzle && t != Item.Type.rabbit && t != Item.Type.spade)
    //        t = Item.GetRandomType();

    //    Item item = new Item(t, Random.Range(1, 3));

    //    window_1.AddItem(item);
    //    window_1.RefreshItems(0);

    //    for (int n = 0; n < item.number; n++)
    //        Lootlocker.Instance.AddItem(item);

    //    Debug.Log("Test_Gather_Widgets + " + item.type + " " + item.number);
    //}
}
