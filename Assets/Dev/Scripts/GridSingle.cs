using UnityEngine;

public class GridSingle : MonoBehaviour
{
    
    private float curTime;
    [SerializeField]
    private GemSingle gemPrefab;

    public bool canSpawn;

    private void Start() => SpawnGem();

    private void Update()
    {
        if (canSpawn is false)
            return;

        curTime += Time.deltaTime;
        if (curTime >= GemManager.instance.gemSpawnTime)
        {
            curTime = 0;
            SpawnGem();
        }
    }

    public void ChangeMyColor(Color color)
    {
        GetComponentInChildren<MeshRenderer>().material.color = color;
    }

    private void SpawnGem()
    {
        canSpawn = false;
        GemSingle gem = Instantiate(gemPrefab, transform);
        gem.transform.localPosition = Vector3.zero;

        gem.OnGemAddedToStack += Gem_OnStackableAddedToStack;
        gem.OnGemAddedToStack += GemManager.instance.OnGemCollected;

        gem.OnGemSpawned();
    }

    private void Gem_OnStackableAddedToStack(GemSingle gem)
    {
        canSpawn = true;
    }
}