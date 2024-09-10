using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    public float zoomInSize = 8f; 
    public float zoomOutSize = 12f;
    public float zoomSpeed = 2f;
    private float targetSize;

    void Start(){
        targetSize = Camera.main.orthographicSize;
    }
    void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Player")){
            targetSize = zoomOutSize;
        }
    }

    void OnCollisionExit2D(Collision2D other) {
        if(other.gameObject.CompareTag("Player")){
            targetSize = zoomInSize;
        }
    }
    void Update(){
        Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, targetSize, zoomSpeed * Time.deltaTime);
    }

}