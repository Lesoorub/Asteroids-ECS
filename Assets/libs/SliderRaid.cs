using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderRaid : MonoBehaviour
{
    public Slider[] sliders;
    [Range(0, 1)]
    public float value;

    void Start()
    {
        Apply();
    }

    void Update()
    {
        Apply();
    }

    void Apply()
    {
        value = Mathf.Clamp01(value);
        foreach (var sl in sliders)
            if (sl != null)
                sl.value = value;
    }

    private void OnValidate()
    {
        Apply();
    }
}
