using UnityEngine;

public abstract class PoweUpEffect : ScriptableObject{
    public Sprite image;
    public abstract void Apply();
}
