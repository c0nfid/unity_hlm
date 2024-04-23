using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettingsEffects : MonoBehaviour
{
    public Color onColor = Color.green; // Цвет при включенном состоянии
    public Color offColor = Color.red; // Цвет при выключенном состоянии
    public float blinkingSpeed = 2f; // Скорость мигания
    public float glowWidth = 0.1f; // Ширина свечения

    [SerializeField]
    private GameObject toggleObj;
    [SerializeField]
    private GameObject labelObj;


    private Toggle toggle; // Компонент Toggle
    private Text label; // Изображение фона


    private void Start()
    {
        toggle = toggleObj.GetComponent<Toggle>(); // Получаем компонент Toggle
        label = labelObj.GetComponent<Text>();
        SetColor(Screen.fullScreen ? onColor : offColor);
    }



    public void FullScreenToggle()
    {
        Screen.fullScreen = toggle.isOn;
        SetColor(toggle.isOn ? onColor : offColor);
    }
    private void SetColor(Color color)
    {
        // Устанавливаем цвет фона и создаем эффект свечения
        label.color = color;
    }
}
