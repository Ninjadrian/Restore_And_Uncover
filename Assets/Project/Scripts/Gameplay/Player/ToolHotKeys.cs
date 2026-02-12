using UnityEngine;

public class ToolHotKeys : MonoBehaviour
{
    public ToolRig toolRig;

    private int currentIndex = -1;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) EquipSlot(0);
        if (Input.GetKeyDown(KeyCode.Alpha2)) EquipSlot(1);
        if (Input.GetKeyDown(KeyCode.Alpha3)) EquipSlot(2);
        if (Input.GetKeyDown(KeyCode.Alpha4)) EquipSlot(3);
        if (Input.GetKeyDown(KeyCode.Alpha5)) EquipSlot(4);

        float scroll = Input.mouseScrollDelta.y;

        if (scroll > 0) 
        {
            EquipSlot(currentIndex + 1);
        }
        else if (scroll < 0) 
        {
            EquipSlot(currentIndex - 1);
        }
    }

    void EquipSlot(int index)
    {
        var inv = InventoryManager.Instance.inventory;
        if (index < 0 || index >= inv.Count) return;

        toolRig.Equip(inv[index]);
        currentIndex = index;
    }

}
