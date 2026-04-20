using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BossNPC : MonoBehaviour, IInteractable
{
    [Header("Links")]
    public BossData BossConfig;
    [SerializeField] private ItemData _neddetItem;
    [SerializeField] private Canvas _bossCanvas;
    [SerializeField] private Image[] _buttons;
    [SerializeField] private TextMeshProUGUI _textSpeetchs;

    [SerializeField] private BossState _bossState;
    private int _stepSpeetch;

    void Awake()
    {
        ShowCanvas(false);
    }

    void Update()
    {
        if (_bossCanvas.gameObject.activeSelf) _bossCanvas.transform.forward = Camera.main.transform.forward;
    }

    public string GetInteractText()
    {
        switch (_bossState)
        {
            case BossState.Start:
                return BossConfig.StartIIText;

            case BossState.Reaction:
                if (PlayerController.Instance.HaveItem)
                {
                    return BossConfig.ReationOnItem[1];
                }
                else return BossConfig.ReationOnItem[0];

            case BossState.Finished:
                return BossConfig.EndIIText;
        }

        return "";
    }

    public void Interact()
    {
        switch (_bossState)
        {
            case BossState.Start:
                if (_stepSpeetch == 0)
                {
                    _stepSpeetch++;
                    ShowCanvas(true);
                    _textSpeetchs.text = BossConfig.StartSpeech;
                }
                else if (_stepSpeetch > 0)
                {
                    ManagerQuest.Instance.ShowQuest(BossConfig.MainQuestDescription);
                    ShowCanvas(false);
                    _bossState = BossState.Reaction;
                    _stepSpeetch = 0;
                }
                break;

            case BossState.Reaction:
                ShowCanvas(true);
                if (PlayerController.Instance.HaveItem
                && _neddetItem.IetmID == PlayerController.Instance.CurretItem.Data.IetmID)
                {
                    _textSpeetchs.text = BossConfig.TrueReaction;
                    ShowButtons(true);
                    if (_stepSpeetch > 0) BossIsHappy();
                    _stepSpeetch++;
                }
                else if (PlayerController.Instance.HaveItem)
                {
                    int speetch = Random.Range(0, BossConfig.FalseReaction.Length);
                    _textSpeetchs.text = BossConfig.FalseReaction[speetch];
                }
                break;

            case BossState.Finished:
                break;
        }
    }

    public void BossDeceived()
    {
        _textSpeetchs.text = BossConfig.DisappoinntmentSpeech;
        ShowButtons(false);
        _bossState = BossState.Finished;
        _stepSpeetch = 0;

        ManagerQuest.Instance.QuestCompleted(false);
    }
    public void BossIsHappy()
    {
        Destroy(PlayerController.Instance.CurretItem.gameObject);
        PlayerController.Instance.CurretItem = null;
        PlayerController.Instance.HaveItem = false;

        ShowButtons(false);
        _textSpeetchs.text = BossConfig.GratitudeReaction;
        _bossState = BossState.Finished;
        _stepSpeetch = 0;

        ManagerQuest.Instance.QuestCompleted(true);
    }

    private void ShowCanvas(bool show)
    {
        if (!show)
        {
            ShowButtons(show);
            _textSpeetchs.text = "";
        }
        _bossCanvas.gameObject.SetActive(show);
    }

    private void ShowButtons(bool show)
    {
        foreach (Image button in _buttons) button.gameObject.SetActive(show);
    }
}

public enum BossState
{
    Start,
    Reaction,
    Finished
}
