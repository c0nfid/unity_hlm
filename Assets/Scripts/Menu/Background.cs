using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Background : MonoBehaviour
{
    [SerializeField] List<Sprite> bg_images;
    // Start is called before the first frame update
    void Start()
    {
        Image img = GetComponent<Image>();
        img.sprite = GetRandomSprite();
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
