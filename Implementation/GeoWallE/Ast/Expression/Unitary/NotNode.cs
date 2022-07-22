using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Compiling;

namespace GeoWallE.Ast.Expression.Unitary
{
    public class NotNode : UnitaryNode
    {
        public NotNode()
        {

        }
        public override TiposParaChequeo TiposEnChequeo { get; set; }
        public override ExpressionTypes Types
        {
            get
            {
               return ExpressionTypes.UnitaryNode;
            }
        }
        public override bool ChequeoSemantico(Scope scope,List< CompilingError> listaerrores)
        {
            if (ParteDerecha.ChequeoSemantico(scope, listaerrores))
            {                 
                    TiposEnChequeo = ParteDerecha.TiposEnChequeo;
                    return true;
            }
            listaerrores.Add(new CompilingError(new CodeLocation(), ErrorCode.Invalid, "")); return false;
        }
        public override objetos Evaluar(Scope scope, Canvas canvas)
        {
            throw new NotImplementedException();
        }
    }
}
