using System;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private bool isPaused = false;
    private Vector3 movement;
    private Vector3 lastMovement;
    private Vector3 lookingDirection;

    public CharacterController controller;
    public Animator animator;
    public Camera mainCamera;
    public float speed = 6f;
    public GameObject rotationPoint;
    public ParticleSystem attackVfx;
    public LayerMask layerMaskRoad;

    private bool canAttack = true;
    public int attackDamage = 10;
    public float attackCooldown = 2f;
    public AttackColliderController attackColliderController;
    public LayerMask layerMaskEnemy;

    private bool whiping = false;
    private bool pulled = false;
    private GameObject pullingEnemy;
    public LayerMask layerMaskRoadEnemy;
    public float whipMaxLength = 100f;
    public float pulledSpeed = 3f;
    public WhipController whipController;
    public Transform whipStart;

    void Start()
    {
        GameController.onGamePaused += GamePaused;
    }

    void Update()
    {
        if(!isPaused)
        {
            if (!whiping && !pulled && canAttack)
            {
                movement = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));

                if (movement.magnitude >= 0.1f)
                {
                    controller.Move(movement.normalized * speed * Time.deltaTime);
                }
            }
            else if (pulled)
            {
                movement = pullingEnemy.transform.position - transform.position;
                
                if (movement.magnitude >= 0.1f)
                {
                    controller.Move(movement.normalized * pulledSpeed * Time.deltaTime);
                }
            }

            if (movement.magnitude >= 0.1f && (movement.x != 0 || movement.z != 0))
            {
                lastMovement = movement;
            }


            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMaskRoad))
            {
                var targetPosition = hit.point;
                targetPosition.y = transform.position.y;
                lookingDirection = (targetPosition - transform.position).normalized;
                Quaternion targetRotation = Quaternion.LookRotation(lookingDirection);
                rotationPoint.transform.rotation = targetRotation;
                
                var mainPs = attackVfx.main;
                mainPs.startRotationZMultiplier = (targetRotation.eulerAngles.y + 90) * Mathf.Deg2Rad;
            }

            if ((Input.GetButtonDown("Fire1") || Input.GetButtonDown("Fire2")) && canAttack && !whiping)
            {
                if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMaskRoadEnemy))
                {
                    var targetPosition = hit.point;
                    GameObject targetEnemy = null;
                    bool pullingWhip = Input.GetButtonDown("Fire1");

                    if (hit.collider != null && hit.collider.gameObject.layer == LayerMask.NameToLayer("Enemy"))
                    {
                        targetEnemy = hit.collider.GetComponent<EnemyController>().GetPullPoint();
                    }

                    Whip(targetPosition, pullingWhip, targetEnemy);
                    LastMovementToLookingDirection();
                }
            }

            if (Input.GetButtonDown("Jump") && canAttack && !whiping)
            {            
                Attack();
                animator.SetTrigger("attack");
                LastMovementToLookingDirection();
            }

            Animate();
        }
    }

    void Animate()
    {
        animator.SetFloat("moveX", movement.x);
        animator.SetFloat("moveZ", movement.z);
        animator.SetFloat("moveMag", movement.magnitude);
        animator.SetFloat("lastMoveX", lastMovement.x);
        animator.SetFloat("lastMoveZ", lastMovement.z);
        if (!whiping && canAttack)
        {
            animator.SetFloat("lookX", Mathf.Round(lookingDirection.x));
            animator.SetFloat("lookZ", Mathf.Round(lookingDirection.z));
        }

        animator.SetBool("whipStart", whipController.Whipping);
        animator.SetBool("whipPull", whipController.WhipPulling);
        animator.SetBool("whipPullSelf", whipController.WhipPullingSelf);
    }

    void Whip(Vector3 targetPosition, bool pull, GameObject targetEnemy)
    {
        whiping = true;
        
        if(Vector3.Distance(targetPosition, whipStart.position) > whipMaxLength)
        {
            targetPosition = whipStart.position + ((targetPosition - whipStart.position).normalized * whipMaxLength);
            targetEnemy = null;
        }
        
        targetPosition.y = whipStart.position.y;
        whipController.Whip(targetPosition, pull, targetEnemy);
    }
    public void WhipReturned()
    {
        whiping = false;
    }

    public void GetPulled(GameObject enemy)
    {
        pullingEnemy = enemy;
        pulled = true;
    }
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(pulled)
        {
            pulled = false;
            whipController.PullOver();
            pullingEnemy.GetComponent<EnemyController>().PullingOver();
        }
    }

    void Attack()
    {
        var hitEnemies = attackColliderController.colliders;
        foreach (var enemy in hitEnemies)
        {
            if (enemy != null)
                enemy.GetComponent<EnemyController>().TakeDamage(attackDamage);
        }

        canAttack = false;
        Invoke("ResetAttack", attackCooldown);
    }

    void ResetAttack()
    {
        canAttack = true;
    }

    void LastMovementToLookingDirection()
    {
        lastMovement.x = Mathf.Round(lookingDirection.x);
        lastMovement.z = Mathf.Round(lookingDirection.z);
    }

    void GamePaused(bool paused)
    {
        isPaused = paused;
    }
}
