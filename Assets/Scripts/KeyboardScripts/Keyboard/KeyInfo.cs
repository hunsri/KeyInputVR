using System;
using KeyInputVR.KeyMaps;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;
using System.Diagnostics;
using Debug = UnityEngine.Debug;
using UnityEngine.XR.Interaction.Toolkit.AffordanceSystem.State;
using UnityEngine.XR.Interaction.Toolkit;
using Unity.VisualScripting;

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

        private bool _isActivated;

        private GameObject _activatedBy = null;

        XRPokeFollowAffordance _followAffordance;

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

            _followAffordance = GetComponentInChildren<XRPokeFollowAffordance>();
        }

        void Update()
        {
            if(_key == Key.A)
                Debug.Log(_followAffordance.initialPosition +"  "+ _followAffordance.pokeFollowTransform.localPosition);
            if(_isActivated)
            {   
                if(_followAffordance.pokeFollowTransform.localPosition == _followAffordance.initialPosition)
                {
                    _isActivated = false;
                }
            }
        }

        private void OnValidate()
        {
            UpdateLabel();
        }

        public Key GetKey()
        {
            return _key;
        }

        public void ActivateKey()
        {
            //if(!_isActivated)
            {
                //Debug.Log("activated");
                OnKeyActivated(_keyMap, _key);
                //_isActivated = true;
            }
        }

        public void DeactivateKey()
        {
            Debug.Log("deactivated");
            _isActivated = false;
        }

        public void EnterHover()
        {
            Debug.Log("enter hover");
        }

        public void EnterSelection(SelectEnterEventArgs eventArgs)
        {
            if(!_isActivated)
            {
                //_activatedBy = eventArgs.interactorObject.transform.gameObject;
                _isActivated = true;
                Debug.Log("ENTERED", eventArgs.interactorObject.transform.gameObject);

                ActivateKey();
            }
            else
            {

            }

            Debug.Log(eventArgs.interactableObject.isSelected);
        }

        public void ExitSelection(SelectExitEventArgs eventArgs)
        {
            //Debug.Log(eventArgs.interactableObject.interactorsSelecting.ToArray().Length, eventArgs.interactorObject.transform.gameObject);
            //XRInteractableAffordanceStateProvider stateProvider = GetComponentInChildren<XRInteractableAffordanceStateProvider>();
            //Debug.Log(stateProvider.gameObject.name, stateProvider.gameObject);
            XRPokeFollowAffordance followAffordance = GetComponentInChildren<XRPokeFollowAffordance>();
            Debug.Log(followAffordance.transform.localPosition);
        }

        public void ExitedHover()
        {
            Debug.Log("exited hover");
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

        private void UpdateLabel()
        {
            if(_keyLabel != null && _keyMap != null)
            {   
                _keyLabel.UpdateLabel(_keyMap, _key, _isShifted);
            }
        }
    }
}
