using UnityEngine;
using TMPro;
using DG.Tweening;

public class HUDController : MonoBehaviour
{
    //References
    [SerializeField] TextMeshProUGUI diamondText;
    [SerializeField] Transform diamondImage;
    //Prefabs
    public GameObject DiamondIcon;
    public GameObject CollectText;

    int diamondCounter;
    private void Awake()
    {
        diamondCounter = 0;
        diamondText.text = "0";
    }

    // Start is called before the first frame update
    void Start()
    {
        Subscribe();
    }

    //Events
    void Subscribe()
    {
        EventManager.Instance.OnCollectDiamond += DiamondCollected;
        EventManager.Instance.OnCollectPile += SpawnCollectText;
    }

    void DiamondCollected(Vector3 position)
    {
        diamondCounter++;
        diamondText.text = diamondCounter.ToString();
        SpawnIcon(position);
    }

    //Spawning
    void SpawnIcon(Vector3 position)
    {
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(position);
        GameObject go = Instantiate(DiamondIcon, screenPosition, Quaternion.identity, transform);
        go.transform.DOMove(diamondImage.position, 1, true).SetEase(Ease.InOutCubic);
        Destroy(go, 1);
    }

    void SpawnCollectText(Vector3 position, int value)
    {
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(position);
        GameObject go = Instantiate(CollectText, screenPosition, Quaternion.identity, transform);
        go.GetComponentInChildren<TextMeshProUGUI>().text = "+" + value.ToString();
        Vector3 targetPos = go.transform.position;
        targetPos.y += 150;
        go.transform.DOMove(targetPos, 1, true).SetEase(Ease.OutCubic);
        Destroy(go, 1);
    }
}
