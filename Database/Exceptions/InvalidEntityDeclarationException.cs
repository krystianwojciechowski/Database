using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Exceptions
{
    internal class InvalidEntityDeclarationException : Exception
    {
        public InvalidEntityDeclarationException(string ClassName, string AttributeName) : base("Class " + ClassName + " must have " + AttributeName + " attribute") { }
    }
}
