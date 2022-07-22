using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Compiling;

namespace GeoWallE.Ast.Expression.Value
{
    public class MeasureNode : ValueNode
    {
        #region Variables
        public Punto Punto1;
        public Punto Punto2;
        string NombrePunto1;
        string NombrePunto2;
        public Medida MedidaEntrePuntos;
        #endregion
        public override ExpressionTypes Types
        {
            get
            {
                return ExpressionTypes.ValueNode; 
            }
        }
        public override TiposParaChequeo TiposEnChequeo { get; set; } 
        public MeasureNode(string Refpunto1, string Refpunto2)
        {
            NombrePunto1 = Refpunto1;
            NombrePunto2 = Refpunto2;
            TiposEnChequeo = TiposParaChequeo.Number;
        }
        public override bool ChequeoSemantico(Scope scope, List<CompilingError> listaerrores)
        {
            if (Value != null) return true;
            if (!scope.Variables.ContainsKey(NombrePunto1)) { listaerrores.Add(new CompilingError(new CodeLocation(), ErrorCode.Invalid, "")); return false; }
            if (!scope.Variables.ContainsKey(NombrePunto2)) { listaerrores.Add(new CompilingError(new CodeLocation(), ErrorCode.Invalid, "")); return false; }
            return true;
        }
        public override objetos Evaluar(Scope scope, Canvas canvas)
        {
            if (Value != null) return Value;
            Punto1 = (Punto)Metodos_Importantes.BuscarEnScopeObjetos(NombrePunto1, scope);
            Punto2 = (Punto)Metodos_Importantes.BuscarEnScopeObjetos(NombrePunto2, scope);
            Value = new Medida(Punto1, Punto2);
            return Value;
        }
    }
}
