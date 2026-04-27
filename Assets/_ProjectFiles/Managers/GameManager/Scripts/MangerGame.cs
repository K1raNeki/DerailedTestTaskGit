using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class MangerGame : MonoBehaviour
{
    [Header("Links")]
    [SerializeField] private ManagerGameData _data;
    [SerializeField] private Canvas _canvas;
    [SerializeField] private Image _background;
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private VideoPlayer _wictoryCry;

    [Header("SpawnItem")]
    [SerializeField] BaseItem[] _item;
    [SerializeField] Soket[] _sokets;

    public static MangerGame Instance;
    public static bool CourceCompledet;


    void Awake()
    {
        Instance = this;
        _canvas.gameObject.SetActive(false);
    }

    private void Start()
    {
        SpawnItem();
    }

    private void SpawnItem()
    {
        for (int i = 0; i < _item.Length; i++)
        {
            _item[i].transform.SetParent(_sokets[i].transform);
            _item[i].transform.position = _sokets[i].AttachPoint.position;
            _item[i].transform.localRotation = Quaternion.identity;
            _item[i].transform.localScale = Vector3.one;

            _sokets[i].CurretItem = _item[i];
            _item[i].MySoket = _sokets[i];
        }
    }

    public void Ending(bool good)
    {
        StartCoroutine(FinelSubtiles(good));
    }

    private IEnumerator FinelSubtiles(bool goodEnd)
    {
        PlayerController.Instance.State = PlayerState.Stan;
        yield return new WaitForSeconds(1f);

        _canvas.gameObject.SetActive(true);
        switch (goodEnd)
        {
            case true:
                _background.color = Color.white;
                _text.text = _data.GoodEndingText;
                _wictoryCry.Play();
                break;

            case false:
                _background.color = Color.red;
                _text.text = _data.BadEndingText;
                break;
        }
    }

}
