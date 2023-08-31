
using System.Reflection;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;
using System;

public class MyInputField : InputFieldOriginal
{

    //Overriden so the input field is selected from the start.
    protected override void Start()
    {
        RefocusInputField();
        base.Start();
    }

    //Handles adding a character
    public void AddCharacter(string newChar)
    {
        RefocusInputField();
        Append(newChar);
        UpdateLabel();
    }

    //Handles Backspace pressed
    public void HandleBackspace()
    {

        bool _hasSelection = caretPositionInternal != caretSelectPositionInternal;

        //If nothing is selected and caret is at begging, do nothing.
        if (caretPosition == 0 && !_hasSelection)
            return;

        RefocusInputField();

        string _inputString = text;

        //Checking first if there is text selected, if not, delets a single character
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

    //Handles movement of the carret, move = -1 for left, move = 1 for right 
    public void MoveCaret(int move)
    {
        bool _hasSelection = caretPositionInternal != caretSelectPositionInternal;

        // If there is a selection, move carret to the leftmost/rightmost position of the selection.
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
        }
        else
        {
            int _newPos = caretPosition + move;

            if (_newPos >= 0 || _newPos < text.Length)
            {
                caretPositionInternal = caretSelectPositionInternal = _newPos;
                UpdateLabel();
                RefocusInputField();
            }
        }
    }

    //Handles Backspace when there is selection
    private String DeleteString(string originalText)
    {
        string newText = String.Empty;      

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
    
    // Overrides to the base OnSelect and OnDeselect to avoid reselection of text each time the InputField is brought in focus
    // by the on screen buttons

    public override void OnSelect(BaseEventData eventData)
    {
      
    }

    public override void OnDeselect(BaseEventData eventData)
    {
  
    }
       
}