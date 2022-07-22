using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Compiling;

namespace GeoWallE.Ast.Expression.Value
{
    public class InfinitySequenceNode: ValueNode
    {
        public int NumeroDondeEmpieza;
        public InfinitySequenceNode(string dondeempieza)
        {
            NumeroDondeEmpieza = int.Parse(dondeempieza);

        }
        public override TiposParaChequeo TiposEnChequeo { get; set; }
        public override ExpressionTypes Types
        {
            get
            {
                return ExpressionTypes.ValueNode;
            }
        }

        public override bool ChequeoSemantico(Scope scope, List<CompilingError> listaerrores)
        {
            throw new NotImplementedException();
        }

        public override objetos Evaluar(Scope scope, Canvas canvas)
        {
            Value = new SecuenciaInfinitaNumeros(NumeroDondeEmpieza);
            return Value;
        }
    }

}
