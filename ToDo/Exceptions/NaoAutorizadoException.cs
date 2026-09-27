namespace ToDo.Exceptions
{
    public class NaoAutorizadoException : Exception
    {
        public NaoAutorizadoException(string mensagem) : base(mensagem)
        {
        }
    }
}