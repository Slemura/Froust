using System;

namespace RpDev.Services.AsyncStateMachine.Data.Exceptions
{
    public class RuntimeStateMachineException : Exception
    {
        public RuntimeStateMachineException(string message) : base(message)
        {
            
        }
    }
}