using UnityEngine;
using UnityEngine.SceneManagement;

public class Door2 : MonoBehaviour
{
    [SerializeField] private GameObject E;
    public float amplitude = 2f;
    public float speed = 1f;
    Vector3 startPos;
    private void Start()
    {
        E.SetActive(false);
        startPos = E.transform.position;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            E.SetActive(true);
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (Input.GetKey(KeyCode.E))
            {
                SceneManager.LoadScene("Level2");
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            E.SetActive(false);
        }
    }

    private void Update()
    {
        float y = Mathf.Sin(Time.time * speed) * amplitude;
        E.transform.position = startPos + new Vector3(0f, y, 0f);
    }
}
