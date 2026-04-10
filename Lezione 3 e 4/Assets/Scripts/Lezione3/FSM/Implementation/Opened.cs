using Unity.VisualScripting;

namespace MiciomaXD
{
    public class Opened : IBasicState
    {
        private FSMContext ctx;

        public Opened(FSMContext ctx)
        {
            this.ctx = ctx;
        }

        public void BeginState()
        {
        }

        public void EndState()
        {
        }

        public void UpdateState()
        {
            if (ctx.closeRequest)
            {
                ctx.closeRequest = false;
                ctx.fsm.SwitchState(new Closing(ctx));
            }
        }
    }
}