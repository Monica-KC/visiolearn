using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class cnButton : MonoBehaviour
{
   
    public TextMeshProUGUI messageText;
    public AudioSource audioSource;// Drag the UI Text or TMP Text element here

    public void OnButtonClick()
    {
        if (messageText != null)
        {
            messageText.text = "LET'S EXLPORE!!";
        }

        if (audioSource != null)
        {
            audioSource.Play(); // Play the audio
        }
}
}
