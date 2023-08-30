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

    public void OnKeyPressed(string key)
    {
        keyVal = key;
        SetKeyPressed();
    }

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

    public void OnKeyReleased()
    {
        keyVal = string.Empty;
        isPressed = false;
        funcKeyPress = null;
        StopCoroutine(processKeyCoro);
    }

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
        InputField.DeleteCharacter();
    }
}