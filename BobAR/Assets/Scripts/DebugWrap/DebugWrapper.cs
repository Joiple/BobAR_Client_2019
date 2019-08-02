﻿using TMPro;
using UnityEngine;

namespace DebugWrap {

    public class DebugWrapper : MonoBehaviour {
        public static TextMeshProUGUI labelText;
        public static int LineCounter = 0;
        [SerializeField] private TextMeshProUGUI internalLabel = null;

        public void Start() {
            labelText = internalLabel;
        }
    }


    public static class Debug {
        public static void Log(object message) {
            if (DebugWrapper.labelText != null) {
                DebugWrapper.labelText.text += ('\n' + message.ToString());

                if (DebugWrapper.LineCounter > 30) {
                    DebugWrapper.labelText.text =
                        DebugWrapper.labelText.text.Substring(DebugWrapper.labelText.text.IndexOf('\n'));
                }
                else DebugWrapper.LineCounter++;
            }

            UnityEngine.Debug.Log(message);
        }
    }

}