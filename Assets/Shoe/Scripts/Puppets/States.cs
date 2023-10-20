using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class States
{
    public enum STATES
    {
        IDLE, PATROL, HUNT
    }

    public enum EVENTS
    {
        START, UPDATE, EXIT
    }

    public virtual void Start() { stage = EVENTS.UPDATE; }
    public virtual void Update() { stage = EVENTS.UPDATE; }
    public virtual void Exit() { stage = EVENTS.EXIT; }

    public STATES name;
    protected EVENTS stage;
    protected States nextState;
    protected float counter;

    public States()
    {

    }

    public States Process()
    {
        if (stage == EVENTS.START) Start();

        if (stage == EVENTS.UPDATE) Update();

        if (stage == EVENTS.EXIT)
        {
            Exit();
            return nextState;
        }
        return this;
    }

    public class Idle : States
    {
        public Idle() : base()
        {
            name = STATES.IDLE;
        }

        public override void Start()
        {
            base.Start();
        }

        public override void Update()
        {
            nextState = new Patrol();
            stage = EVENTS.EXIT;

        }

        public override void Exit()
        {
            base.Exit();
        }
    }

    public class Patrol : States
    {
        public Patrol() : base()
        {
            name = STATES.PATROL;
        }

        public override void Start()
        {
            Debug.Log("Patrol");
            base.Start();
        }

        public override void Update()
        {
            PuppetStateMachine.instance.DetectCharacter();
            base.Update();
        }

        public override void Exit()
        {
            base.Exit();
        }
    }

    public class Hunt : States
    {
        public Hunt() : base()
        {
            name = STATES.HUNT;
        }

        public override void Start()
        {
            base.Start();
        }

        public override void Update()
        {
            PuppetStateMachine.instance.DetectCharacter();
            base.Update();
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}
