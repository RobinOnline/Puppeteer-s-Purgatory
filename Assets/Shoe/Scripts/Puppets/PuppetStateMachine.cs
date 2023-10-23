using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using static States;

public class PuppetStateMachine : MonoBehaviour
{
    public STATES State;
    public States currentState;
    [Space]

    [Header("Stats")]
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
    public Transform target;
    [SerializeField] private Transform TransformL, TransformR;

    [Header("Patrol")]
    public Transform[] Waypoints;
    public int currentRoom;

    [Space]
    Collider[] _targets;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Animator anim;

    public static PuppetStateMachine instance;

    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
    }

    private void Start()
    {
        currentState = new Idle();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        currentState = currentState.Process();
    }

    public void DetectCharacter()
    {
        //Guardamos todos los objetos encontrados con el overlap
        Collider[] _targets = Physics.OverlapSphere(transform.position, visionRange, targetLayer);
        //Si ha encontrado algún objeto, la longitud del array es mayor que 0
        if (_targets.Length > 0)
        {
            //Calculamos la direccion hacia el objeto
            Vector3 _targetDir = _targets[0].transform.position - rayOrigin.position;
            //Si esta fuera del angulo de vision, lo ignoramos
            //Se calcula si esta dentro con el angulo que hay entre el forward y la direccion
            //del objetivo. Si este angulo es menor que la mitad del angulo de vision, esta dentro
            if (Vector3.Angle(transform.forward, _targetDir) > visionAngle / 2f)
            {
                return;
            }
            //Lanzamos un rayo desde el enemigo hacia el jugador para comprobar si esta
            //escondido detras de alguna pared u obstaculo
            //Sumamos un offset al origen en el eje Y para que no lance el rayo desde los pies
            if (Physics.Raycast(rayOrigin.position, _targetDir.normalized,
                _targetDir.magnitude, obstacleLayer) == false)
            {
                target = _targets[0].transform;
            }
            //Dibujamos el rayo que comprueba si esta tras un obstaculo
            //Sumamos un offset al origen en el eje Y para que no lance el rayo desde los pies
            Debug.DrawRay(rayOrigin.position, _targetDir, Color.magenta);
        }
        //Si el array está vacío, no ha encontrado nada
        else
        {
            //Dejamos el target a null para que deje de perseguirlo
            target = null;
        }
    }
    public void Patrol()
    {
        agent.SetDestination(Waypoints[currentRoom].position);
        anim.SetBool("Hunting", false);
        if (agent.remainingDistance <= .1f)
        {
            currentRoom++;
            
        }
        if (currentRoom >= Waypoints.Length)
        {
            currentRoom = 0;
        }
    }

    public void Hunt()
    {
        if (target != null)
        {
            anim.SetBool("Hunting", true);
            agent.SetDestination(target.position);
            agent.speed = speed;
            if (agent.remainingDistance <= 1f)
            {
                agent.velocity = Vector3.zero;
            }
        }
    }

    public void LookAtTarget()
    {
        Vector3 _direction = target.position - transform.position;
        _direction.y = 0;
        Quaternion _rot = Quaternion.LookRotation(_direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, _rot, Time.deltaTime * rotationSpeed);
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
