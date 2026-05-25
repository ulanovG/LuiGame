using System.Xml.Serialization;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Rendering;

public class EnemyController : MonoBehaviour
{
    private Vector3 movement;
    private Vector3 lastMovement;

    public GameObject target;
    public CharacterController controller;
    public Animator animator;
    public float deathAnimationLength = 3f;
    public float speed = 1.5f;
    public float bordersX = 12.5f;
    public float houseAngle = 20f;
    private Vector3 leftAngled;
    private Vector3 rightAngled;

    private bool freeze = false;
    private float stunTimer = 0f;
    private bool stunned = false;
    public ParticleSystem hitEffect;
    public int maxHealth = 10;
    private int currentHealth;
    public float stunDuration = 0.3f;

    private bool pulled = false;
    private bool pulling = false;
    public float pulledSpeed = 3f;
    public GameObject pullPoint;
    public GameObject player;
    public WhipController whipController;
    public float pullStunDuration = 0.2f;

    void Start()
    {
        currentHealth = maxHealth;
        leftAngled = new Vector3(math.sin(-houseAngle), 0, math.cos(-houseAngle));
        rightAngled = new Vector3(math.sin(houseAngle), 0, math.cos(houseAngle));
    }

    void Update()
    {
        if (stunTimer <= 0 && !pulled && !pulling && !freeze)
        {
            if (transform.position.x >= bordersX)
            {
                movement = leftAngled;
                controller.Move(movement * speed * Time.deltaTime);
            }
            else if (transform.position.x <= -bordersX)
            {
                movement = rightAngled;
                controller.Move(movement * speed * Time.deltaTime);
            }
            else
            {
                movement = target.transform.position - transform.position;

                if(movement.magnitude > 0.1f) 
                {
                    controller.Move(movement.normalized * speed * Time.deltaTime);
                }
            }
        }
        else if (pulled)
        {
            var offset = player.transform.position - transform.position;

            if (offset.magnitude > 0.1f)
            {
                controller.Move(offset.normalized * pulledSpeed * Time.deltaTime);
            }
        }

        if (movement.magnitude >= 0.1f && (movement.x != 0 || movement.z != 0))
        {
            lastMovement = movement;
        }

        if (stunTimer > 0)
        {
            stunTimer -= Time.deltaTime;
        }
        if (stunTimer < 0)
        {
            stunTimer = 0;
        }
        if (stunTimer == 0 && stunned)
        {
            stunned = false;
        }
        Animate();
    }

    void Animate()
    {
        animator.SetFloat("moveX", movement.x);
        animator.SetFloat("moveZ", movement.z);
        animator.SetFloat("moveMag", movement.magnitude);
        animator.SetFloat("lastMoveX", lastMovement.x);
        animator.SetFloat("lastMoveZ", lastMovement.z);
        animator.SetBool("pulled", pulled || pulling);
        animator.SetBool("stunned", stunned);
        animator.SetBool("dead", freeze);
    }

    public void GetPulled()
    {
        pulled = true;
        
    }
    public void GetPulling()
    {
        pulling = true;
    }
    public void PullingOver()
    {
        pulling = false;
        Stun(pullStunDuration);
    }
    private void StopPulled(ControllerColliderHit hit)
    {
        pulled = false;
        whipController.PullOver();
        Stun(pullStunDuration);
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(pulled && hit.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            StopPulled(hit);
        }
    }

    public GameObject GetPullPoint()
    {
        return pullPoint;
    }

    void OnTriggerEnter (Collider other) 
    {
        if(other.gameObject.tag == "attackCollider")
        {
            PlayerController.onPlayerAttack += TakeDamage;
        }
    }
    void OnTriggerExit (Collider other) 
    {
        if(other.gameObject.tag == "attackCollider")
        {
            PlayerController.onPlayerAttack -= TakeDamage;
        }
    }
    void OnDisable()
    {
        PlayerController.onPlayerAttack -= TakeDamage;
    }

    public void TakeDamage (int damage)
    {
        currentHealth -= damage;

        if(hitEffect != null)
        {
            hitEffect.Play(true);
        }

        if(currentHealth <= 0)
        {
            Die();
        }
        else
        {
            Stun(stunDuration);
        }
    }

    void Die()
    {
        freeze = true;
        PlayerController.onPlayerAttack -= TakeDamage;
        Destroy(gameObject, deathAnimationLength);
    }

    void Stun(float duration)
    {
        stunTimer = duration;
        stunned = true;
    }
}
