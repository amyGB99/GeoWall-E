using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoWallE.Ast.Expression
{
    public abstract class TernaryNode : ExpressionNode
    {
        public ExpressionNode IF { get; set; }
        public ExpressionNode THEN { get; set; }
        public ExpressionNode ELSE { get; set; }
    }
}
