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

    public static MangerGame Instance;
    public static bool CourceCompledet;


    void Awake()
    {
        Instance = this;
        _canvas.gameObject.SetActive(false);
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
