using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenView : MonoBehaviour
{
    [SerializeField]
    Text m_Text;

    /// <summary>
    /// The Text component this behavior uses to display the updated text.
    /// </summary>
    public Text text
    {
        get => m_Text;
        set => m_Text = value;
    }

    /// <summary>
    /// See <see cref="MonoBehaviour"/>.
    /// </summary>
    protected void Awake()
    {
        if (m_Text == null)
            Debug.LogWarning("Missing required Text component reference. Use the Inspector window to assign which Text component to increment.", this);
    }

    public void AddString(string text)
    {
        if (m_Text != null)
        {
            m_Text.text += text;
        }
    }

    public void AddCharacter(char character)
    {
        if (m_Text != null)
        {
            m_Text.text += character;
        }
    }

    public void RemoveCharacter()
    {
        if (m_Text != null && m_Text.text.Length > 0)
        {
            m_Text.text = m_Text.text.Remove(m_Text.text.Length - 1);
        }
    }
}
