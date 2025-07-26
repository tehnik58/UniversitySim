using UnityEngine;

public abstract class InteractableObject : MonoBehaviour
{
    [SerializeField] private Outline outline; // опционально - подсветка при наведении
    
    protected virtual void Awake()
    {
        if (outline != null) outline.enabled = false;
    }
    
    public virtual void OnMouseEnterCustom()
    {
        if (outline != null) outline.enabled = true;
        // Дополнительные эффекты при наведении
    }
    public virtual void OnMouseEnterCustom(GameObject _build)
    {
        if (outline != null) outline.enabled = true;
        // Дополнительные эффекты при наведении
    }
    
    public virtual void OnMouseExitCustom()
    {
        if (outline != null) outline.enabled = false;
    }
    
    public abstract void OnMouseDownCustom(); // Основной метод взаимодействия
}