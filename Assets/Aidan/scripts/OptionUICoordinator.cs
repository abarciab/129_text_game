using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteAlways]
public class OptionUICoordinator : MonoBehaviour
{
    [SerializeField] Slider slider;
    [SerializeField] Image fillImg;
    [Range(0, 1)] public float progress;

    private void Update()
    {
        slider.value = progress;
        var col = fillImg.color;
        col.a = Mathf.Pow(progress, 2);
        fillImg.color = col;
    }
}
