using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VirtualKeyboard : MonoBehaviour {

    public MyInputField InputField;

    private bool isPressed;

    private Coroutine processKeyCoro;

    private string keyVal = string.Empty;

    private delegate void FuncKeyPress();

    private FuncKeyPress funcKeyPress;

    private float longPressTriggerTime = 0.5f;
    private float longPressRepeatTime = 0.05f;

    //Called when OnPointerDown is triggered on a character key on the onscreen keyboard. Stores the char in keyVal
    public void OnKeyPressed(string key)
    {
        keyVal = key;
        SetKeyPressed();
    }

    //Following three called when OnPointerDown is triggered on a function key  on the onscreen keyboard. Clears keyval.
    //Stores the required method for execution in a delegate. 
    public void OnLeftKeyPressed()
    {
        keyVal = string.Empty;
        funcKeyPress = KeyLeft;
        SetKeyPressed();
    }

    public void OnRightKeyPressed()
    {
        keyVal = string.Empty;
        funcKeyPress = KeyRight;
        SetKeyPressed();
    }

    public void OnDeletePressed()
    {
        keyVal = string.Empty;
        funcKeyPress = KeyDelete;
        SetKeyPressed();
    }

    private void SetKeyPressed()
    {
        isPressed = true;
        processKeyCoro = StartCoroutine(ProcessKey());
    }

    //Called when OnPointerUp is triggered on any key on the onscreen keyboard 
    public void OnKeyReleased()
    {
        keyVal = string.Empty;
        isPressed = false;
        funcKeyPress = null;
        StopCoroutine(processKeyCoro);
    }

    //Courutine for handling both single and long keypresses
    private IEnumerator ProcessKey()
    {
        WaitForSeconds _longPressTrigger = new WaitForSeconds(longPressTriggerTime);
        WaitForSeconds _longPressRepeat = new WaitForSeconds(longPressRepeatTime);
        
        HandleKeyPressed();

        yield return _longPressTrigger;
   
        while (isPressed)
        {
            HandleKeyPressed();
            yield return _longPressRepeat;
        }

        yield break;
    }

    private void HandleKeyPressed()
    {

        if (keyVal.Length != 0)
        {
            KeyPress(keyVal.ToString());

        }
        else
        {
            funcKeyPress?.Invoke();
        }
    }

    private void KeyPress(string c)
    {
        InputField.AddCharacter(c);
    }

    private void KeyLeft()
    {
        InputField.MoveCaret(-1);
    }

    private void KeyRight()
    {
        InputField.MoveCaret(1);
    }

    private void KeyDelete()
    {
        InputField.HandleBackspace();
    }
}