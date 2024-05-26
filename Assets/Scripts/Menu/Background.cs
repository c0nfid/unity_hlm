using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Background : MonoBehaviour
{
    [SerializeField] List<Sprite> bg_images;
    // Start is called before the first frame update
    void Start()
    {
        Image img = GetComponent<Image>();
        img.sprite = GetRandomSprite();
        
    }

    private void Awake()
    {
        Application.targetFrameRate = -1;
        // Limit the framerate to 60
        Application.targetFrameRate = 60;
    }

    Sprite GetRandomSprite()
    {
        int index = Random.Range(0, bg_images.Count);
        return bg_images[index];
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
