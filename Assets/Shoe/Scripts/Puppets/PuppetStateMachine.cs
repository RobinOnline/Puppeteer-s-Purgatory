using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using static States;

public class PuppetStateMachine : MonoBehaviour
{
    public STATES currentState;
    [Space]

    [SerializeField] private float speed;
    [SerializeField] private float huntSpeed;
    [SerializeField] private float rotationSpeed;

    [Header("Detection Var")]
    [SerializeField] private float visionRange;
    [SerializeField] private float visionAngle;
    [SerializeField] private float audioRange;
    [SerializeField] private LayerMask targetLayer;
    [SerializeField] private LayerMask obstacleLayer;
    [SerializeField] private Transform rayOrigin;
    Vector3 _targetDir;
    [SerializeField] private Transform target;
    [SerializeField] private Transform TransformL, TransformR;

    [Space]
    Collider[] _targets;
    [SerializeField] private NavMeshAgent agent;

   

    private void Start()
    {
        currentState = STATES.IDLE;
    }

    public void DetectCharacter()
    {

    }
    public void Patrol()
    {

    }

    public void LookAtTarget()
    {

    }

    public float GetDistanceToTarget()
    {
        Vector3 _direction = PlayerPickUp.instance.transform.position - transform.position;
        return _direction.sqrMagnitude;
    }


    private void OnDrawGizmos()
    {
        //Dibujamos el rango de vision
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, visionRange);
        //Dibujamos el cono de vision
        Gizmos.color = Color.green;
        //Rotamos los helper para que tengan la rotacion igual a la mitad del angulo de vision
        //Para dibujar el cono de vision, rotamos dos objetos vacios para luego lanzar un rayo
        //en el forward de cada uno de ellos y dibuje el cono
        TransformL.localRotation = Quaternion.Euler(0f, visionAngle / -2f, 0f);
        TransformR.localRotation = Quaternion.Euler(0f, visionAngle / 2f, 0f);
        Gizmos.DrawRay(TransformL.position, TransformL.forward * visionRange);
        Gizmos.DrawRay(TransformR.position, TransformR.forward * visionRange);

        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, audioRange);
    }
}
