using System.Collections;
using UnityEngine;
using TMPro;

public class HintZone : DeactivalibleZone
{
    [SerializeField] private TMP_Text _text = null;
    [SerializeField] private string _message = null;

    private static Coroutine _hintRoutine = null;

    protected override void ActivateZoneEffect(Player player)
    {
        if (_hintRoutine != null)
            StopCoroutine(_hintRoutine);
        if (_text.gameObject.activeSelf)
            _text.gameObject.SetActive(false);

        _hintRoutine = StartCoroutine(ShowHint());
    }

    protected override void DeactivateZoneEffect(Player player)
    {
        SetActive(false, Unactive);
    }

    private IEnumerator ShowHint()
    {
        yield return new WaitForSeconds(.5f);
        _text.gameObject.SetActive(true);
        _text.text = _message;
        yield return new WaitForSeconds(6);
        _text.gameObject.SetActive(false);
    }
}
