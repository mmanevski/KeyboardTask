
using System.Reflection;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;
using System;

public class MyInputField : InputFieldOriginal
{
    bool isActivated;

    protected override void Start()
    {
        //Maybe a bit wonkey
        ActivateInputField();
        base.Start();
      
    }

    public void AddCharacter(string _char)
    {
        ActivateInputField();
        Select();
        Append(_char);
        UpdateLabel();
        //EventSystem.current.SetSelectedGameObject(this.gameObject);
        //caretPosition = text.Length;
    }
    public void DeleteCharacter()
    {
        bool _hasSelection = caretPositionInternal != caretSelectPositionInternal;

        if (caretPosition == 0)
            return;


        ActivateInputField();
        Select();

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

    private void MoveCaret(int _move)
    { 
        
    }

    private String DeleteString(string _text)
    {
        if (caretPositionInternal == caretSelectPositionInternal)
            return _text;

        if (caretPositionInternal < caretSelectPositionInternal)
        {
            _text = text.Substring(0, caretPositionInternal) + text.Substring(caretSelectPositionInternal, text.Length - caretSelectPositionInternal);
            caretSelectPositionInternal = caretPositionInternal;
        }
        else
        {
            _text = text.Substring(0, caretSelectPositionInternal) + text.Substring(caretPositionInternal, text.Length - caretPositionInternal);
            caretPositionInternal = caretSelectPositionInternal;
        }

        return _text;
    }

    public override void OnSelect(BaseEventData eventData)
    {
        Debug.Log("Overrides InputField.OnSelect");
        //base.OnSelect(eventData);
        //ActivateInputField();
    }

    public override void OnDeselect(BaseEventData eventData)
    {
        Debug.Log("Overrides InputField.Deselect");
        //DeactivateInputField();
        //base.OnDeselect(eventData);
    }



}