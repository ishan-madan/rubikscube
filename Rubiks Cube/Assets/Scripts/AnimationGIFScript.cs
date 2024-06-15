using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimationGIFScript : MonoBehaviour
{

    public Material[] animatedImages;
    public MeshRenderer background;
    public Image backgroundImage;
    public Sprite[] backgroundImages;

    // Update is called once per frame
    void Update()
    {
        if (background != null){
            background.material = animatedImages[(int)(Time.time * 10)%animatedImages.Length];
        }
        else if (backgroundImage != null){
            backgroundImage.sprite = backgroundImages[(int)(Time.time * 10)%backgroundImages.Length];
        }
        
    }
}
