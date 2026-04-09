namespace MiciomaXD
{

    /// <summary>
    /// Implement this interface to create a manageable state by the finite state machine.
    /// </summary>
    public interface IBasicState
    {
        public void BeginState();
        public void UpdateState();
        public void EndState();
    }
}