using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GemManager : MonoBehaviour
{
    public static GemManager instance;
    public float gemSpawnTime = 1f;
    public GemInfo[] availableGems;
    [HideInInspector]public List<CollectedGem> collectedGems = new List<CollectedGem>();

    private void Awake() => instance = this;
    private IEnumerator Start()
    {
        yield return null;
        yield return null;
        LoadSave();
    }

    private void LoadSave()
    {
        Dictionary<string, int> countGemSave = SaveManager.LoadDictionary<string, int>("gemCounts");
        foreach (var gem in countGemSave)
        {

            collectedGems.Add(new CollectedGem(GetAvailableGem(gem.Key), gem.Value));
        }
        UIManager.instance.UpdateGemPopUp();
    }

    public GemInfo GetMeRandomGem() => availableGems[UnityEngine.Random.Range(0, availableGems.Length)];

    public void OnGemCollected(GemSingle gem)
    {
        GemInfo gemInfo = gem.myGemInfo;
        if (collectedGems.Any(x => gemInfo.gemName.Equals(x.gemInfo.gemName)))
        {
            CollectedGem colGem = GetCollectedGem(gemInfo);
            colGem.count += 1;
        }
        else
        {
            collectedGems.Add(new CollectedGem(gemInfo, 1));
        }
        UIManager.instance.UpdateGemPopUp();
    }

    private CollectedGem GetCollectedGem(GemInfo gemInfo)
    {
        return collectedGems.Where(x => gemInfo.gemName.Equals(x.gemInfo.gemName)).FirstOrDefault();
    }
    private GemInfo GetAvailableGem(string gemName)
    {
        return availableGems.Where(x => gemName.Equals(x.gemName)).FirstOrDefault();
    }

    public int CalculateGemPrice(GemInfo gemInfo, float myScale)
    {
        return (int)(gemInfo.startPrice + myScale * 100);
    }

}

[System.Serializable]
public class GemInfo
{
    public string gemName;
    public int startPrice;
    public Sprite icon;
    public GameObject gemPrefab;
}

[System.Serializable]
public class CollectedGem
{
    public GemInfo gemInfo;
    private int _count;
    public int count { get => _count; set
        {
            _count = value;
            if(gemInfo!= null)
                SaveManager.SaveToDictionary("gemCounts", gemInfo.gemName, _count);
        }
    }

    public CollectedGem(GemInfo gemInfo, int count)
    {
        this.gemInfo = gemInfo;
        this.count = count;
    }
}

