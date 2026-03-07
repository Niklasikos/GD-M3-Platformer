using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public float totalCoins;
    public float totalExtraJumps;
    public float level1;
    public float level2;
    public float level3;
    public float level4;
    public float[] data;
    public string fileName = "data.sav";

    void Start()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
        data = new float[] {totalCoins, totalExtraJumps, level1, level2, level3, level4};

        ResetData();

        bool FileExists = File.Exists(fileName);
        if (File.Exists(fileName))
        {
            data = System.Array.ConvertAll(File.ReadAllLines(fileName), float.Parse);

            totalCoins = data[0];
            totalExtraJumps = data[1];
            level1 = data[2];
            level2 = data[3];
            level3 = data[4];
            level4 = data[5];
        }
        else
        {
            File.WriteAllLines(fileName, System.Array.ConvertAll(data, f => f.ToString()));
        }
        SceneManager.LoadScene("MainMenu");
    }

    public void UpdateFiles()
    {
        data[0] = totalCoins;
        data[1] = totalExtraJumps;
        data[2] = level1;
        data[3] = level2;
        data[4] = level3;
        data[5] = level4; 
        File.WriteAllLines(fileName, System.Array.ConvertAll(data, f => f.ToString()));
    }

    public void ResetData()
    {
        totalCoins = 0;
        totalExtraJumps = 1;
        level1 = 0;
        level2 = 0;
        level3 = 0;
        level4 = 0;

        data[0] = totalCoins;
        data[1] = totalExtraJumps;
        data[2] = level1;
        data[3] = level2;
        data[4] = level3;
        data[5] = level4; 
    }

    void Update()
    {
        if(Input.GetKey(KeyCode.P))
        {
            Debug.Log(totalCoins);
        }
    }
}
