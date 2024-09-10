using UnityEngine;
using UnityEngine.UI;

public class EnemyHeathBar : MonoBehaviour{

    public Slider slider;
    public Color Low;
    public Color High;
    public Vector3 offset;

    public void SetHeath(float heath, float maxHealth){
        slider.gameObject.SetActive(heath<maxHealth);
        slider.value = heath;
        slider.maxValue = maxHealth;
        slider.fillRect.GetComponentInChildren<Image>().color = Color.Lerp(Low, High,slider.normalizedValue);
    }
    
    void Update(){
        slider.transform.position = Camera.main.WorldToScreenPoint(transform.parent.position + offset);
    }

}
