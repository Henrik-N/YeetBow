using UnityEngine;

public class BallHealthComp : MonoBehaviour
{
    
    [SerializeField, Range(1f, 100f)] private int maxHealth = 20;
    [SerializeField] private int currentHealth;
    private int startHealth;
    private Color startColor;
    
    private SpriteRenderer rend;
    
    // Start is called before the first frame update
    private void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        currentHealth = maxHealth;
        startHealth = maxHealth;
        startColor = rend.color;
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            currentHealth = startHealth;
            rend.color = startColor;
        }
    }
    
    public void TakeDamage(int dmg)
    {
        currentHealth -= dmg;
        Debug.Log("lmao dmg");
        rend.color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        if (currentHealth <= 0)
        {
            rend.color = Color.black;
            GameLoop.Instance.GameOver();
        }
    }
}
