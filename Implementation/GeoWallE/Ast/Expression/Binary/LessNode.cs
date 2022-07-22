using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Compiling;
using GeoWallE.Ast.Expression.Value;

namespace GeoWallE.Ast.Expression.Binary
{
    public class LessNode : BinaryNode
    {
        public LessNode()
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
            if (ParteDerecha.ChequeoSemantico(scope, listaerrores) && ParteIsquierda.ChequeoSemantico(scope, listaerrores) && ParteDerecha.TiposEnChequeo != TiposParaChequeo.Figura && ParteIsquierda.TiposEnChequeo != TiposParaChequeo.Figura)
            {
                if (ParteDerecha.TiposEnChequeo == ParteIsquierda.TiposEnChequeo)
                {
                    TiposEnChequeo = ParteDerecha.TiposEnChequeo;
                    return true;
                }
            }
            listaerrores.Add(new CompilingError(new CodeLocation(), ErrorCode.Invalid, "")); return false;
        }
        public override objetos Evaluar(Scope scope, Canvas canvas)
        {
            if(ParteDerecha.Evaluar(scope,canvas) is Numero && ParteIsquierda.Evaluar(scope,canvas) is Numero )
            {
                Numero a = (Numero)ParteDerecha.Evaluar(scope, canvas);
                Numero b = (Numero)ParteIsquierda.Evaluar(scope, canvas);
                if (b.valor < a.valor)
                    return new Numero( 1);
                else return  new Numero(0);
            }
            
            return new Numero(0);
        }
    }
}
