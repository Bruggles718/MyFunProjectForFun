using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonPersistent<GameManager>
{
    [SerializeField] private UIButtonPrompt buttonPrompt;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetButtonPromptActive(bool value, string text, string buttonText)
    {
        this.buttonPrompt.SetActive(value, text, buttonText);
    }
}
