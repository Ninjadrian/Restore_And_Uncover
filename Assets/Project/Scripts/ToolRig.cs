using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ToolRig : MonoBehaviour
{
    public static ToolRig Instance;
    
    [SerializeField] private Image toolIcon;
    [SerializeField] private TMP_Text toolName;

    [SerializeField] private Sprite emptyImage;

    [System.Serializable]
    public class ToolEntry
    {
        public ToolData toolData;
        public GameObject toolObject;
    }

    public List<ToolEntry> tools = new();

    private ToolData currentTool;

    private void Awake()
    {
        Instance = this;
    }

    public void TurnOff()
    {
        //Apaga todo
        for (int i = 0; i < tools.Count; i++)
        {
            if (tools[i].toolObject != null)
            {
                tools[i].toolObject.SetActive(false);
            }
        }
    }

    public void Unequip()
    {
        TurnOff();
        toolIcon.sprite = emptyImage;
        toolName.text = null;
    }

    public void Equip(ToolData tool)
    {
        currentTool = tool;

        TurnOff();

        //Prende la herramienta correspondiente
        for (int i = 0; i < tools.Count; i++)
        {
            if (tools[i].toolData == tool && tools[i].toolObject != null)
            {
                tools[i].toolObject.SetActive(true);
                toolIcon.sprite = tools[i].toolData.icon;
                toolName.text = tools[i].toolData.toolName;
                break;
            }
        }
    }

    public ToolData GetCurrentTool()
    {
        return currentTool;
    }
}
