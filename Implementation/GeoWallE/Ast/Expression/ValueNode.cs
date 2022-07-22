using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoWallE.Ast.Expression
{
    public abstract class ValueNode : ExpressionNode
    {
        public ExpressionTypes Tipo { get { return ExpressionTypes.ValueNode; } }
      
    }
}
