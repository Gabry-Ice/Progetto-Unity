using MiciomaXD;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace MiciomaXD.Es3
{
    public class BobAnim : MonoBehaviour
    {
        [SerializeField] BobContext ctx;
        BobFSM fsm;

        private void Start()
        {
            fsm = new BobFSM();
            ctx.fsm = fsm;
            fsm.Init(new BobIdle(ctx));
        }

        private void Update()
        {
            fsm.MachineUpdate();
        }
    }

    public class BobMoving : IBasicState
    {
        private BobContext ctx;

        public BobMoving(BobContext ctx)
        {
            this.ctx = ctx;
        }

        public void BeginState()
        {
            Debug.Log("Bob is moving to dest " + ctx.dests[ctx.currentDestIndex].name);

            ctx.animator.SetBool("IsMoving", true);
        }

        public void EndState()
        {
            ctx.animator.SetBool("IsMoving", false);
        }

        public void UpdateState()
        {
            ctx.animator.SetFloat("Speed", ctx.agent.velocity.magnitude / ctx.agent.speed, 0.2f, Time.deltaTime);

            if (!ctx.agent.pathPending && ctx.agent.remainingDistance <= ctx.agent.stoppingDistance)
            {
                ctx.fsm.SwitchState(new BobIdle(ctx));
            }
        }
    }

    public class BobIdle : IBasicState
    {
        private BobContext ctx;

        public BobIdle(BobContext ctx)
        {
            this.ctx = ctx;
        }

        public void BeginState()
        {
            Debug.Log("Bob is deciding his destination...");

            int candidate = -1;
            do
            {
                candidate = Random.Range(0, ctx.dests.Count);
            } while (candidate == ctx.currentDestIndex); //do not pick the same dest as before

            ctx.currentDestIndex = candidate;

            Transform dest = ctx.dests[ctx.currentDestIndex];
            Vector3 onNM = NavMesh.SamplePosition(dest.position, out NavMeshHit hit, 1f, NavMesh.AllAreas) ? hit.position : dest.position;
            ctx.agent.SetDestination(onNM);
        }

        public void EndState()
        {

        }

        public void UpdateState()
        {
            ctx.fsm.SwitchState(new BobMoving(ctx));
        }
    }

    [Serializable]
    public class BobContext
    {
        public BobFSM fsm;
        public NavMeshAgent agent;
        public List<Transform> dests;
        public int currentDestIndex = -1; //-1 = no dest

        public Animator animator;
    }

    public class BobFSM : BasicStateMachine { }
}
