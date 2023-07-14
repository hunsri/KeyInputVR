using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.AffordanceSystem.Receiver.Rendering;
using UnityEngine.XR.Interaction.Toolkit.AffordanceSystem.Rendering;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;

namespace KeyInputVR.Keyboard
{
    public class KeyManipulator : MonoBehaviour
    {   
        [SerializeField]
        private Material _activatedMaterial;

        private Material _previousMaterial;

        [SerializeField]
        private XRPokeFollowAffordance _followAffordance;

        [SerializeField]
        private ColorMaterialPropertyAffordanceReceiver _colorReceiver;

        [SerializeField]
        private MaterialPropertyBlockHelper _materialHelper;

        private void Start()
        {
            _previousMaterial = _materialHelper.rendererTarget.gameObject.GetComponent<Renderer>().material;
        }

        private void SetReturnsToInitialPosition(bool canReturn)
        {
            if(_followAffordance != null)
                _followAffordance.returnToInitialPosition = canReturn;
        }

        public void FixateToActivated()
        {   
            _materialHelper.rendererTarget.gameObject.GetComponent<Renderer>().material = _activatedMaterial;
        }

        public void ReleaseFromFixation()
        {
            _materialHelper.rendererTarget.gameObject.GetComponent<Renderer>().material = _previousMaterial;
        }
    }
}
