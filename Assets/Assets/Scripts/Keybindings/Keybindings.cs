using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Keybindings", menuName = "Keybindings")]
public class Keybindings : ScriptableObject
{
    [System.Serializable]
    public class KeybindingCheck
    {
        public KeybindingAction keybindingAction;
        public KeyCode keyCode;
        public bool modifierCtrl;
        public bool modifierAlt;
        public bool modifierShift;
    }

    public KeybindingCheck[] keybindingChecks; 
}
