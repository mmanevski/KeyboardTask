
using System.Reflection;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

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