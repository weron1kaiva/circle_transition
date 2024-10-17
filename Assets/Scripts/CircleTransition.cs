using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CircleTransition : MonoBehaviour
{
    private Canvas _canvas;
    private Image _blackScreen;
    private static readonly int RADIUS = Shader.PropertyToID("_Radius");
    private void Awake()
    {
        Cursor.visible = false;
        _canvas = GetComponent<Canvas>();
        _blackScreen = GetComponentInChildren<Image>();
    }
    private void Start()
    {
        DrawBlackScreen();
    }
    private void Update()
    {
        HandleInput();
    }
    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            OpenBlackScreen();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            CloseBlackScreen();
        }
    }
    public void OpenBlackScreen()
    {
        StartCoroutine(Transition(2, 0, 1));
    }

    public void CloseBlackScreen()
    {
        StartCoroutine(Transition(2, 1, 0));
    }
    private void DrawBlackScreen()
    {
        var canvasRect = _canvas.GetComponent<RectTransform>().rect;
        var squareValue = Mathf.Max(canvasRect.width, canvasRect.height);
        _blackScreen.rectTransform.sizeDelta = new Vector2(squareValue, squareValue);
    }
    private IEnumerator Transition(float duration, float beginRadius, float endRadius)
    {
        var mat = _blackScreen.material;
        var time = 0f;
        while (time <= duration)
        {
            time += Time.deltaTime;
            var t = time / duration;
            var radius = Mathf.Lerp(beginRadius, endRadius, t);

            _blackScreen.material.SetFloat(RADIUS, radius);

            yield return null;
        }
    }

}

