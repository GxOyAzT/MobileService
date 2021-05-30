using System;

namespace MobileService.Entities.Exceptions
{
    public class BuilderNotDefinedException : Exception
    {
        public BuilderNotDefinedException()
        {
        }

        public BuilderNotDefinedException(string message)
        : base(message)
        {
        }

        public BuilderNotDefinedException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
