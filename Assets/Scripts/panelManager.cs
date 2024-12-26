using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class panelManager : MonoBehaviour
{
    public GameObject mainPanel; // Assign Main Panel in Inspector
    public GameObject[] layerPanels; // Assign all Layer Panels in Inspector

    // Show the main panel and hide all layer panels
    public void ShowMainPanel()
    {
        mainPanel.SetActive(true);

        foreach (GameObject panel in layerPanels)
        {
            panel.SetActive(false);
        }
    }

    // Show a specific layer panel and hide the main panel
    public void ShowLayerPanel(GameObject layerPanel)
    {
        mainPanel.SetActive(false);

        foreach (GameObject panel in layerPanels)
        {
            panel.SetActive(false);
        }

        layerPanel.SetActive(true);
    }
}
