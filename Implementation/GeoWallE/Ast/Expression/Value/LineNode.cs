using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeoWallE;
using Compiling;


namespace GeoWallE.Ast.Expression.Value
{
    public class LineNode : ValueNode
    {
        #region Variables
        string NombrePunto1;
        string NombrePunto2;
        string NombreRecta;
        Punto Punto1;
        Punto Punto2;
        #endregion
        public override ExpressionTypes Types
        {
            get
            {
                return ExpressionTypes.ValueNode;
            }
        }
        public override TiposParaChequeo TiposEnChequeo { get; set; }
        public LineNode(string punto1,string punto2)
        {
            NombrePunto1 = punto1;
            NombrePunto2 = punto2;
            TiposEnChequeo = TiposParaChequeo.Figura;
        }
        public LineNode(string nombrerecta)
        {
            NombreRecta = nombrerecta;
            Value = new Recta(NombreRecta);
            TiposEnChequeo = TiposParaChequeo.Figura;
        }
        public override bool ChequeoSemantico(Scope scope,List< CompilingError> listaerrores)
        {
            if (Value != null)
            {
                
                return true;
            }
            if (!scope.Variables.ContainsKey(NombrePunto1)) { listaerrores.Add(new CompilingError(new CodeLocation(), ErrorCode.Invalid, "")); return false; }
            if (!scope.Variables.ContainsKey(NombrePunto2)) { listaerrores.Add(new CompilingError(new CodeLocation(), ErrorCode.Invalid, "")); return false; }
            return true;
        }
        public override objetos Evaluar(Scope scope, Canvas canvas)
        {
            if (Value != null) return Value;
            else
            {
                //PointNode punto1 = (PointNode)Metodos_Importantes.BuscarEnScopeVariables(NombrePunto1,scope);
                Punto1 = (Punto)Metodos_Importantes.BuscarEnScopeObjetos(NombrePunto1, scope);
                //PointNode punto2 = (PointNode)Metodos_Importantes.BuscarEnScopeVariables(NombrePunto2,scope);
                Punto2 = (Punto)Metodos_Importantes.BuscarEnScopeObjetos(NombrePunto2, scope);
                Value = new Recta(Punto1, Punto2,NombreRecta);
                return Value;
            }
        }
    }
}
