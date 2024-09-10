using UnityEngine;

public class PickUpItem : MonoBehaviour{
    public float scaleMultiplier = 0.25f;
    public float speed = 1.0f;
    public GameObject arrow;
    public Transform arrowPosition;
    private Vector3 startScale;
    private GameObject currentArrow;

    void Start(){
        startScale = transform.localScale;
        currentArrow = Instantiate(arrow,arrowPosition.position, arrowPosition.rotation);
        currentArrow.transform.SetParent(arrowPosition);
    }
    public void SwitchEnableArrow(bool active){
        currentArrow.SetActive(active);
    }
    public Vector3 getStartScale(){
        return startScale;
    }

    void Update(){
        float scaleChange = Mathf.Sin(Time.time * speed) * 0.5f + 0.5f;
        float finalScale = 1 + scaleChange * scaleMultiplier;
        transform.localScale = new Vector3(startScale.x * finalScale, startScale.y * finalScale, startScale.z * finalScale);
    }
}
