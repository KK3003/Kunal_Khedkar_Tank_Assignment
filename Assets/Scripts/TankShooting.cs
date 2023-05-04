using UnityEngine;
using UnityEngine.UI;

public class TankShooting : MonoBehaviour
{
    public Rigidbody projectilePrefab;
    public Transform projectileSpawnPoint;

    public float powerMin = 10f;
    public float powerMax = 30f;
    public float angleMin = 30f;
    public float angleMax = 60f;
    public float gravity = -9.8f;

    private float currentPower;
    private float currentAngle;
    private bool aiming;
    private bool fired;

    // UI
    public Text angletext;
    public Text powertext;
    public LineRenderer player1LineRenderer;
    public LineRenderer player2LineRenderer;
    public Transform tank1;
    public Transform tank2;

    private void Start()
    {
        aiming = true;
        fired = false;
        currentPower = powerMin;
        currentAngle = angleMin;
    }


    private void Update()
    {
        if (aiming)
        {
                // Update power and angle with arrow keys or touch input
                float horizontal = Input.GetAxis("Horizontal");
                float vertical = Input.GetAxis("Vertical");
                if (horizontal != 0f)
                {
                    currentAngle += horizontal;
                    currentAngle = Mathf.Clamp(currentAngle, angleMin, angleMax);
                    angletext.text = currentAngle.ToString();

                    // Update line for the current player
                    LineRenderer lineRenderer = GetLineRenderer();
                    if (lineRenderer != null && GameManager.instance.currentPlayerIndex == 1)
                    {
                        lineRenderer.SetPosition(0, GameManager.instance.tank1.transform.position);
                        Vector3 angleVector = Quaternion.Euler(currentAngle, 0f, 0f) * Vector3.forward;
                        lineRenderer.SetPosition(1, GameManager.instance.tank1.transform.position + angleVector * 2f);
                    }
                    else if (lineRenderer != null && GameManager.instance.currentPlayerIndex == 2)
                    {
                    lineRenderer.SetPosition(0, GameManager.instance.tank2.transform.position);
                    Vector3 angleVector = Quaternion.Euler(currentAngle, 0f, 0f) * Vector3.forward;
                    lineRenderer.SetPosition(1, GameManager.instance.tank2.transform.position + angleVector * 2f);
                    }
                }
                    if (vertical != 0f)
                    {
                        currentPower += vertical;
                        currentPower = Mathf.Clamp(currentPower, powerMin, powerMax);
                        powertext.text = currentPower.ToString();
                    } 
                 }

            if (fired && projectilePrefab.velocity.magnitude == 0f)
            {
                aiming = true;
                fired = false;
                GameManager.instance.NextPlayer();
            }
    }

    private LineRenderer GetLineRenderer()
    {
        int playerNumber = GameManager.instance.currentPlayerIndex;
        if (playerNumber == 1)
        {
            return player1LineRenderer;
        }
        else if (playerNumber == 2)
        {
            return player2LineRenderer;
        }
        else
        {
            return null;
        }
    }


    public void Fire()
    {
            if (aiming && !fired)
            {
                // Fire projectile with current power and angle
                Rigidbody projectileInstance = Instantiate(projectilePrefab, projectileSpawnPoint.position, Quaternion.identity);
                projectileInstance.velocity = Quaternion.Euler(currentAngle, 0f, 0f) * Vector3.forward * currentPower;
                projectileInstance.useGravity = true;
                fired = true;

                aiming = false;

                // Determine which tank was hit and reduce its health
                RaycastHit hit;
                if (Physics.Raycast(projectileSpawnPoint.position, projectileInstance.velocity, out hit))
                {
                    TankHealth hitTankHealth = hit.transform.GetComponent<TankHealth>();
                    if (hitTankHealth != null)
                    {
                        hitTankHealth.TakeDamage(20, GameManager.instance.currentPlayerIndex);
                    }
                }
            }
    }
}


