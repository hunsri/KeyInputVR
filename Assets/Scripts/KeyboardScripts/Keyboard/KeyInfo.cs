using System;
using KeyInputVR.KeyMaps;
using UnityEngine;
using UnityEngine.InputSystem;

namespace KeyInputVR.Keyboard
{
    public class KeyInfo : MonoBehaviour
    {
        private IKeyMap _keyMap;

        [SerializeField]
        private Key _key;

        [SerializeField]
        private KeyLabel _keyLabel;

        [SerializeField]
        private KeyMarker _keyMarker;

        private bool _isShifted;

        public event Action<IKeyMap, Key> OnKeyActivated = delegate { };

        // Start is called before the first frame update
        void Start()
        {
            if(_key == Key.None)
            {
                Debug.LogWarning("Key type of '"+ transform.name +"' isn't set!", gameObject);
            }

            if(_keyMap == null)
            {
                Debug.LogWarning("KeyMap of '"+ transform.name +"' isn't set!", gameObject);
            }
        }

        public Key GetKey()
        {
            return _key;
        }

        public void ActivateKey()
        {
            OnKeyActivated(_keyMap, _key);
        }

        public void SetKeyMap(IKeyMap keyMap)
        {
            _keyMap = keyMap;
        }

        public void SetShiftState(bool shift)
        {   
            _isShifted = shift;
            UpdateLabel();
        }

        public void LockAppearanceToActive()
        {
            _keyMarker.MarkAsActive();
        }

        public void ReleaseFromActiveAppearance()
        {
            _keyMarker.UnmarkAsActive();
        }

        private void OnValidate()
        {   
            UpdateLabel();
        }

        private void UpdateLabel()
        {
            if(_keyLabel != null && _keyMap != null)
            {   
                _keyLabel.UpdateLabel(_keyMap, _key, _isShifted);
            }
        }
    }
}
