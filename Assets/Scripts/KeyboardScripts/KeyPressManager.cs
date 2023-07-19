using UnityEngine;
using UnityEngine.InputSystem;
using KeyInputVR.KeyMaps;
using KeyInputVR.Keyboard;

public class KeyPressManager : MonoBehaviour
{
    [SerializeField]
    private ScreenView _screenView;

    [SerializeField]
    private KeyboardInfo _keyboard;

    private void Awake()
    {   
        KeyInfo[] keyInfos;
        
        if(_keyboard == null)
        {
            Debug.LogWarning("No keyboard info attached to '"+ transform.name +"'! Directly accessing all KeyInfos inside the scene instead.", gameObject);
            keyInfos = Resources.FindObjectsOfTypeAll<KeyInfo>();
        }
        else
        {
            keyInfos = _keyboard.KeyInfos.ToArray();
        }


        foreach(KeyInfo info in keyInfos)
        {   
            info.OnKeyActivated += KeyActivated;
        }
    }

    private void KeyActivated(IKeyMap keyMap, Key key)
    {
        KeyDefinition keyDefinition = keyMap.GetKeyMap()[key];

        HandleInput(keyDefinition);
    }

    private void HandleInput(KeyDefinition keyDefinition)
    {
        switch(keyDefinition.KeyType)
        {
            case KeyType.REGULAR:
                {
                    string text = ShouldOutputShiftedVariant() ? keyDefinition.ShiftedOutput : keyDefinition.BaseOutput;
                    _screenView.AddString(text);
                    ApplyShiftKeyState(false);
                    break;
                }
            case KeyType.SPACE:
                {
                    _screenView.AddString(" ");
                    ApplyShiftKeyState(false);
                    break;
                }
            case KeyType.BACKSPACE:
                {
                    _screenView.RemoveCharacter();
                    ApplyShiftKeyState(false);
                    break;
                }
            case KeyType.TAB:
                {
                    //four spaces
                    _screenView.AddString("    ");
                    ApplyShiftKeyState(false);
                    break;
                }
            case KeyType.SHIFT:
                {
                    ApplyShiftKeyState(true);
                    break;
                }
            case KeyType.CAPSLOCK:
                {
                    InvertCapsLockState();   
                    break;
                }
            default: break;
        }
    }

    private void InvertCapsLockState()
    {
        if(_keyboard != null)
        {
            _keyboard.IsCapsLocked = !_keyboard.IsCapsLocked;
            _keyboard.DisplayCapsLockKeysAsActive(_keyboard.IsCapsLocked);
        }
    }

    private void ApplyShiftKeyState(bool lastKeyWasShift)
    {
        if(_keyboard != null)
        if(lastKeyWasShift && !_keyboard.IsShifted)
        {
            _keyboard.IsShifted = true;
            _keyboard.DisplayShiftKeysAsActive(true);
        }
        else
        {
            _keyboard.IsShifted = false;
            _keyboard.DisplayShiftKeysAsActive(false);
        }
    }

    private bool ShouldOutputShiftedVariant()
    {
        return _keyboard.IsSetToUppercase;
    }
}
