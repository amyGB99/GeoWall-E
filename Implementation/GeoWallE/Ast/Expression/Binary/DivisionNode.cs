using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Compiling;
using GeoWallE.Ast.Expression.Value;

namespace GeoWallE.Ast.Expression.Binary
{
    public class DivisionNode : BinaryNode
    {
        public DivisionNode()
        {
            Value = null;
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
            if(ParteDerecha.TiposEnChequeo == ParteIsquierda.TiposEnChequeo && ParteDerecha.TiposEnChequeo != TiposParaChequeo.Figura && ParteIsquierda.TiposEnChequeo != TiposParaChequeo.Figura)
            {
                TiposEnChequeo = ParteIsquierda.TiposEnChequeo;
                return true;
            }
            listaerrores.Add(new CompilingError(new CodeLocation(), ErrorCode.Invalid, ""));return false;
        }
        public override objetos Evaluar(Scope scope,Canvas canvas)
        {
            object o = ParteDerecha.Evaluar(scope, canvas);
            if (Equals(((Numero)o).valor, (double)0))
            {
                return null;
            }
            Value = (new Numero(((Numero)(ParteIsquierda.Evaluar(scope,canvas))).valor / ((Numero)(ParteDerecha.Evaluar(scope,canvas))).valor));
            return Value;
        }
    }
}
