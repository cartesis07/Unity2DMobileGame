using UnityEngine;

public class SensitivityPlus : MonoBehaviour
{
    public Player_Movement playerMovement;

    // Start is called before the first frame update
    void Awake()
    {
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Movement>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (playerMovement.sensitivity <= 3.0f)
            {
                playerMovement.sensitivity += 0.2f;
            }
        }
    }
}
