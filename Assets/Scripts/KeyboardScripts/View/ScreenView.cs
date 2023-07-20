using System.Reflection;
using TMPro;
using UnityEngine;

public class ScreenView : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField _inputField;

    protected void Awake()
    {
        if(_inputField == null)
        {
            Debug.LogWarning("Missing text input field to display the text output.", this);
        }

        SetCaretToVisible();
        SetCaretPosition(_inputField.text.Length);
    }

    public void InsertString(string text)
    {
        _inputField.text = _inputField.text.Insert(_inputField.caretPosition, text);
        SetCaretPosition(_inputField.caretPosition+text.Length);
    }

    public void RemoveNextCharacter()
    {
        if(_inputField.caretPosition < _inputField.text.Length)
        {
            _inputField.text = _inputField.text.Remove(_inputField.caretPosition, 1);
        }
    }

    public void RemovePreviousCharacter()
    {
        if(_inputField.caretPosition > 0)
        {
            _inputField.text = _inputField.text.Remove(_inputField.caretPosition-1, 1);

            // Removing the last element of a text also automatically adjusts the caret position.    
            // So only adjust caret position if it's not at the end of the text after removal.
            if(_inputField.caretPosition < _inputField.text.Length)
            {
                SetCaretPosition(_inputField.caretPosition-1);
            }
        }
    }

    public void BeginNewLine()
    {
        InsertString(System.Environment.NewLine);
        _inputField.MoveTextEnd(false);
    }

    public void MoveCaretToPreviousCharacter()
    {
        SetCaretPosition(_inputField.caretPosition-1);
    }

    public void MoveCaretToNextCharacter()
    {
        SetCaretPosition(_inputField.caretPosition+1);
    }

    private void SetCaretPosition(int position)
    {
        _inputField.caretPosition = position;
    }

    private void SetCaretToVisible()
    {
        // Using Reflection to make the caret visible even if the input field is not selected
        _inputField.GetType().GetField("m_AllowInput", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(_inputField, true);
        _inputField.GetType().InvokeMember("SetCaretVisible", BindingFlags.NonPublic | BindingFlags.InvokeMethod | BindingFlags.Instance, null, _inputField, null);
    }
}
