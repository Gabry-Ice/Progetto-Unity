using UnityEngine;

namespace MiciomaXD
{
    public class Door : MonoBehaviour
    {
        [SerializeField] DoorFSM fsm;
        [SerializeField] FSMContext ctx;

        private void Awake()
        {
            fsm = new DoorFSM();
            ctx.fsm = fsm;
            fsm.Init(new Closed(ctx));
        }

        private void Update()
        {
            fsm.MachineUpdate();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                ctx.openRequest = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                ctx.closeRequest = true;
            }
        }
    }
}