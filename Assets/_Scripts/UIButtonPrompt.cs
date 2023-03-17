using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIButtonPrompt : MonoBehaviour
{
    [Header("Animated Sprite")]
    [SerializeField] private Image image;
    [SerializeField] private Sprite sprite1;
    [SerializeField] private Sprite sprite2;
    [SerializeField] private float timePerSpriteInSeconds;

    private float initialWidth;

    [Header("Text")]
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private TextMeshProUGUI buttonText;
    
    private int currentImage;
    private float lockTime;

    private Vector3 originalButtonTextPos;
    [SerializeField] private float changeInYButtonText = 5f;
    private Vector3 changedPos;

    // Start is called before the first frame update
    void Start()
    {
        this.image.sprite = this.sprite1;
        this.currentImage = 0;
        this.lockTime = 0;
        this.gameObject.SetActive(false);
        this.originalButtonTextPos = this.buttonText.rectTransform.localPosition;
        this.changedPos = this.originalButtonTextPos;
        this.initialWidth = this.image.rectTransform.sizeDelta.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time < lockTime) return;
        this.SwapSprite();
    }

    private void SwapSprite()
    {
        this.changedPos.y = this.originalButtonTextPos.y - this.changeInYButtonText;
        if (this.currentImage == 0)
        {
            this.currentImage = 1;
            this.image.sprite = this.sprite2;
            this.buttonText.rectTransform.localPosition = this.changedPos;
        }
        else
        {
            this.currentImage = 0;
            this.image.sprite = this.sprite1;
            this.buttonText.rectTransform.localPosition = this.originalButtonTextPos;
        }
        this.lockTime = Time.time + timePerSpriteInSeconds;
    }

    public void SetActive(bool value, string text, string buttonText)
    {
        this.text.text = text;
        this.buttonText.text = buttonText;
        this.currentImage = 1;
        this.SwapSprite();
        this.image.rectTransform.sizeDelta = new Vector2(this.initialWidth + (this.initialWidth/2 * (this.buttonText.text.Length - 1)), this.image.rectTransform.sizeDelta.y);
        this.gameObject.SetActive(value);
    }
}
