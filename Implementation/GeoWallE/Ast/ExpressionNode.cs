using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoWallE.Ast
{
   public abstract class ExpressionNode : AstNode
    {
        public abstract ExpressionTypes Types { get; }
        public objetos Value { get; set; }
        public  abstract objetos Evaluar(Scope scope,Canvas canvas);
        public abstract TiposParaChequeo TiposEnChequeo { get; set; }
    }
}
