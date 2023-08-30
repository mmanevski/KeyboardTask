using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VirtualKeyboard : MonoBehaviour {

    public MyInputField InputField;

    private KeyCode keyPressed;
    private bool isPressed;

    private Coroutine processKeyCoro;

    public void HandleKeyPressed(KeyCode key)
    {

        if (key.ToString().Length == 1 && char.IsLetter(key.ToString()[0]))
        {
            //This is your desired action
            KeyPress(key.ToString());

        }
        if (key == KeyCode.LeftArrow)
        {
            KeyLeft();
        }
        if (key == KeyCode.RightArrow)
        {
            KeyRight();
        }
        if (key == KeyCode.Delete)
        {
            KeyDelete();
        }
    }

    public void OnKeyPressed(int key)
    {
        keyPressed = KeyCode.A;
        isPressed = true;
        processKeyCoro = StartCoroutine(ProcessKey());
    }

    public void OnKeyReleased()
    {
        keyPressed = KeyCode.None;
        isPressed = false;
        StopCoroutine(processKeyCoro);
    }

    private IEnumerator ProcessKey()
    {
        WaitForSeconds _longPressTrigger = new WaitForSeconds(1f);
        WaitForSeconds _longPressRepeat = new WaitForSeconds(0.1f);
        
        HandleKeyPressed(keyPressed);

        yield return _longPressTrigger;
   
        while (isPressed)
        {
            HandleKeyPressed(keyPressed);
            yield return _longPressRepeat;
        }

        yield break;
    }

    public void KeyPress(string c)
    {
        InputField.AddCharacter(c);
    }

    public void KeyLeft()
    {
        InputField.MoveCaret(-1);
    }

    public void KeyRight()
    {
        InputField.MoveCaret(1);
    }

    public void KeyDelete()
    {
        InputField.DeleteCharacter();
    }
}
