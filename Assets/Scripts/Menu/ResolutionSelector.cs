using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResolutionSelector : MonoBehaviour
{
    //public Dropdown resolutionDropdown; // Ссылка на Dropdown для отображения доступных разрешений
    private Resolution[] resolutions; // Массив доступных разрешений
    private int currentResolutionIndex = 0; // Текущий индекс выбранного разрешения

    [SerializeField] private GameObject resobj;
    private Text reslabel;


    public Color onColor = Color.green; // Цвет при включенном состоянии
    public Color offColor = Color.red; // Цвет при выключенном состоянии

    [SerializeField]
    private GameObject toggleObj;
    [SerializeField]
    private GameObject labelObj;
    
    private Toggle toggle;
    private Text label;
    
    List<string> options = new List<string>();
    private void Start()
    {
        toggle = toggleObj.GetComponent<Toggle>(); // Получаем компонент Toggle
        label = labelObj.GetComponent<Text>();
        reslabel = resobj.GetComponent<Text>();
        toggle.isOn = false;
        SetColor(toggle.isOn ? onColor : offColor);

        // Получаем список доступных разрешений
        resolutions = Screen.resolutions;

        // Заполняем Dropdown доступными разрешениями
        //resolutionDropdown.ClearOptions();
        

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);
        }

        // resolutionDropdown.AddOptions(options);

        // Устанавливаем начальное разрешение (текущее разрешение экрана)
        currentResolutionIndex = GetResolutionIndex(Screen.currentResolution);
        reslabel.text = options[currentResolutionIndex];
        // resolutionDropdown.value = currentResolutionIndex;
        // resolutionDropdown.RefreshShownValue();
    }

    private void Update()
    {
        SetColor(toggle.isOn ? onColor : offColor);
        // Обрабатываем нажатие стрелочек
        if (Input.GetKeyDown(KeyCode.RightArrow) & toggle.isOn)
        {
            ChangeResolution(1);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) & toggle.isOn)
        {
            ChangeResolution(-1);
        }
        // Обрабатываем нажатие Enter для подтверждения выбора
        else if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter) & toggle.isOn)
        {
            ApplyResolution();
            toggle.isOn = false;
        }
    }

    private void ChangeResolution(int direction)
    {
        // Изменяем индекс выбранного разрешения
        currentResolutionIndex = (currentResolutionIndex + direction) % resolutions.Length;
        if (currentResolutionIndex < 0)
            currentResolutionIndex = resolutions.Length - 1;

        // Обновляем Dropdown
        reslabel.text = options[currentResolutionIndex];
        // resolutionDropdown.value = currentResolutionIndex;
        // resolutionDropdown.RefreshShownValue();
    }

    private void ApplyResolution()
    {
        // Применяем выбранное разрешение
        Screen.SetResolution(resolutions[currentResolutionIndex].width, resolutions[currentResolutionIndex].height, Screen.fullScreen);
    }

    private int GetResolutionIndex(Resolution resolution)
    {
        // Находим индекс разрешения в массиве
        for (int i = 0; i < resolutions.Length; i++)
        {
            if (resolutions[i].width == resolution.width && resolutions[i].height == resolution.height)
            {
                return i;
            }
        }
        return 0; // Если разрешение не найдено, возвращаем индекс первого разрешения
    }
    private void SetColor(Color color)
    {
        // Устанавливаем цвет фона и создаем эффект свечения
        label.color = color;
    }
}