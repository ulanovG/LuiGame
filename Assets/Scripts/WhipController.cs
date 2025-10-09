using UnityEngine;

public class WhipController : MonoBehaviour
{
    public PlayerController playerController;
    public Transform whipStart;
    private Vector3 target;
    private bool playerPull = false;
    private bool whip = false;
    private bool back = false;
    private bool pulling = false;
    public float whipSpeed = 10f;
    public float whipSpeedBack = 20f;
    private GameObject pulledEnemy;
    private GameObject targetEnemy;
    private bool drawWhip = false;
    public LineRenderer whipLine;

    void Update()
    {
        if (whip)
        {
            var offset = (targetEnemy != null ? targetEnemy.transform.position : target) - transform.position;

            if (offset.magnitude > 0.2f)
            {
                transform.Translate(offset.normalized * whipSpeed * Time.deltaTime);
            }
            else
            {
                whip = false;
                back = true;
            }
        }
        if (pulling)
        {
            transform.position = pulledEnemy.transform.position;
        }
        if (back)
        {
            var offset = whipStart.position - transform.position;

            if (offset.magnitude > 0.2f)
            {
                transform.Translate(offset.normalized * whipSpeedBack * Time.deltaTime);
            }
            else
            {
                back = false;
                drawWhip = false;
                playerController.WhipReturned();
            }
        }

        DrawWhip();
    }

    void DrawWhip()
    {
        whipLine.gameObject.SetActive(drawWhip);
        if(drawWhip)
        {
            whipLine.SetPosition(1, transform.localPosition);
        }
    }

    public void Whip(Vector3 targetPoint, bool pull, GameObject targetEn)
    {
        target = targetPoint;
        playerPull = pull;
        targetEnemy = targetEn;
        whip = true;
        drawWhip = true;
    }
    public void PullOver()
    {
        pulling = false;
        back = true;
        targetEnemy = null;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            var enemyController = other.GetComponent<EnemyController>();
            
            if(playerPull)
            {
                enemyController.GetPulled();
            }
            else
            {
                playerController.GetPulled(other.gameObject);
                enemyController.GetPulling();
            }

            pulledEnemy = enemyController.GetPullPoint();
            whip = false;
            pulling = true;
        }
        else if (other.gameObject.layer == LayerMask.NameToLayer("Borders"))
        {
            whip = false;
            back = true;
        }
        
    }
}
