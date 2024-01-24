using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GemPopUpSingle : MonoBehaviour
{
    public Image iconHolder;
    public TextMeshProUGUI nameHolder;
    public TextMeshProUGUI countHolder;
    public GemInfo myGemInfo;

    public void Initialize(GemInfo gemInfo)
    {
        myGemInfo = gemInfo;
        iconHolder.sprite = gemInfo.icon;
        nameHolder.text = gemInfo.gemName;
        countHolder.text = 0.ToString();
    }

    public void UpdateMe(int count)
    {
        this.countHolder.text = count.ToString();

    }
}