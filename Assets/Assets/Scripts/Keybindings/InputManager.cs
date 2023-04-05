using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField]
    private PointerType[] keybindingSets;

    private Keybindings activeKeybindings;

    private void Awake()
    {
        activeKeybindings = keybindingSets[0].GetKeybindings();
    }

    public KeyCode GetKeyForAction(KeybindingAction keybindingAction)
    {
        foreach(Keybindings.KeybindingCheck keybindingCheck in activeKeybindings.keybindingChecks)
        {
            if (keybindingCheck.keybindingAction == keybindingAction)
            {
                return keybindingCheck.keyCode;
            }
        }

        return KeyCode.None;
    }

    public bool GetKeyDown(KeybindingAction key)
    {
        foreach (Keybindings.KeybindingCheck keybindingCheck in activeKeybindings.keybindingChecks)
        {
            if (keybindingCheck.keybindingAction == key)
            {
                bool ctrlCondition = !(keybindingCheck.modifierCtrl ^ (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)));
                bool shiftCondition = !(keybindingCheck.modifierShift ^ (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)));
                bool altCondition = !(keybindingCheck.modifierAlt ^ (Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt)));

                return(Input.GetKeyDown(keybindingCheck.keyCode) && ctrlCondition && shiftCondition && altCondition);
            }
        }

        return false;
    }

    public bool GetKeyUp(KeybindingAction key)
    {
        foreach (Keybindings.KeybindingCheck keybindingCheck in activeKeybindings.keybindingChecks)
        {
            bool ctrlCondition = !(keybindingCheck.modifierCtrl ^ (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)));
            bool shiftCondition = !(keybindingCheck.modifierShift ^ (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)));
            bool altCondition = !(keybindingCheck.modifierAlt ^ (Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt)));

            if (keybindingCheck.keybindingAction == key)
            {
                return(Input.GetKeyUp(keybindingCheck.keyCode) && ctrlCondition && shiftCondition && altCondition);
            }
        }

        return false;
    }

    public bool GetKey(KeybindingAction key)
    {
        foreach (Keybindings.KeybindingCheck keybindingCheck in activeKeybindings.keybindingChecks)
        {
            if (keybindingCheck.keybindingAction == key)
            {
                bool ctrlCondition = !(keybindingCheck.modifierCtrl ^ (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)));
                bool shiftCondition = !(keybindingCheck.modifierShift ^ (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)));
                bool altCondition = !(keybindingCheck.modifierAlt ^ (Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt)));

                return(Input.GetKey(keybindingCheck.keyCode) && ctrlCondition && shiftCondition && altCondition);
            }
        }

        return false;
    }

    public void SetActiveKeybindings(PointerTypes activePointer)
    {
        foreach(PointerType type in keybindingSets)
        {
            if(type.GetPointerType() == activePointer)
            {
                activeKeybindings = type.GetKeybindings();
                return;
            }
        }

        return;
    }
}
