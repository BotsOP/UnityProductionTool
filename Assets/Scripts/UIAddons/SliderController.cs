using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SliderController : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private TMP_InputField inputField;

    public void sliderChanged(float _value)
    {
        Debug.Log(_value + "slider");
        _value = Mathf.Round(_value * 100f) / 100f;
        inputField.text = _value.ToString();
    }

    public void inputChanged(string _value)
    {
        Debug.Log(float.Parse(_value) + " input");
        slider.value = float.Parse(_value);
    }
}
