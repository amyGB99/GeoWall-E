using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Compiling;
using GeoWallE.Ast.Expression.Value;

namespace GeoWallE.Ast.Expression.Ternary
{
    public class ConditionalNode : TernaryNode
    {
      
        public override ExpressionTypes Types
        {
            get
            {
                return ExpressionTypes.TernaryNode;
            }
        }
        public override TiposParaChequeo TiposEnChequeo { get; set; }
        public ConditionalNode(ExpressionNode If,ExpressionNode Then, ExpressionNode Else)
        {
            IF = If;
            THEN = Then;
            ELSE = Else;
            TiposEnChequeo = TiposParaChequeo.condicional;
        }
        public override bool ChequeoSemantico(Scope scope,List< CompilingError> listaerrores)
        {
            if (!IF.ChequeoSemantico(scope, listaerrores)) { listaerrores.Add(new CompilingError(new CodeLocation(), ErrorCode.Invalid, "")); return false; }
            if (!ELSE.ChequeoSemantico(scope, listaerrores)) { listaerrores.Add(new CompilingError(new CodeLocation(), ErrorCode.Invalid, "")); return false; }
            if (!THEN.ChequeoSemantico(scope, listaerrores)) { listaerrores.Add(new CompilingError(new CodeLocation(), ErrorCode.Invalid, "")); return false; }
            return true;
        }
        public override objetos Evaluar(Scope scope, Canvas canvas)
        {
            object o = IF.Evaluar(scope,canvas);
            if (Equals(((Numero)o).valor, (double)0))
                return Value = ELSE.Evaluar(scope,canvas);
            else
                return Value = THEN.Evaluar(scope,canvas);
        }
    }
}
