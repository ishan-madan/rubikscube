using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManage : MonoBehaviour
{
    public void PlayButton() {
        SceneManager.LoadScene("ControlScene");
    }

    public void ControlsButton() {
        SceneManager.LoadScene("Rubik's Cube");
    }
}
