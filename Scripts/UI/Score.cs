using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    [SerializeField] private TMP_Text _text = null;

    private int _amount = 0;
    private int _max = 0;

    private void Awake()
    {
        _max = FindObjectsOfType<CollectableItem>().Length;
        ChangeText();
    }

    private void OnEnable()
    {
        CollectableItem.OnItemCollect += OnItemCollect;
    }

    private void OnDisable()
    {
        CollectableItem.OnItemCollect -= OnItemCollect;
    }

    private void OnItemCollect()
    {
        _amount++;
        ChangeText();
    }

    private void ChangeText()
    {
        _text.text = $"{_amount}/{_max}";
    }
}
