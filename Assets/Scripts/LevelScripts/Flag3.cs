using UnityEngine;

public class Flag3 : MonoBehaviour
{
    public GameObject winUI;
    public bool win = false;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if(win == false)
            {
                Time.timeScale = 0;
                winUI.SetActive(true); 
                GameManager.Instance.totalCoins = GameManager.Instance.totalCoins + Player.Instance.coins;
                GameManager.Instance.level4 = 1;
                GameManager.Instance.UpdateFiles();
                win = true;
            }
            
        }
    }
}
