using Helpers.Observer;
using TMPro;
using UnityEngine;

namespace GameUI
{
    sealed class TimerTextExtension : MonoBehaviour
    {
        [SerializeField] private TMP_InputField _inputFieldReference;

        private void OnEnable()
        {
            _inputFieldReference.onValueChanged.AddListener(OnValueChanged);
        }
        private void OnDisable()
        {
            _inputFieldReference.onValueChanged.RemoveListener(OnValueChanged);
        }

        private void OnValueChanged(string value)
        {
            AlarmEnterEvent.Trigger(AlarmEnterType.Digits);
        }
    }
}
