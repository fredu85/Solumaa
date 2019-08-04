using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OverviewHistoryUI : MonoBehaviour
{
    Queue<GameObject> HistoryEntries = new Queue<GameObject>();
    public GameObject Base;
    public StringList EntireHistory;
    int count;

    public void CreateNewEntry()
    {
        GameObject go = Instantiate(Base);
        go.transform.SetParent(gameObject.transform);
        go.transform.SetSiblingIndex(0);

        go.GetComponent<TextMeshProUGUI>().text = EntireHistory.Items[count];
        go.transform.localScale = new Vector3(1, 1, 1);
        count++;
        if (count > 15)
        {
            GameObject poop = HistoryEntries.Dequeue();
            Destroy(poop);
            HistoryEntries.Enqueue(go);
        }
        else
            HistoryEntries.Enqueue(go);
    }
}
