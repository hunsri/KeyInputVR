using UnityEngine.InputSystem;

namespace KeyInputVR.KeyMaps
{
    public enum KeyType
    {
        UNSPECIFIED, REGULAR, SHIFT, CAPSLOCK, TAB, SPACE, BACKSPACE, ENTER
    };

    public static class KeyTypeExtensions
    {
        public static KeyType ToKeyType(this Key key) => key switch
        {
            Key.LeftShift => KeyType.SHIFT,
            Key.RightShift => KeyType.SHIFT,
            Key.CapsLock => KeyType.CAPSLOCK,
            Key.Tab => KeyType.TAB,
            Key.Space => KeyType.SPACE,
            Key.Backspace => KeyType.BACKSPACE,
            Key.Enter => KeyType.ENTER,
            Key.NumpadEnter => KeyType.ENTER,
            _ => KeyType.UNSPECIFIED
        };
    }
}
