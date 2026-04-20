using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ManagerQuest : MonoBehaviour
{
    [Header("Links")]
    public static ManagerQuest Instance;
    [SerializeField] GameObject _mainContainer;
    [SerializeField] private Image _checkBox;
    [SerializeField] private Text _questText;

    public static bool BossQuestCompleted;

    void Awake()
    {
        Instance = this;

        DisableQuestContainer();
    }

    public void ShowQuest(string descr)
    {
        _mainContainer.SetActive(true);
        _questText.text = descr;
    }

    public void QuestCompleted(bool complete)
    {
        switch (complete)
        {
            case true:
                _checkBox.color = Color.green;
                MangerGame.Instance.Ending(false);
                break;

            case false:
                _checkBox.color = Color.red;
                break;
        }
        StartCoroutine(CompledetAnimation());
        BossQuestCompleted = true;
    }

    private IEnumerator CompledetAnimation()
    {
        yield return new WaitForSeconds(2f);
        DisableQuestContainer();
    }

    private void DisableQuestContainer()
    {
        _checkBox.color = Color.gray;
        _questText.text = "";
        _mainContainer.SetActive(false);
    }

}
