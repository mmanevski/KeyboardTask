
using System.Reflection;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;
using System;

public class MyInputField : InputFieldOriginal
{

    protected override void Start()
    {
        RefocusInputField();
        base.Start();
    }

    public void AddCharacter(string newChar)
    {
        RefocusInputField();
        Append(newChar);
        UpdateLabel();
    }
    public void DeleteCharacter()
    {
        bool _hasSelection = caretPositionInternal != caretSelectPositionInternal;

        if (caretPosition == 0 && !_hasSelection)
            return;

        RefocusInputField();

        string _inputString = text;

        if (_hasSelection)
        {
            _inputString = DeleteString(_inputString);
        }
        else
        {
            _inputString = _inputString.Remove(caretPositionInternal - 1, 1);
            caretPosition = caretPosition - 1;
        }

        text = _inputString;

        UpdateLabel();

    }

    public void MoveCaret(int move)
    {
        bool _hasSelection = caretPositionInternal != caretSelectPositionInternal;

        if (_hasSelection)
        {
            if (move == -1)
            {
                caretPositionInternal = caretSelectPositionInternal = Mathf.Min(caretPositionInternal, caretSelectPositionInternal);
            }
            else if (move == 1)
            {
                caretPositionInternal = caretSelectPositionInternal = Mathf.Max(caretPositionInternal, caretSelectPositionInternal);
            }

            UpdateLabel();
            RefocusInputField();

            return;
        }

        int _newPos = caretPosition + move;

        if (_newPos >= 0 || _newPos < text.Length)
        {
            caretPositionInternal = caretSelectPositionInternal = _newPos;
            UpdateLabel();
            RefocusInputField();
        }
    }

    private String DeleteString(string originalText)
    {
        string newText = String.Empty;
        if (caretPositionInternal == caretSelectPositionInternal)
        {
            return originalText;
        }
            

        if (caretPositionInternal < caretSelectPositionInternal)
        {
            newText = originalText.Substring(0, caretPositionInternal) + originalText.Substring(caretSelectPositionInternal, originalText.Length - caretSelectPositionInternal);
            caretSelectPositionInternal = caretPositionInternal;
        }
        else
        {
            newText = originalText.Substring(0, caretSelectPositionInternal) + originalText.Substring(caretPositionInternal, originalText.Length - caretPositionInternal);
            caretPositionInternal = caretSelectPositionInternal;
        }

        return newText;
    }

    private void RefocusInputField()
    {
        ActivateInputField();
        Select();
    }
    
    // TODO: Explain tomorrow :D

    public override void OnSelect(BaseEventData eventData)
    {
      
    }

    public override void OnDeselect(BaseEventData eventData)
    {
  
    }
       
}