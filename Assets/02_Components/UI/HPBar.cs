using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    [SerializeField] private Image fillImage;

    RectTransform rectT;

    void Awake()
    {
        rectT = GetComponent<RectTransform>();
    }

    void onPlayerHPChanged(float currentHP, float maxHP)
    {
        fillImage.fillAmount = currentHP / maxHP;
    }

    void onPlayerPosChanged(Vector3 worldPos)
    {
        rectT.position = Camera.main.WorldToScreenPoint(worldPos);
    }



    void OnEnable()
    {
        EventManager.onPlayerHPChanged += onPlayerHPChanged;
        EventManager.onPlayerPosChanged += onPlayerPosChanged;
    }

    void OnDisable()
    {
        EventManager.onPlayerHPChanged -= onPlayerHPChanged;
        EventManager.onPlayerPosChanged -= onPlayerPosChanged;
    }

}
