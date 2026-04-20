using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class ComputerQuest : MonoBehaviour
{
    [Header("Links")]
    [SerializeField] CanvasGroup _fadeGroup;
    [SerializeField] VideoPlayer _videoPlayer;
    [SerializeField] Image[] _stepImg;

    private int _stepInt;
    private bool _questStart;

    // in a config InteractionController
    private Color _selectColor = Color.green;
    private Color _unselectColor = Color.white;


    private void Awake()
    {
        _unselectColor.a = 0.5f / 2f;
        _selectColor.a = 1;

        foreach (Image img in _stepImg) img.color = _unselectColor;

        _fadeGroup.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (_questStart && PlayerController.Instance.IsInteractPressed()) NextStep();
    }

    public void ComputerQuestStart(bool start)
    {
        _questStart = start;
        if (start) PlayerController.Instance.State = PlayerState.Stan;
        else
        {
            _videoPlayer.Play();
            PlayerController.Instance.State = PlayerState.Default;
        }
        StartCoroutine(FadeCanvas(start, 0.2f));
        MangerGame.CourceCompledet = true;
    }

    private void NextStep()
    {
        _stepImg[_stepInt].color = _selectColor;
        _stepInt++;
        if (_stepInt == _stepImg.Length) ComputerQuestStart(false);
    }

    private IEnumerator FadeCanvas(bool enable, float duration = 1f)
    {
        if (enable) _fadeGroup.gameObject.SetActive(true);

        for (float t = 0; t < 1; t += Time.deltaTime / duration)
        {
            _fadeGroup.alpha = enable ? t : 1 - t;
            yield return null;
        }
        _fadeGroup.alpha = enable ? 1 : 0;

        if (!enable) _fadeGroup.gameObject.SetActive(false);
    }

}
