/*
 *  A window for building structures on the map : Chris
 */

using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BuildingScreen : InstanceMonoBehaviour<BuildingScreen>
{
    [SerializeField] private Material fadeMat;

    public bool isOpen { get; private set; } = false;

    Item item = null;
    GameObject preview = null;

    protected override void Awake()
    {
        base.Awake();

        // init stuff here

        Close(true, true);
    }

    void Update()
    {
        if (!isOpen) return;

        // check for key commands

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Close(false);
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            //if (Ground.Instance.BuildStructure(item, origin + transform.forward * 3))
            //{
            //    item.Clear();
            //    QuickSlots.Instance.window.SetBoxSelected(-1);
            //    QuickSlots.Instance.Refresh();
            //}

            Close(false);
        }
    }

    public void Open(bool instant, Item _item)
    {
        isOpen = true;

        item = _item;

        // make a preview structure and put it in front of the player so we can see how it fits in the game world

        preview = Ground.Instance.GetStructureForItem(item);

        preview.transform.SetParent(Player.Instance.gameObject.transform);

        preview.GetComponent<Renderer>().material = fadeMat;

        preview.transform.localPosition = new Vector3(0, 0, 7);

        QuickSlots.Instance.Close(instant);

        Cursor.lockState = CursorLockMode.None;

        Vector3 openpos = new Vector3(0, 0, 0f);

        if (instant)
            gameObject.transform.localPosition = openpos;
        else
            LeanTween.move(gameObject.GetComponent<RectTransform>(), openpos, 0.3f).setDelay(0.1f);
    }

    public void Close(bool instant, bool force = false)
    {
        if (!isOpen && !force) return;

        isOpen = false;
        Cursor.lockState = CursorLockMode.Locked;

        if (preview != null) Destroy(preview); // get rid of the preview structure if there is one

        Vector3 closedpos = new Vector3(-1000, 0, 0f);

        if (instant)
            gameObject.transform.localPosition = closedpos;
        else
            LeanTween.move(gameObject.GetComponent<RectTransform>(), closedpos, 0.3f).setDelay(0.1f);

        if(!PlayerBackpack.Instance.isOpen && !CraftingScreen.Instance.isOpen)
            QuickSlots.Instance.Open(instant);
    }

    
}
