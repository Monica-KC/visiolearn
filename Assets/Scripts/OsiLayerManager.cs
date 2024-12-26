using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class OsiLayerManager : MonoBehaviour
{
     public GameObject packet;                // Packet object for animation
    // Text to display layer explanation
    public Animator packetAnimator;          // Animator for packet movement
    public GameObject mainButtonsPanel;      // Main buttons for OSI layers
    public GameObject firstPanel;  
    public GameObject secondPanel; 
    public GameObject thirdPanel;          // Overlay panel with back button

    // Physical Layer button click method
    public void OnPhysicalLayerButtonClicked()
    {
        firstPanel.SetActive(true);
        mainButtonsPanel.SetActive(false);

        // Start packet animation
        packetAnimator.Play("PacketMovement");

        // Show Physical Layer explanation
         }

     public void OnDataLayerButtonClicked()
    {
        secondPanel.SetActive(true);
        mainButtonsPanel.SetActive(false);

         }
     public void OnNetLayerButtonClicked()
    {
        thirdPanel.SetActive(true);
        mainButtonsPanel.SetActive(false);

         }


    
        
    

    // Back button to return to main screen
    
}
