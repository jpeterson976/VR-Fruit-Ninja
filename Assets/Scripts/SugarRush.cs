﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SugarRush : MonoBehaviour
{
    public Slider slider;

    public void SetTime(float time) => slider.value = time;
}
