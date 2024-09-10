using UnityEngine;
using UnityEngine.UI;

public class ButtonHighlightHandler : MonoBehaviour{
    private Button button;
    private bool isCharacterOverButton = false;
    private Color normalColor;
    private Color highlightedColor;

    void Start(){
        button = GetComponent<Button>();
        normalColor = button.colors.normalColor;
        highlightedColor = button.colors.highlightedColor;
    }

    void Update(){
        if (isCharacterOverButton && Input.GetKeyDown(KeyCode.Space)){
            button.onClick.Invoke();
        }
    }

    private void OnTriggerEnter2D(Collider2D other){
        if (other.CompareTag("Player")){
            isCharacterOverButton = true;
            HighlightButton();
        }
    }

    private void OnTriggerExit2D(Collider2D other){
        if (other.CompareTag("Player")){
            isCharacterOverButton = false;
            UnhighlightButton();
        }
    }

    private void HighlightButton(){
        var colors = button.colors;
        colors.normalColor = highlightedColor;
        button.colors = colors;
    }

    private void UnhighlightButton()
    {
        var colors = button.colors;
        colors.normalColor = normalColor;
        button.colors = colors;
    }
    
}
