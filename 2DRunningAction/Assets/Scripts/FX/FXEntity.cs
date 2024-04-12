using UnityEngine;

public class FXEntity : MonoBehaviour
{
    private SpriteRenderer sr;
    private float originalTimeScale = 1.0f;

    [Header("Flash FX")]
    [SerializeField] private float flashDuration;
    [SerializeField] private Material hitMat;
    private Material originMat;

    void Start()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
    }

    private void GrayColorBlink()
    {
        if (sr.color != Color.white)
            sr.color = Color.white;
        else
            sr.color = Color.gray;
    }

    public void CancelColorChange()
    {
        CancelInvoke();
        sr.color = Color.white;
    }
}
