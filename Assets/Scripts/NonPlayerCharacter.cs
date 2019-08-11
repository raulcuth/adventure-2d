﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonPlayerCharacter : MonoBehaviour
{
    public float displayTime = 4.0f;

    public GameObject dialogBox;

    private float _timerDisplay;
    
    // Start is called before the first frame update
    void Start()
    {
        dialogBox.SetActive(false);
        _timerDisplay = -1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (_timerDisplay >= 0)
        {
            _timerDisplay -= Time.deltaTime;
            if (_timerDisplay < 0)
            {
                dialogBox.SetActive(false);
            }
        }
    }

    public void DisplayDialog()
    {
        _timerDisplay = displayTime;
        dialogBox.SetActive(true);
    }
}
