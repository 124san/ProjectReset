using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class is used for changing properties of singleton classes
public class SingletonSetter : MonoBehaviour
{
    public static void SetGlowIndicator(bool value) {
        PlayerManager.instance.SetGlowIndicator(value);
    }

    public static void SetDisguise(bool value) {
        PlayerManager.instance.SetDisguise(value);
    }
}
