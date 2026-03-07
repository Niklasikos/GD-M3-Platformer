using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class Shop : MonoBehaviour
{
    [SerializeField] private GameObject E;
    public GameObject ShopUI;
    public float amplitude = 2f;
    public float speed = 1f;
    public bool inTrigger;
    public TMP_Text text;
    public TMP_Text textTriplejump;
    public bool tripleJumpBought;
    Vector3 startPos;
    public float triplejumpValue = 20f;
    public bool notInShop = true;
    private void Start()
    {
        E.SetActive(false);
        ShopUI.SetActive(false);
        startPos = E.transform.position;
        tripleJumpBought = false;
        if(GameManager.Instance.data[1] == 2)
        {
            tripleJumpBought = true;
            textTriplejump.text = "Sold Out";
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            E.SetActive(true);
            inTrigger = true;
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            inTrigger = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            E.SetActive(false);
            inTrigger = false;
        }
    }

    private void Update()
    {
        float y = Mathf.Sin(Time.time * speed) * amplitude;
        E.transform.position = startPos + new Vector3(0f, y, 0f);

        if(inTrigger == true)
        {
            if(Time.timeScale == 1 && Input.GetKeyDown(KeyCode.E))
            {
                Time.timeScale = 0;
                OpenShop();
            }
            if(Time.timeScale == 0 && Input.GetKeyDown(KeyCode.Escape))
            {
                Time.timeScale = 1;
                CloseShop();
            }
        }

    }

    public void OpenShop()
    {
        ShopUI.SetActive(true);
        text.text = ": " + GameManager.Instance.data[0];
        notInShop = false;
    }

    public void CloseShop()
    {
        ShopUI.SetActive(false);
        notInShop = true;
    }

    public void TripleJump()
    {
        if(tripleJumpBought == false)
        {
            if (GameManager.Instance.totalCoins >= triplejumpValue)
            {
                GameManager.Instance.totalCoins -= triplejumpValue;
                GameManager.Instance.totalExtraJumps++;
                GameManager.Instance.UpdateFiles();                
                tripleJumpBought = true;
                PlayerLevelSelect.Instance.extraJumpsValue++;
                textTriplejump.text = "Sold Out";
                text.text = ": " + GameManager.Instance.data[0];
            }
        }
        
    }
}
