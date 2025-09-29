using UnityEngine;

public class Player : MonoBehaviour
{
    public float jumpForce = 10f;
    public Rigidbody2D rb;
    public SpriteRenderer sr;
    public Color colorCyan;
    public Color colorYellow;
    public Color colorMagenta;
    public Color colorPink;
    public string currentColor;

    void Start()
    {
        SetRandomColor();
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump") || Input.GetMouseButtonDown(0))
        {
            rb.velocity = Vector2.up * jumpForce;
            if (SoundManager.instance != null)
                SoundManager.instance.PlayJump();
            else
                Debug.Log("Invalid Sound Manager");
        }
    }

    void OnTriggerEnter2D(Collider2D c)
    {
        if (c.tag == "ColorChanger")
        {
            ScoreManager.instance.AddScore();
            Spawner.instance.SpawnNextStar(c.transform.position);
            Destroy(c.gameObject);
            Spawner.instance.SpawnNextObstacle();
            SetRandomColor();

            if (SoundManager.instance != null)
                SoundManager.instance.PlayGetStar();
            else
                Debug.Log("Invalid Sound Manager");

            return;
        }

        if (c.tag != currentColor)
        {
            if (GameController.instance == null)
            {
                Debug.Log("Invalid Game Controller Instance");
                return;
            }

            GameController.instance.GameOver();
        }
    }

    void SetRandomColor()
    {
        int ind = Random.Range(0, 4);

        switch (ind)
        {
            case 0:
                currentColor = "Cyan";
                sr.color = colorCyan;
                break;
            case 1:
                currentColor = "Yellow";
                sr.color = colorYellow;
                break;
            case 2:
                currentColor = "Magenta";
                sr.color = colorMagenta;
                break;
            case 3:
                currentColor = "Pink";
                sr.color = colorPink;
                break;
        }
    }
}
