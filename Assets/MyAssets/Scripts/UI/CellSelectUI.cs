using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

//Very WIP code for testing

public class CellSelectUI : MonoBehaviour
{
    public ObjectList playerCellList;
    public ObjectList cellTypeList;//0 Attack, 1 Average, 2 Secret, 3 Support, 4 All abilities
    public GameObject defaultAI;
    public GameObject Player;
    public GameObject defaultButton;
    public ToggleGroup chosenCell;

    public List<GameObject> cellButtons = new List<GameObject>();
    public List<GameObject> typeButtons = new List<GameObject>();
    //public List<TMP_Text> inputTexts = new List<TMP_Text>();
    //public List<CellSelectUIElement> textInputs = new List<CellSelectUIElement>();

    int activeListCount;
    bool defaultVars;

    public void ReceiveInput(int chosenType)
    {
        if (chosenCell.AnyTogglesOn())
        {
            if (defaultVars)
                defaultVars = false;

            //Get specific toggle button which is chosen cell index
            List<Toggle> toggles = chosenCell.ActiveToggles().ToList();
            int num = int.MinValue;
            for (int i = 0; i < cellButtons.Count; i++)
                if (cellButtons[i].GetComponent<Toggle>() == toggles[0])
                    num = i;

            //-1 is for default ai prefab type, or to place an empty prefab on the list, which is ignored in-game
            if (chosenType == -1)
            {
                playerCellList.Items[num + 1] = defaultAI;
                ((Image)toggles[0].targetGraphic).sprite = ((Image)defaultButton.GetComponent<Button>().targetGraphic).sprite;
                toggles[0].transform.Find("Selected").GetComponent<Image>().sprite = defaultButton.GetComponent<Button>().spriteState.pressedSprite;
                return;
            }

            ((Image)toggles[0].targetGraphic).sprite = ((Image)typeButtons[chosenType].GetComponent<Button>().targetGraphic).sprite;
            toggles[0].transform.Find("Selected").GetComponent<Image>().sprite = typeButtons[chosenType].GetComponent<Button>().spriteState.pressedSprite;

            //Add correct cell type to player cell group list
            if (num != int.MinValue)
            {
                if (!(playerCellList.Items.Count == activeListCount))
                    playerCellList.Add(cellTypeList.Items[chosenType]);
                else
                    playerCellList.Items[num + 1] = cellTypeList.Items[chosenType];
            }
        }
    }

    public void DefaultCellList()
    {
        defaultVars = true;
        playerCellList.Items.Clear();
        //Check amount of active cell buttons (amount of choosable cells)
        activeListCount = 0;
        for (int i = 0; i < cellButtons.Count; i++)
            if (cellButtons[i].activeInHierarchy)
                activeListCount++;

        //Populate empty player cell group list
        playerCellList.Add(Player);
        for (int i = 0; i < activeListCount; i++)
            playerCellList.Add(defaultAI);

        activeListCount++; //Add player to cell amount

        //Update button images
        foreach (var cell in cellButtons)
            if (cell.gameObject.activeInHierarchy)
            {
                ((Image)cell.GetComponent<Toggle>().targetGraphic).sprite = ((Image)defaultButton.GetComponent<Button>().targetGraphic).sprite;
                cell.transform.Find("Selected").GetComponent<Image>().sprite = defaultButton.GetComponent<Button>().spriteState.pressedSprite;
            }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (!CheckForDefaultState(playerCellList))
        {
            defaultVars = false;
            //Check amount of active cell buttons (amount of choosable cells)
            activeListCount = 0;
            for (int i = 0; i < cellButtons.Count; i++)
                if (cellButtons[i].gameObject.activeInHierarchy)
                    activeListCount++;

            activeListCount++; //Add player to cell amount

            //Check if player cell group list is of a different length to select UI capacity
            if (activeListCount < playerCellList.Items.Count)
                for (int i = playerCellList.Items.Count; i > activeListCount; i--)
                    playerCellList.Items.RemoveAt(i);
            else if (activeListCount > playerCellList.Items.Count)
                for (int i = playerCellList.Items.Count; activeListCount > i; i++)
                    playerCellList.Add(defaultAI);

            //Update button images
            int q = 1;
            foreach (var cell in cellButtons)
            {
                if (cell.gameObject.activeInHierarchy)
                {
                    for (int i = 0; i < cellTypeList.Items.Count; i++)
                    {
                        if (playerCellList.Items[q] == cellTypeList.Items[i])
                        {
                            ((Image)cell.GetComponent<Toggle>().targetGraphic).sprite = ((Image)typeButtons[i].GetComponent<Button>().targetGraphic).sprite;
                            cell.transform.Find("Selected").GetComponent<Image>().sprite = typeButtons[i].GetComponent<Button>().spriteState.pressedSprite;
                            break;
                        }
                        else if (playerCellList.Items[q] == defaultAI)
                        {
                            ((Image)cell.GetComponent<Toggle>().targetGraphic).sprite = ((Image)defaultButton.GetComponent<Button>().targetGraphic).sprite;
                            cell.transform.Find("Selected").GetComponent<Image>().sprite = defaultButton.GetComponent<Button>().spriteState.pressedSprite;
                            break;
                        }
                    }
                    q++;
                }
            }
        }
        else
            DefaultCellList();
    }

    bool CheckForDefaultState(ObjectList list)
    {
        //Go through the list, and if it only contains default AI prefabs, return that it is "unchanged" or default
        bool unchanged = false;
        for (int i = 0; i < list.Items.Count; i++)
        {
            if (list.Items[i] == defaultAI)
                unchanged = true;
            else
                unchanged = false;
            if (!unchanged)
                break;
        }
        return unchanged;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
