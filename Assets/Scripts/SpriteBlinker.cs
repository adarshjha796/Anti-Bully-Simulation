using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpriteBlinker : MonoBehaviour
{
    [Header("Image")]
    [SerializeField] private SpriteRenderer scrollImage;

    [Header("Color")]
    [SerializeField] private Color color1;
    [SerializeField] private Color color2;


    private void Update()
    {
        BlinkingImage();
    }


    public void BlinkingImage()
    {
        scrollImage.color = Color.Lerp(color1,color2,Mathf.PingPong(Time.time,1));
    }
}