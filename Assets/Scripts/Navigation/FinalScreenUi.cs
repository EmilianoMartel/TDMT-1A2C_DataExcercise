using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalScreenUi : MonoBehaviour
{
    [SerializeField] private string _winText = "YOU WIN";
    [SerializeField] private string _looseText = "YOU LOOSE";

    [SerializeField] private TMPro.TMP_Text _text;
    [SerializeField] private BoolChannel _finalGame;

    private void OnEnable()
    {
        _finalGame.Sucription(SetFinalText);
    }

    private void OnDisable()
    {
        _finalGame.Unsuscribe(SetFinalText);
    }

    private void Awake()
    {
        if (!_text)
        {
            Debug.LogError($"{name}: Text is null.\nCheck and assigned one.\nDisabling component.");
            enabled = false;
            return;
        }
        if (!_finalGame)
        {
            Debug.LogError($"{name}: FinalGame is null.\nCheck and assigned one.\nDisabling component.");
            enabled = false;
            return;
        }
    }

    private void SetFinalText(bool win)
    {
        if (win) 
            _text.text = _winText;
        else _text.text = _looseText;
    }
}
