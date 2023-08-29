using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VirtualKeyboard : MonoBehaviour {

    public MyInputField InputField;

    public void KeyPress(string c)
    {
        InputField.AddCharacter(c);
    }

    public void KeyLeft()
    {

    }

    public void KeyRight()
    {

    }

    public void KeyDelete()
    {
        InputField.DeleteCharacter();
    }
}
