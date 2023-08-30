
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

        if (caretPosition == 0)
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

        int _newPos = caretPosition + move;

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

        if (_newPos >= 0 || _newPos < text.Length)
        {
            caretPositionInternal = caretSelectPositionInternal = _newPos;
            UpdateLabel();
            RefocusInputField();
        }


    }

    private String DeleteString(string newText)
    {
        if (caretPositionInternal == caretSelectPositionInternal)
            return newText;

        if (caretPositionInternal < caretSelectPositionInternal)
        {
            newText = text.Substring(0, caretPositionInternal) + text.Substring(caretSelectPositionInternal, text.Length - caretSelectPositionInternal);
            caretSelectPositionInternal = caretPositionInternal;
        }
        else
        {
            newText = text.Substring(0, caretSelectPositionInternal) + text.Substring(caretPositionInternal, text.Length - caretPositionInternal);
            caretPositionInternal = caretSelectPositionInternal;
        }

        return newText;
    }

    private void RefocusInputField()
    {
        Debug.Log("Internal caret: " + caretPositionInternal + " Selection caret: " + caretSelectPositionInternal + " CaretPos: " + caretPosition);
        ActivateInputField();
        Select();
        Debug.Log("Internal caret: " + caretPositionInternal + " Selection caret: " + caretSelectPositionInternal + " CaretPos: " + caretPosition);
    }

    // TODO: Because you can't really explain this, try to fix this by resseting the caretInternal vars
    
    public override void OnSelect(BaseEventData eventData)
    {

        //Debug.Log("Overrides InputField.OnSelect");
        
    }

    public override void OnDeselect(BaseEventData eventData)
    {
        //Debug.Log("Overrides InputField.Deselect")
    }
       



}