using UnityEngine;

public class ValidateKeyCard : MonoBehaviour
{
    public GameObject keyCard;

    private void Awake()
    {
        if(PowerSystem.HasPower == true)
        {
            keyCard.SetActive(true);
        }
    }

    public void ActiveSwitchCard()
    {
        ToolData activeTool = ToolRig.Instance.GetCurrentTool();

        if (activeTool != null && activeTool.id == "keyCard")
        {
            keyCard.SetActive(true);
            PowerSystem.SetPower(true);
            InventoryManager.Instance.RemoveTool(activeTool);
            ToolRig.Instance.Unequip();
        }
    }
}
