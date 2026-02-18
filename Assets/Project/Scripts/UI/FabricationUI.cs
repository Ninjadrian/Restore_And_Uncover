using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.HID;
using UnityEngine.UI;
using static UnityEngine.AudioSettings;

public class FabricationUI : MonoBehaviour
{
    public GameObject fabricationPanel;
    public GameObject hud;

    public Image blueprint;
    public Image result;

    public TMP_Text metalAmount;
    public TMP_Text plasticAmount;
    public TMP_Text cardboardAmount;

    private int indexFabricationItem = 0;

    private bool isPanelActive;

    private bool canFabricate = false;
    private bool isFabricating = false;

    private void Update()
    {
        if (GameManager.Instance.gameState == GameState.PLAY)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                Fabrication();
            }
        }
    }

    public void Fabrication()
    {        
        isPanelActive = !isPanelActive;
        fabricationPanel.SetActive(isPanelActive);  
        hud.SetActive(!isPanelActive);

        if (isPanelActive)
        {
            FabricationItem(indexFabricationItem);
        }
    }

    private void FabricationItem(int index)
    {
        var item = InventoryManager.Instance.unlockedBlueprints[index];
        blueprint.sprite = item.icon;

        int metal = InventoryManager.Instance.GetMaterialCount(MaterialType.Metal);
        int plastic = InventoryManager.Instance.GetMaterialCount(MaterialType.Plastic);
        int cardboard = InventoryManager.Instance.GetMaterialCount(MaterialType.Cardboard);

        if (isFabricating)
        {
            metal = metal - item.metalAmount;
            plastic = plastic - item.plasticAmount;
            cardboard = cardboard - item.cardboardAmount;

            InventoryManager.Instance.AddTool(item.resultTool);
        }

        metalAmount.text = metal + "/" + item.metalAmount.ToString();
        plasticAmount.text = plastic + "/" + item.plasticAmount.ToString();
        cardboardAmount.text = cardboard + "/" + item.cardboardAmount.ToString();

        result.sprite = item.resultTool.icon;

        if (metal >= item.metalAmount && plastic >= item.plasticAmount && cardboard >= item.cardboardAmount) 
        {
            canFabricate = true;
        }
    }

    public void FabricateButton()
    {
        if (canFabricate) 
        {
            isFabricating = true;
            FabricationItem(indexFabricationItem);
            canFabricate = false;
        }
    }

}
