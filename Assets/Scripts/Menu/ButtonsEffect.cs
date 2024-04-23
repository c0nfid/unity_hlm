using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Unity.VisualScripting;
public class ButtonsRotating : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Color color1;
    public Color color2;

    public List<Color> highlightColors = new List<Color> {new Color32(207, 104, 169, 255), new Color32(224, 198, 115, 255)}; //cf68a9 e0c673
    public List<Color> backColors = new List<Color> {new Color32(0, 104, 169, 255), new Color32(224, 0, 115, 255)}; //cf68a9 e0c673

    
    [SerializeField] private float duration = 0.001f;
    [SerializeField] GameObject back;
    [SerializeField] GameObject Wrapper;

    private Color startColor;
    private Color targetColor;
    private Color startHighlightColor;
    private Color targetHighlightColor;
    private Color startTextColor;
    private Color targetTextColor;

    private Button button;
    private RectTransform rectButtonTransform;
    private Text text;

    private float mod = 0.1f;

    private float zVal = 0.0f;

    private bool isMouseOver = false;

    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        rectButtonTransform = Wrapper.GetComponent<RectTransform>();
        if (back)
            text = back.GetComponent<Text>();
        
            

        startColor = color1;
        targetColor = color2;
        startHighlightColor = highlightColors[0];
        targetHighlightColor = highlightColors[1];
        startTextColor = backColors[0];
        targetTextColor = backColors[1];

        SetButtonColors(startColor, startHighlightColor);
        StartCoroutine(ChangeColor());
    }

    void Update()
        {
            text.enabled = (EventSystem.current.currentSelectedGameObject == button.gameObject) || isMouseOver;

            Vector3 ret = new Vector3(0, 0, zVal);
            rectButtonTransform.eulerAngles = ret;

            zVal += mod;

            if (rectButtonTransform.eulerAngles.z >= 1.38f && rectButtonTransform.eulerAngles.z < 1.5f)
            {
                mod = -0.001f;
            }
            else if (rectButtonTransform.eulerAngles.z < 359.0f && rectButtonTransform.eulerAngles.z > 350.5f)
            {
                mod = 0.001f;
            }
        }
    
    // IEnumerator ChangeColor()
    // {
    //     while(true)
    //     {

    //         float t = 0;
    //         while (t < duration)
    //         {
    //             Debug.Log(Time.deltaTime);
    //             t += Time.deltaTime;
    //             Color newNColor = Color.Lerp(startColor, targetColor, t / duration);
    //             Color newHColor = Color.Lerp(startHighlightColor, targetHighlightColor, t / duration);
                
    //             SetButtonColors(newNColor, newHColor);
    //             yield return null;
    //         }
    //         SwapColors();
    //     }
    // }

    private IEnumerator ChangeColor()
    {
        while (true)
        {
            yield return StartCoroutine(LerpColor(startColor, targetColor, startHighlightColor, targetHighlightColor, startTextColor, targetTextColor, duration));

            SwapColors();
        }
    }

    private IEnumerator LerpColor(Color startNormalColor, Color targetNormalColor, Color startHighlightColor, Color targetHighlightColor, Color startTextColor, Color targetTextColor, float duration)
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;

            SetButtonColors(Color.Lerp(startNormalColor, targetNormalColor, t), Color.Lerp(startHighlightColor, targetHighlightColor, t));
            if (text.enabled)
                SetTextColor(Color.Lerp(startTextColor, targetTextColor, t));
            yield return null;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isMouseOver = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isMouseOver = false;
    }

    void SwapColors()
    {
        (startColor, targetColor) = (targetColor, startColor);
        (startHighlightColor, targetHighlightColor) = (targetHighlightColor, startHighlightColor);
        (startTextColor, targetTextColor) = (targetTextColor, startTextColor);
        // startColor = b.colors.normalColor;
        //     if (startColor == color1)
        //         targetColor = color2;
        //     else if (startColor == color2)
        //         targetColor = color1;
        
        // startHColor = b.colors.highlightedColor;
        //     if (startHColor == highlightColors[0])
        //         targetHColor = highlightColors[1];
        //     else if (startHColor == highlightColors[1])
        //         targetHColor = highlightColors[0];
    }
    private void SetButtonColors(Color normal, Color highlight)
    {
        ColorBlock colorBlock = button.colors;
        colorBlock.normalColor = normal;
        colorBlock.highlightedColor = highlight;
        colorBlock.selectedColor = highlight;
        button.colors = colorBlock;
    }

    private void SetTextColor(Color color)
    {
        if (text)
            text.color = color;
    }
    
}
