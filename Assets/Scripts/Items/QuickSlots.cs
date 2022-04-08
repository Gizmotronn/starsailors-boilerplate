/*
 *  Quickslots that are always visible from the character view
 */

using UnityEngine;

public class QuickSlots : InstanceMonoBehaviour<QuickSlots>
{
    public bool isOpen { get; private set; } = false;
    public ItemWindow window;

    protected override void Awake()
    {
        base.Awake();

        Debug.Log("QuickSlots awake");
        window.Init(Player.Instance.backpack);
    }

    void Update()
    {
        if (!isOpen) return;

        // check for key commands

        if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1))
        {
            window.SetBoxSelected(0);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2))
        {
            window.SetBoxSelected(1);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Keypad3))
        {
            window.SetBoxSelected(2);
        }

        if (Input.GetKeyDown(KeyCode.Alpha4) || Input.GetKeyDown(KeyCode.Keypad4))
        {
            window.SetBoxSelected(3);
        }

        if (Input.GetKeyDown(KeyCode.Alpha5) || Input.GetKeyDown(KeyCode.Keypad5))
        {
            window.SetBoxSelected(4);
        }
    }

    public void Refresh()
    {
        window.RefreshItems(0);
    }

    public void Open(bool instant)
    {
        isOpen = true;
        Refresh();
        Cursor.lockState = CursorLockMode.None;

        Vector3 openpos = new Vector3(0, -160, 0f);

        if (instant)
            gameObject.transform.localPosition = openpos;
        else
            LeanTween.move(gameObject.GetComponent<RectTransform>(), openpos, 0.3f).setDelay(0.1f);
    }

    public void Close(bool instant)
    {
        isOpen = false;
        Cursor.lockState = CursorLockMode.Locked;

        Vector3 closedpos = new Vector3(0, -600, 0f);

        if (instant)
            gameObject.transform.localPosition = closedpos;
        else
            LeanTween.move(gameObject.GetComponent<RectTransform>(), closedpos, 0.3f).setDelay(0.1f);
    }
}
