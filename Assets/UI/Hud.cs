using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hud : MonoBehaviour
{
    public Text text;
    public float roundStart;

    void Update() {
        float time = Time.time - roundStart;
        text.text = time.ToString("0.00");
    }
}
