using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlowIndicatorSetter : MonoBehaviour
{
    public static void SetGlowIndicator(bool value) {
        PlayerManager.instance.SetGlowIndicator(value);
    }
}
