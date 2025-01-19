using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public float moveSpeed = 3f; // Sebesség, amivel a Health Pickup balra mozog

    void Update()
    {
        // Mozgás balra folyamatosan
        transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
    }

    public void SetSpeed(float speed)
    {
        moveSpeed = speed; // Beállítja a Tie Fighter sebességéhez
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Health Pickup ütközött ezzel: " + collision.gameObject.name);

        if (collision.CompareTag("Player"))
        {
            Debug.Log("Játékos felvette a Health Pickupot!");

            PlayerLives playerLives = collision.GetComponent<PlayerLives>();
            if (playerLives != null && playerLives.lives < playerLives.maxLives)
            {
                playerLives.lives += 1;
                playerLives.UpdateLivesUI();
            }

            Destroy(gameObject);
        }
    }
}
