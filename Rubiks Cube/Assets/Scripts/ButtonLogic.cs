using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonLogic : MonoBehaviour
{

    Automate automate;
    public Button shuffleButton;
    public Button solveButton;
    ColorBlock buttonColorActive;
    // ColorBlock buttonColorInactive;
    ColorBlock buttonColorDisabled;

    // Start is called before the first frame update
    void Start()
    {
        automate = FindObjectOfType<Automate>();

        // set the color block values for buttonColorActive to be the same as the normal values
        buttonColorActive = shuffleButton.colors;

        // set the color block values for buttonColorInactive to be purple no matter what
        // buttonColorInactive.normalColor = new Color(0.7176f, 0.2156f, 0.9686f, 1);
        // buttonColorInactive.highlightedColor = new Color(0.7176f, 0.2156f, 0.9686f, 1);
        // buttonColorInactive.pressedColor = new Color(0.7176f, 0.2156f, 0.9686f, 1);
        // buttonColorInactive.selectedColor = new Color(0.7176f, 0.2156f, 0.9686f, 1);
        // buttonColorInactive.disabledColor = new Color(0.7176f, 0.2156f, 0.9686f, 1);
        // buttonColorInactive.colorMultiplier = 1;
        // buttonColorInactive.fadeDuration = 0.3f;

        // set the color block values for buttonColorDisabled to be gray no matter what
        buttonColorDisabled.normalColor = new Color(0.27f, 0.27f, 0.27f, 1);
        buttonColorDisabled.highlightedColor = new Color(0.27f, 0.27f, 0.27f, 1);
        buttonColorDisabled.pressedColor = new Color(0.27f, 0.27f, 0.27f, 1);
        buttonColorDisabled.selectedColor = new Color(0.27f, 0.27f, 0.27f, 1);
        buttonColorDisabled.disabledColor = new Color(0.27f, 0.27f, 0.27f, 1);
        buttonColorDisabled.colorMultiplier = 1;
        buttonColorDisabled.fadeDuration = 0.3f;
    }

    // Update is called once per frame
    void Update()
    {
        if(automate.shuffling){
            shuffleButton.colors = buttonColorDisabled;
            solveButton.colors = buttonColorDisabled;

            shuffleButton.interactable = false;
            solveButton.interactable = false;
        }
        else {
            shuffleButton.colors = buttonColorActive;
            solveButton.colors = buttonColorActive;
            shuffleButton.interactable = true;
            solveButton.interactable = true;
        }
    }

}
