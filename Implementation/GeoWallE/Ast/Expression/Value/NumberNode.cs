using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Compiling;

namespace GeoWallE.Ast.Expression.Value
{
    public class NumberNode : ValueNode
    {
        public  NumberNode(Numero numero)
        {
            Value = numero;
            TiposEnChequeo = TiposParaChequeo.Number;
        }
        public override TiposParaChequeo TiposEnChequeo { get; set; }
        public override ExpressionTypes Types
        {
            get
            {
               return ExpressionTypes.Number;
            }
        }
        public override bool ChequeoSemantico(Scope scope,List< CompilingError> listaerrores)
        {
            return true;
        }
        public override objetos Evaluar(Scope scope, Canvas canvas)
        {
            return Value;
        }
    }
}
