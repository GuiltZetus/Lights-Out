using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    public Slider slider;
    public void setMaxHP(float hp)
    {
        //set the starting hp to max hp
        slider.maxValue = hp;
        slider.value = hp;
    }
    public void setHP(float hp)
    {
        slider.value = hp; 
    }
}
