using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Compiling;

namespace GeoWallE.Ast.Expression.Binary
{
    public class AndNode : BinaryNode
    {
        public AndNode()
        {

        }
        public override TiposParaChequeo TiposEnChequeo { get; set; }
        public override ExpressionTypes Types
        {
            get
            {
                return ExpressionTypes.BinaryNode;
            }
        }
        public override bool ChequeoSemantico(Scope scope,List< CompilingError> listaerrores)
        {
            ParteDerecha.ChequeoSemantico(scope, listaerrores);
            ParteIsquierda.ChequeoSemantico(scope, listaerrores);
            if (ParteDerecha.ChequeoSemantico(scope, listaerrores) && ParteIsquierda.ChequeoSemantico(scope, listaerrores))
            {
                if (ParteDerecha.TiposEnChequeo == ParteIsquierda.TiposEnChequeo)
                {
                    TiposEnChequeo = ParteDerecha.TiposEnChequeo;
                    return true;
                }
            }
            listaerrores.Add(new CompilingError(new CodeLocation(), ErrorCode.Invalid, "")); return false;
        }
        public override objetos Evaluar(Scope scope,Canvas canvas)
        {
            throw new NotImplementedException();
        }
    }
}
