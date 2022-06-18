
namespace Exceptions
{
    /// <summary>
    /// Исключение. Ошмбка минимальной длинны названия счета
    /// </summary>
    [Serializable]
    public class MinLenghtNameAccountException : Exception
    {
        public MinLenghtNameAccountException() { }
        public MinLenghtNameAccountException(string message) : base(message) { }
        public MinLenghtNameAccountException(string message, Exception inner) : base(message, inner) { }
        protected MinLenghtNameAccountException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
