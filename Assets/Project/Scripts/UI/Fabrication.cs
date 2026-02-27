using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.HID;
using UnityEngine.UI;

public class Fabrication : MonoBehaviour
{
    public GameObject fabricationPanel;
    public GameObject hud;

    public Image blueprint;
    public Image result;

    public TMP_Text metalAmount;
    public TMP_Text plasticAmount;
    public TMP_Text cardboardAmount;
    public TMP_Text electronicAmount;

    public int indexFabricationItem = 0;

    private bool isFabricationActive;

    private bool canFabricate = false;
    private bool isFabricating = false;

    private void Update()
    {
        if (GameManager.Instance.gameState == GameState.PLAY ||
            GameManager.Instance.gameState == GameState.Fabricate)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                Fabricate();
            }
        }
    }

    public void Fabricate()
    {        
        isFabricationActive = !isFabricationActive;
        fabricationPanel.SetActive(isFabricationActive);  
        hud.SetActive(!isFabricationActive);

        if (isFabricationActive)
        {
            GameManager.Instance.Fabrication();
            FabricationItem(indexFabricationItem);
        }
        else
        {
            GameManager.Instance.Play();
        }
    }

    private void FabricationItem(int index)
    {
        var item = InventoryManager.Instance.unlockedBlueprints[index];
        blueprint.sprite = item.icon;

        int metal = InventoryManager.Instance.GetMaterialCount(MaterialType.Metal);
        int plastic = InventoryManager.Instance.GetMaterialCount(MaterialType.Plastic);
        int cardboard = InventoryManager.Instance.GetMaterialCount(MaterialType.Cardboard);
        int electronic = InventoryManager.Instance.GetMaterialCount(MaterialType.Electronic);

        if (isFabricating)
        {
            metal = metal - item.metalAmount;
            plastic = plastic - item.plasticAmount;
            cardboard = cardboard - item.cardboardAmount;
            electronic = electronic - item.elecronicAmount;

            InventoryManager.Instance.AddTool(item.resultTool);
        }

        metalAmount.text = metal + "/" + item.metalAmount.ToString();
        plasticAmount.text = plastic + "/" + item.plasticAmount.ToString();
        cardboardAmount.text = cardboard + "/" + item.cardboardAmount.ToString();
        electronicAmount.text = electronic + "/" + item.elecronicAmount.ToString();

        result.sprite = item.resultTool.icon;

        if (metal >= item.metalAmount && plastic >= item.plasticAmount && 
            cardboard >= item.cardboardAmount && electronic >= item.elecronicAmount) 
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

    public void Next()
    {
        if (indexFabricationItem >= InventoryManager.Instance.unlockedBlueprints.Count - 1) return;

        indexFabricationItem += 1;
        FabricationItem(indexFabricationItem);
    }

    public void Previous()
    {
        if (indexFabricationItem == 0) return;

        indexFabricationItem -= 1;
        FabricationItem(indexFabricationItem);
    }
}
