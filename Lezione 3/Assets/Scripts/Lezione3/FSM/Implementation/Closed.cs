namespace MiciomaXD
{
    public class Closed : IBasicState
    {
        private FSMContext ctx;

        public Closed(FSMContext ctx)
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
            if (ctx.openRequest)
            {
                ctx.openRequest = false;
                ctx.fsm.SwitchState(new Opening(ctx));
            }
        }
    }
}