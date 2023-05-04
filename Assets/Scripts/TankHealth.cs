using UnityEngine;

public class TankHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    
    private void Start()
    {
        currentHealth = maxHealth;
    }


    public void TakeDamage(int damageAmount, int attackerIndex)
    {
        if (attackerIndex != GameManager.instance.currentPlayerIndex)
        {
            currentHealth -= damageAmount;
            if (currentHealth <= 0)
            {
                GameManager.instance.EndGame(GameManager.instance.currentPlayerIndex);
            }
        }
        else
        {
            currentHealth -= damageAmount;
            if (currentHealth <= 0)
            {
                Die();
            }
        }
    }


    private void Die()
    {
        
        Destroy(gameObject);
    }
}
