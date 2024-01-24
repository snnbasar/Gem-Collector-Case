using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public GoldArea goldArea;
    public GemPupUp gemPopupArea;

    private void Awake() => instance = this;


    public void SetTotalGold(int gold)
    {
        goldArea.totalGoldText.text = KMBMaker(gold);
        AnimateScaleOfAThing(goldArea.totalGoldText.rectTransform);
    }


    public void AnimateScaleOfAThing(RectTransform obje)
    {
        if (DOTween.IsTweening(obje))
            return;
        float time = 0.25f;
        float scaleMultiplier = 1.1f;
        Vector3 scale = Vector3.one;
        Sequence moneyScaleAnimSeq = DOTween.Sequence();
        moneyScaleAnimSeq.Append(obje.DOScale(scale * scaleMultiplier, time / 2));
        moneyScaleAnimSeq.Append(obje.DOScale(scale, time / 2));
    }


    public GemPopUpSingle CreateGemPopUp(GemInfo gemInfo)
    {
        GemPopUpSingle gemPopup = Instantiate(gemPopupArea.prefab, gemPopupArea.parent);
        gemPopupArea.gemPopUps.Add(gemPopup);

        gemPopup.Initialize(gemInfo);

        return gemPopup;
    }

    public void UpdateGemPopUp(GemPopUpSingle gemPopupSingle, int count)
    {
        gemPopupSingle.UpdateMe(count);
    }
    public void UpdateGemPopUp()
    {
        foreach (var collectedGem in GemManager.instance.collectedGems)
        {
            if (String.IsNullOrEmpty(collectedGem.gemInfo?.gemName))
                continue;
            GemPopUpSingle gemPopUpSingle = GetGemPopUpSingle(collectedGem.gemInfo);
            if(gemPopUpSingle == null)
            {
                gemPopUpSingle = CreateGemPopUp(collectedGem.gemInfo);
            }
            UpdateGemPopUp(gemPopUpSingle, collectedGem.count);
        }
    }
    public GemPopUpSingle GetGemPopUpSingle(GemInfo gemInfo) => gemPopupArea.gemPopUps.Where(x => gemInfo.gemName.Equals(x.myGemInfo?.gemName)).FirstOrDefault();
    public List<GemPopUpSingle> GetPopUps() => gemPopupArea.gemPopUps;


    public void PopUpMenuButton()
    {
        gemPopupArea.menu.SetActive(!gemPopupArea.menu.activeSelf);
    }

    public static string KMBMaker(double num)
    {
        double numStr;
        string suffix;
        string text;
        if (num < 1000d)
        {
            numStr = num;
            text = numStr.ToString();
            suffix = "";
        }
        else if (num < 1000000d)
        {
            numStr = num / 1000d;
            text = num % 1000d != 0 ? numStr.ToString("0.0") : numStr.ToString("0");
            suffix = "K";
        }
        else if (num < 1000000000d)
        {
            numStr = num / 1000000d;
            text = num % 1000000d != 0 ? numStr.ToString("0.0") : numStr.ToString("0");
            suffix = "M";
        }
        else
        {
            numStr = num / 1000000000d;
            text = num % 1000000000d != 0 ? numStr.ToString("0.0") : numStr.ToString("0");
            suffix = "B";
        }
        return text.ToString() + suffix;
    }






    [System.Serializable]
    public class GoldArea
    {
        public RectTransform goldArea;
        public TextMeshProUGUI totalGoldText;
    }
    [System.Serializable]
    public class GemPupUp
    {
        public GemPopUpSingle prefab;
        public RectTransform parent;
        public GameObject menu;
        public List<GemPopUpSingle> gemPopUps = new List<GemPopUpSingle>();
    }
}
