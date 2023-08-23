using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Section12ChessGame.Entities.BoardClasses
{
    internal class BoardException : ApplicationException
    {
        public BoardException(string message) 
            : base(message) { }
    }
}
