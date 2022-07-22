using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Compiling;

namespace GeoWallE.Ast.Expression.Value
{
    public class CircleNode : ValueNode
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
        public string NombreCentro;
        public string NombreMedida;
        public Punto Centro;
        public Medida Radio;
        #endregion
        public CircleNode(string nombre)
        {
            Nombre = nombre;
            Value = new Circunferencia(Nombre);
            TiposEnChequeo = TiposParaChequeo.Figura;
          
        }
        public CircleNode(string centro, string radio)
        {
            NombreCentro = centro;
            NombreMedida = radio;
            TiposEnChequeo = TiposParaChequeo.Figura;
        }
        public override bool ChequeoSemantico(Scope scope,List<CompilingError> listaerrores)
        {
            if (Value != null)
            {
                return true;
            }
            if (!scope.Variables.ContainsKey(NombreCentro)) { listaerrores.Add(new CompilingError(new CodeLocation(), ErrorCode.Invalid, "No se encuentra la variable en el scope")); return false; }
            if (!scope.Variables.ContainsKey(NombreMedida)) { listaerrores.Add(new CompilingError(new CodeLocation(), ErrorCode.Invalid, "No se encuentra la variable en el scope")); return false; }
            return true;
        }
        public override objetos Evaluar(Scope scope, Canvas canvas)
        {
            if (Value != null) return Value;
            else

            {
                Centro = (Punto)Metodos_Importantes.BuscarEnScopeObjetos(NombreCentro, scope);
                Radio = (Medida)Metodos_Importantes.BuscarEnScopeObjetos(NombreMedida, scope);
                Value = new Circunferencia(Centro, Radio);
                return Value;
            }
        }
    }
}
