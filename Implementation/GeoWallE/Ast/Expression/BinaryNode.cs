using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeoWallE.Ast;

namespace GeoWallE.Ast.Expression
{
    public abstract class BinaryNode : ExpressionNode
    {
         public ExpressionNode ParteIsquierda { get; set; }
        public ExpressionNode ParteDerecha { get; set; }
        public ExpressionTypes Tipo { get { return ExpressionTypes.BinaryNode; } }
    }
}
