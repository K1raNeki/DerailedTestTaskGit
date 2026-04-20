using TMPro;
using UnityEngine;

public class InspectionUI : MonoBehaviour
{
    [Header("Linkks")]
    public TextMeshProUGUI NameItem;
    public TextMeshProUGUI DescriptionItem;
    [SerializeField] private GameObject _canvasGameObj;


    private void Awake()
    {
        EnableInspectionUI(false);
    }

    public void EnterText(string nameItem, string descItem)
    {
        NameItem.text = nameItem;
        DescriptionItem.text = descItem;
    }

    public void EnableInspectionUI(bool enable)
    {
        NameItem.text = "";
        DescriptionItem.text = "";

        _canvasGameObj.SetActive(enable);
    }

}
