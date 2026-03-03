using UnityEngine;

public class LevelSelectManager : MonoBehaviour
{
    public GameObject Door1;
    public GameObject Door2;
    public GameObject Door3;

    private bool Door1Unlocked;
    private bool Door2Unlocked;
    private bool Door3Unlocked;
    void Start()
    {
        Door1.SetActive(false);
        Door2.SetActive(false); 
        Door3.SetActive(false);

        LoadBools();
    }

    void Update()
    {
        
    }

    public void LoadBools()
    {
        
    }
}
