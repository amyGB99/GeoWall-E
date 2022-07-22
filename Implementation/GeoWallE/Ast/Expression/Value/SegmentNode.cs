using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Compiling;

namespace GeoWallE.Ast.Expression.Value
{
    public class SegmentNode : ValueNode
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
        public string NombrePunto1;
        public string NombrePunto2;
        public string NombreSegmento;
        public Punto Punto1;
        public Punto Punto2;
        #endregion
        public SegmentNode(string punto1, string punto2)
        {
            NombrePunto1 = punto1;
            NombrePunto2 = punto2;
            TiposEnChequeo = TiposParaChequeo.Figura;
        }
        public SegmentNode(string nombre)
        {
            NombreSegmento = nombre;
            Value = new Segmento(NombreSegmento);
            TiposEnChequeo = TiposParaChequeo.Figura;
        } 
        public override bool ChequeoSemantico(Scope scope,List< CompilingError> listaerrores)
        {
            if (Value != null) return true;
            else
            {
                if (!scope.Variables.ContainsKey(NombrePunto1)) { listaerrores.Add(new CompilingError(new CodeLocation(), ErrorCode.Invalid, "")); return false; }
                if (!scope.Variables.ContainsKey(NombrePunto2)) { listaerrores.Add(new CompilingError(new CodeLocation(), ErrorCode.Invalid, "")); return false; }
                return true;
            }
        }
        public override objetos Evaluar(Scope scope, Canvas canvas)
        {
            if (Value != null) return Value;
            else
            {   
                Punto1 = (Punto)Metodos_Importantes.BuscarEnScopeObjetos(NombrePunto1, scope);
                Punto2 = (Punto)Metodos_Importantes.BuscarEnScopeObjetos(NombrePunto2, scope);
                Value = new Segmento(Punto1, Punto2,NombreSegmento);
                return Value;
            }
        }
    }
}
