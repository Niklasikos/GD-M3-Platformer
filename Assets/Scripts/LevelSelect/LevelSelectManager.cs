using UnityEngine;

public class LevelSelectManager : MonoBehaviour
{
    public GameObject Door1;
    public GameObject Door2;
    public GameObject Door3;
    public GameObject Door4;

    private bool Door1Unlocked;
    private bool Door2Unlocked;
    private bool Door3Unlocked;
    private bool Door4Unlocked;
    void Start()
    {
        Door1.SetActive(false);
        Door2.SetActive(false); 
        Door3.SetActive(false);
        Door4.SetActive(false);
        
        if(GameManager.Instance.data[2] == 1)
        {
            Door1.SetActive(true);
        }
        if(GameManager.Instance.data[3] == 1)
        {
            Door2.SetActive(true);
        }
        if(GameManager.Instance.data[4] == 1)
        {
            Door3.SetActive(true);
        }
        if(GameManager.Instance.data[5] == 1)
        {
            Door4.SetActive(true);
        }
    }

    void Update()
    {
        
    }
}
