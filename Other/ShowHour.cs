using System;
using TMPro;
using UnityEngine;

namespace GameMain
{
    [RequireComponent(typeof(TMP_Text))]
    public class ShowHour : MonoBehaviour
    {
        private TMP_Text _glassesText;

        private void Start()
        {
            _glassesText = GetComponent<TMP_Text>();
            InvokeRepeating(nameof(SetHour), 0, 60);
        }

        private void SetHour()
        {
            _glassesText.text = $"G E E";
        }
    }
}