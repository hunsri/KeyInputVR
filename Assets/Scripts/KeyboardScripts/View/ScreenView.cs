using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScreenView : MonoBehaviour
{
    [SerializeField]
    Text m_Text;

    [SerializeField]
    private TMP_InputField _inputField;

    private int _caretPosition;

    /// <summary>
    /// The Text component this behavior uses to display the updated text.
    /// </summary>
    // public Text text
    // {
    //     get => m_Text;
    //     set => m_Text = value;
    // }

    /// <summary>
    /// See <see cref="MonoBehaviour"/>.
    /// </summary>
    protected void Awake()
    {
        // if (m_Text == null)
        //     Debug.LogWarning("Missing required Text component reference. Use the Inspector window to assign which Text component to increment.", this);
    
        if(_inputField == null)
            Debug.LogWarning("Missing text input field to display the text output.", this);
        else
        {
            SetCaretPosition(_inputField.text.Length);
        }
    }

    public void AddString(string text)
    {
        // if (m_Text != null)
        // {
        //     m_Text.text += text;
        // }

        if(_inputField != null)
        {
            _inputField.text += text;
        }
        SetCaretPosition(_inputField.text.Length);
    }

    public void AddCharacter(char character)
    {
        // if (m_Text != null)
        // {
        //     m_Text.text += character;
        // }

        if(_inputField != null)
        {
            _inputField.text += character;
        }
        SetCaretPosition(_inputField.text.Length);
    }

    public void RemoveCharacter()
    {
        // if (m_Text != null && m_Text.text.Length > 0)
        // {
        //     m_Text.text = m_Text.text.Remove(m_Text.text.Length - 1);
        // }

        if(_inputField != null && _inputField.text.Length > 0)
        {
            _inputField.text = _inputField.text.Remove(_inputField.text.Length - 1);
        }
        SetCaretPosition(_inputField.text.Length);
    }
    
    private void SetCaretPosition(int characterIndex)
    {
        _inputField.Select();
        _inputField.caretPosition = characterIndex;
        _inputField.ForceLabelUpdate();
    }

    void SetCarretVisible(int pos)
    {
        _inputField.caretPosition = pos; // desired cursor position
        
        _inputField.GetType().GetField("m_AllowInput", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(_inputField, true);
        _inputField.GetType().InvokeMember("SetCaretVisible", BindingFlags.NonPublic | BindingFlags.InvokeMethod | BindingFlags.Instance, null, _inputField, null);
    }
}
