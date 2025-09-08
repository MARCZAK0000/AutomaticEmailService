using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.RenderEmail.RenderExceptons
{
    public class RenderComponentParametersNull : Exception
    {
        public RenderComponentParametersNull()
        {
        }

        public RenderComponentParametersNull(string? message) : base(message)
        {
        }
    }
}
