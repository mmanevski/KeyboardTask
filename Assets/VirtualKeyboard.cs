using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VirtualKeyboard : MonoBehaviour {

    public MyInputField InputField;

    private bool isPressed;

    private Coroutine processKeyCoro;

    private string keyVal = "";

    private delegate void FuncKeyPress();

    private FuncKeyPress funcKeyPress;

    public void HandleKeyPressed(string key)
    {

        if (key != "")
        {
            KeyPress(key.ToString());

        }
        else
        {
            funcKeyPress?.Invoke();
        }
    }


    public void OnKeyPressed(string key)
    {
        keyVal = key;
        isPressed = true;
        processKeyCoro = StartCoroutine(ProcessKey());
    }

    public void OnLeftKeyPressed()
    {
        keyVal = "";
        funcKeyPress = KeyLeft;
        isPressed = true;
        processKeyCoro = StartCoroutine(ProcessKey());
    }

    public void OnRightKeyPressed()
    {
        keyVal = "";
        funcKeyPress = KeyRight;
        isPressed = true;
        processKeyCoro = StartCoroutine(ProcessKey());
    }

    public void OnDeletePressed()
    {
        keyVal = "";
        funcKeyPress = KeyDelete;
        isPressed = true;
        processKeyCoro = StartCoroutine(ProcessKey());
    }

    public void OnKeyReleased()
    {
        keyVal = "";
        isPressed = false;
        funcKeyPress = null;
        StopCoroutine(processKeyCoro);
    }

    private IEnumerator ProcessKey()
    {
        WaitForSeconds _longPressTrigger = new WaitForSeconds(1f);
        WaitForSeconds _longPressRepeat = new WaitForSeconds(0.1f);
        
        HandleKeyPressed(keyVal);

        yield return _longPressTrigger;
   
        while (isPressed)
        {
            HandleKeyPressed(keyVal);
            yield return _longPressRepeat;
        }

        yield break;
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
