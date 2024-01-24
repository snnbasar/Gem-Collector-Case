using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private int _totalGold;
    public int totalGold { get => _totalGold; set
        {
            _totalGold = value;
            UIManager.instance.SetTotalGold(_totalGold);
            SaveManager.Save("totalGold", _totalGold);
        } }


    private void Awake() => instance = this;
    private void Start()
    {
        _totalGold = SaveManager.Load("totalGold", 0);
        UIManager.instance.SetTotalGold(totalGold);
    }


    public void IncreaseTotalGold(int amount)
    {
        totalGold += amount;
    }
}