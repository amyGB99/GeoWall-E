using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoWallE.Ast
{
    public abstract class InstruccionNode : AstNode
    {
        public abstract void Ejecutar(Scope scope, Canvas canvas);

    }
}
