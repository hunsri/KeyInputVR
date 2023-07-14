using System.Collections;
using System.Collections.Generic;
using KeyInputVR.KeyMaps;
using UnityEngine;

namespace KeyInputVR.Keyboard
{
    public class KeyboardInfo : MonoBehaviour
    {
        private static readonly KeyMappingType s_fallbackMapping = KeyMappingType.GERMAN_QWERTZ;

        [SerializeField]
        private KeyMappingType _selectedMapping;

        private IKeyMap _keyMap;

        private bool _isShifted;
        public bool IsShifted {get {return _isShifted;} set {SetShiftState(value);}}
        //public bool IsCapsLocked {get {return IsCapsLocked;} set{IsCapsLocked = value; UpdateCapsLockState();}}

        [SerializeField]
        public List<KeyInfo> KeyInfos = new List<KeyInfo>();
        
        public List<KeyInfo> RegularKeys {get;} = new List<KeyInfo>();

        public List<KeyInfo> ShiftKeys {get;} = new List<KeyInfo>();

        public List<KeyInfo> CapsKeys {get;} = new List<KeyInfo>();

        private void Awake()
        {
            ApplyMapping(_selectedMapping);

            foreach(KeyInfo info in KeyInfos)
            {   
                info.SetKeyMap(_keyMap);

                KeyType keyType = _keyMap.GetKeyMap()[info.GetKey()].KeyType;

                switch(keyType)
                {
                    case KeyType.REGULAR:
                        RegularKeys.Add(info);
                        break;
                    case KeyType.SHIFT:
                        ShiftKeys.Add(info);
                        break;
                    case KeyType.CAPSLOCK:
                        CapsKeys.Add(info);
                        break;
                    default:
                        break;
                }
            }
        }

        // private void OnValidate()
        // {
        //     ApplyMapping(_selectedMapping);
        // }

        private void ApplyMapping(KeyMappingType mapping)
        {
            if(_selectedMapping == KeyMappingType.NONE)
            {
                _selectedMapping = s_fallbackMapping;
                Debug.LogWarning("No keyboard mapping selected for '"+ transform.name +"!' Using fallback mapping '"+ _selectedMapping.ToString() +"'", gameObject);
            }
            switch(_selectedMapping)
            {
                case KeyMappingType.GERMAN_QWERTZ:
                    _keyMap = new GermanKeyMap();
                    break;
                default:
                    break;
            }
        }

        private void SetShiftState(bool shifted)
        {
            _isShifted = shifted;

            foreach(KeyInfo info in RegularKeys)
            {
                info.SetShiftState(shifted);
            }
        }

        private void UpdateCapsLockState()
        {

        }

    }
}
