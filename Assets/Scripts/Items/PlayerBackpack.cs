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
            window.Init(Player.Instance.backpack, 8, 3, false);
            break;
        }

        Close();
    }

    void Update()
    {
        if (!isOpen) return;

        // check for key commands

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Close();
        }
    }

    public void Open()
    {
        //gameObject.SetActive(true);
        isOpen = true;
        window.RefreshItems(0);
        Cursor.lockState = CursorLockMode.None;

        LeanTween.move(gameObject.GetComponent<RectTransform>(), new Vector3(0, 0, 0f), 0.3f).setDelay(0.1f);
    }

    void Close()
    {
        //gameObject.SetActive(false);
        isOpen = false;
        Cursor.lockState = CursorLockMode.Locked;
        LeanTween.move(gameObject.GetComponent<RectTransform>(), new Vector3(-1000, 0, 0f), 0.3f).setDelay(0.1f);
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
