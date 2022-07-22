using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoWallE.Ast.Expression
{
   public abstract class UnitaryNode:ExpressionNode
    {
        public ExpressionNode ParteDerecha { get; set; }
        public ExpressionTypes Tipo { get { return ExpressionTypes.UnitaryNode; } }
    }
}
