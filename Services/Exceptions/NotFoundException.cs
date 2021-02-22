using System;

namespace insulin_backend.Services.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException()
        {
        }

        public NotFoundException(string message) : base(message) { }
        
    }
}