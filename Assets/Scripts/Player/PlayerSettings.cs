using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerSettings : MonoBehaviour
{

    public Image image;
    public Player player;
    public TextMeshProUGUI lblPlayerName;
    public TMP_InputField inputPlayerName;
    public TMP_InputField redInput;
    public TMP_InputField greenInput;
    public TMP_InputField blueInput;
    
    void Start()
    {
        lblPlayerName.text = player.screenName;
    }

    private void ChangeColor(Color color) {
        image.color = color;
        player.gameObject.GetComponent<Image>().color = color;
    }

    public void GreenPaddle() {
        ChangeColor(Color.green);
    }

    public void RedPaddle() {
        ChangeColor(Color.red);
    }

    public void BluePaddle() {
        ChangeColor(Color.blue);
    }

    public void RGBColor() {
        float r = ParseInput(redInput.text);
        float g = ParseInput(greenInput.text);
        float b = ParseInput(blueInput.text);

        Color color = new Color(r,g,b);
        ChangeColor(color);
    }

    public float ParseInput(string text) {
        float res;
        if(!float.TryParse(text, out res)) return 0;
        if (res < 0) return 0;
        if (res > 255) return 255;
        return res;
    }

    public void ClearColorInputs() {
        redInput.text = "R";    
        greenInput.text = "G";
        blueInput.text = "B";
    }

    public void ChangePlayerName() {
        player.screenName = inputPlayerName.text;
        lblPlayerName.text = player.screenName;
        inputPlayerName.text = "";
    }

    public void CancelEdit() {
        inputPlayerName.text = "";
    }
}
