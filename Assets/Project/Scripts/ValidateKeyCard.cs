using UnityEngine;

public class ValidateKeyCard : MonoBehaviour
{
    public GameObject keyCard;

    public void ActiveSwitchCard()
    {
        ToolData activeTool = ToolRig.Instance.GetCurrentTool();

        if (activeTool.id == "keyCard")
        {
            keyCard.SetActive(true);
            PowerSytem.SetPower(true);
            InventoryManager.Instance.RemoveTool(activeTool);
            ToolRig.Instance.Unequip();
        }
    }
}
