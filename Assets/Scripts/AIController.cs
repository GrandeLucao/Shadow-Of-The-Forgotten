using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;   
    public AudioSource audioFoda; 
    private Animator anim; 
    public float startWaitTime;
    public float timeToRotate;
    public float speedWalk;
    public float speedRun;

    public float viewRadius;
    public float viewAngle;
    public LayerMask playerMask;
    public LayerMask obstacleMask;
    public float meshResolution=1f;
    public int edgeIterations=4;
    public float edgeDistance=0.5f;

    public AudioClip walkSound;
    public AudioClip runSound;

    public Transform[] waypoints;
    int m_CurrentWaypointIndex;

    Vector3 playerLastPosition=Vector3.zero;
    Vector3 m_PlayerPosition;

    float m_WaitTime;
    float m_TimeToRotate;
    bool m_PlayerInRange;
    bool m_PlayerNear;
    bool m_IsPatrol;
    bool m_CaughtPlayer;
    bool m_gameOver;


    void Start()
    {
        audioFoda=GetComponent<AudioSource>();
        m_PlayerPosition=Vector3.zero;
        m_IsPatrol=true;
        m_CaughtPlayer=false;
        m_PlayerInRange=false;
        m_gameOver=false;
        m_WaitTime=startWaitTime;
        m_TimeToRotate=timeToRotate;

        m_CurrentWaypointIndex=0;
        navMeshAgent=GetComponent<NavMeshAgent>();
        anim=GetComponent<Animator>();

        navMeshAgent.isStopped=false;
        navMeshAgent.speed=speedWalk;
        navMeshAgent.SetDestination(waypoints[m_CurrentWaypointIndex].position);   
        anim.SetInteger("troca", 2); 
    }

    void Update()
    {
        if(m_gameOver)
        {
            speedRun=22;
            viewAngle=9000;
            viewRadius=360;
            InfChase();
        }
        else
        {
            EnviromentView();
            if(!m_IsPatrol)
            {
                Chasing();
            }
            else
            {
                Patroling();
            }
        }
        
    }

    private void Chasing()
    {        
        anim.SetInteger("troca",3);
        m_PlayerNear=false;
        audioFoda.Pause();
        audioFoda.loop=false;
        playerLastPosition=Vector3.zero;
            if (!audioFoda.isPlaying){
            audioFoda.clip=runSound;
            audioFoda.loop=true;
            audioFoda.Play();
            }

        if(!m_CaughtPlayer)
        {
            Move(speedRun);
            navMeshAgent.SetDestination(m_PlayerPosition);
        }
        if(navMeshAgent.remainingDistance<=navMeshAgent.stoppingDistance)
        {
            if(m_WaitTime<=0 && !m_CaughtPlayer && Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position)>=15f)
            {
                m_IsPatrol=true;
                m_PlayerNear=false;
                Move(speedWalk);
                m_TimeToRotate=timeToRotate;
                m_WaitTime=startWaitTime;
                navMeshAgent.SetDestination(waypoints[m_CurrentWaypointIndex].position);
            }
            else
            {
                if(Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position)>=2.5f)
                {
                    Stop();
                    m_WaitTime-=Time.deltaTime;
                }
            }
        }

    }

    private void InfChase()
    {
        audioFoda.Pause();
        audioFoda.loop=false;
        anim.SetInteger("troca",3);
        playerLastPosition=Vector3.zero;
        if (!audioFoda.isPlaying){
        audioFoda.clip=runSound;
        audioFoda.loop=true;
        audioFoda.Play();
        }

        if(!m_CaughtPlayer)
        {
            m_PlayerPosition=GameObject.FindGameObjectWithTag("Player").transform.position;
            navMeshAgent.SetDestination(m_PlayerPosition);
            Move(speedRun);
        }
    }

    private void Patroling()
    {
        anim.SetInteger("troca",2);
        audioFoda.Pause();
        audioFoda.loop=false;
        if (!audioFoda.isPlaying){
            audioFoda.clip=walkSound;
            audioFoda.loop=true;
            audioFoda.Play();
        }
        if(m_PlayerNear)
        {
            if(m_TimeToRotate<=0)
            {
                Move(speedWalk);
                LookingPlayer(playerLastPosition);
            }
            else
            {
                Stop();
                m_TimeToRotate-=Time.deltaTime;
            }
        }
        else
        {
            m_PlayerNear=false;
            playerLastPosition=Vector3.zero;
            navMeshAgent.SetDestination(waypoints[m_CurrentWaypointIndex].position);
            if(navMeshAgent.remainingDistance<=navMeshAgent.stoppingDistance)
            {
                if(m_WaitTime<=0)
                {
                    NextPoint();
                    Move(speedWalk);
                    m_WaitTime=startWaitTime;
                }
                else
                {
                    Stop();
                    m_WaitTime-=Time.deltaTime;
                }
            }
        }
    }

    void Move(float speed)
    {
        navMeshAgent.isStopped=false;
        navMeshAgent.speed=speed;
    }

    void Stop()
    {
        navMeshAgent.isStopped=true;
        navMeshAgent.speed=0;
        audioFoda.Stop();
        anim.SetInteger("troca",1);
    }

    public void NextPoint()
    {
        m_CurrentWaypointIndex=(m_CurrentWaypointIndex+1)%waypoints.Length;
        navMeshAgent.SetDestination(waypoints[m_CurrentWaypointIndex].position);
    }

    void CaughtPlayer()
    {
        m_CaughtPlayer=true;
        FindObjectOfType<AudioManager>().Stop("BGM");
        FindObjectOfType<AudioManager>().Play("Jumpscare");
        FindObjectOfType<gameController>().GameOverScene();
    }

    void LookingPlayer(Vector3 player)
    {
        navMeshAgent.SetDestination(player);
        if(Vector3.Distance(transform.position, player)<=0.3)
        {
            if(m_WaitTime<=0)
            {
                m_PlayerNear=false;
                Move(speedWalk);
                navMeshAgent.SetDestination(waypoints[m_CurrentWaypointIndex].position);
                m_WaitTime=startWaitTime;
                m_TimeToRotate=timeToRotate;
            }
            else
            {
                Stop();
                m_WaitTime-=Time.deltaTime;
            }
        }
    }

    void EnviromentView()
    {
        Collider[] playerInRange = Physics.OverlapSphere(transform.position, viewRadius, playerMask);

        for(int i=0;i<playerInRange.Length;i++)
        {
            Transform player =playerInRange[i].transform;
            Vector3 dirToPlayer=(player.position-transform.position).normalized;
            if(Vector3.Angle(transform.forward, dirToPlayer)<viewAngle/2)
            {
                float dstToPlayer=Vector3.Distance(transform.position, player.position);
                if(!Physics.Raycast(transform.position, dirToPlayer, dstToPlayer, obstacleMask))
                {
                    m_PlayerInRange=true;
                    m_IsPatrol=false;
                }
                else
                {
                    m_PlayerInRange=false;
                }
            }
            if(Vector3.Distance(transform.position,player.position)>viewRadius)
            {
                m_PlayerInRange=false;
            }
            if(m_PlayerInRange)
            {
                m_PlayerPosition=player.transform.position;
            }
        }
    }

    public void Timeout()
    {
        m_gameOver=true;
    }

    private void OnTriggerEnter(Collider coll)
    {
        if(coll.gameObject.tag=="Player")
        {
            CaughtPlayer();
        }
    }
}
