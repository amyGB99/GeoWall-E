using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Compiling;

namespace GeoWallE.Ast.Expression.Value
{
    public class RayNode : ValueNode
    {
        public ExpressionTypes Type { get; private set; }
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
        public string NombrePuntoInicial;
        public string NombrePunto2;
        public Punto Punto1;
        public Punto Punto2;
        #endregion
        public RayNode(string nombre)
        {
            Nombre = nombre;
            Value = new Rayo(Nombre);
            TiposEnChequeo = TiposParaChequeo.Figura; 
   
        }
        public RayNode(string punto1, string punto2)
        {
            NombrePuntoInicial = punto1;
            NombrePunto2 = punto2;
            TiposEnChequeo = TiposParaChequeo.Figura;
        }
        public override bool ChequeoSemantico(Scope scope, List<CompilingError> listaerrores)
        {
            if (Value != null) return true;
            else
            {
                if (!scope.Variables.ContainsKey(NombrePuntoInicial)) { listaerrores.Add(new CompilingError(new CodeLocation(), ErrorCode.Invalid, "")); return false; }
                if (!scope.Variables.ContainsKey(NombrePunto2)) { listaerrores.Add(new CompilingError(new CodeLocation(), ErrorCode.Invalid, "")); return false; }
                return true;
            }
        }
        public override objetos Evaluar(Scope scope, Canvas canvas)
        {
            if (Value != null) return Value;
            else
            {
                
                Punto1 = (Punto)Metodos_Importantes.BuscarEnScopeObjetos(NombrePuntoInicial,scope);
                Punto2 = (Punto)Metodos_Importantes.BuscarEnScopeObjetos(NombrePunto2, scope);
                Value = new Rayo(Punto1, Punto2, Nombre);
                return Value;
            }
        }
    }
}
