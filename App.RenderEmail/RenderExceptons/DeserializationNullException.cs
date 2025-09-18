using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.RenderEmail.RenderExceptons
{
    internal class DeserializationNullException : Exception
    {
        public DeserializationNullException()
        {
        }

        public DeserializationNullException(string? message) : base(message)
        {
        }
    }
}
