using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Compiling;

namespace GeoWallE.Ast.Expression.Value
{
    public class PointNode : ValueNode
    {
        public override ExpressionTypes Types
        {
            get
            {
                return ExpressionTypes.ValueNode;
            }
        }
        public override TiposParaChequeo TiposEnChequeo { get; set; }
        #region Variables
        public string Nombre;
        #endregion
        public PointNode(string nombre)
        {
            Nombre = nombre;
            TiposEnChequeo = TiposParaChequeo.Figura;
        
        }
        public override bool ChequeoSemantico(Scope scope,List< CompilingError> listaerrores)
        {

            if (scope.Variables.ContainsKey(Nombre)) { listaerrores.Add(new CompilingError(new CodeLocation(), ErrorCode.Invalid, "")); return false; }
            return true;
        }
        public override objetos Evaluar(Scope scope, Canvas canvas)
        {

            if (Value != null) return Value;
            else
                Value = new Punto(Nombre);
            return Value;
        }
    }
}
