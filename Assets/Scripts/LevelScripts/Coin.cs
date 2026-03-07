using UnityEngine;

public class Coin : MonoBehaviour
{
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Player player = collision.gameObject.GetComponent<Player>(); 
            player.coins += 1;
            AudioSource.PlayClipAtPoint(audioSource.clip, transform.position);
            Destroy(gameObject);
            player.UpdateCoinCounter();
        }
    }
}
