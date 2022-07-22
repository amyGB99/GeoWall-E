using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Compiling;

namespace GeoWallE.Ast.Expression.Value
{
    public class ArcNode : ValueNode
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
        public string NombreVariable;
        public string NombreCentroArco;
        public string NombreRadioArco;
        public string NombrePuntoInicial;
        public string NombrePuntoFinal;
        public Punto Centro;
        public Punto PuntoInicial;
        public Punto PuntoFinal;
        public Medida Radio;
        #endregion
        public ArcNode(string nombre)
        {
            NombreVariable = nombre;
            Value = new Arco(NombreVariable);
            TiposEnChequeo = TiposParaChequeo.Figura;
        }
        public ArcNode(string centro, string puntoinicial, string puntofinal, string radio)
        {
            NombrePuntoInicial = puntoinicial;
            NombrePuntoFinal = puntofinal;
            NombreCentroArco = centro;
            NombreRadioArco = radio;
            TiposEnChequeo = TiposParaChequeo.Figura;
        }
        public override bool ChequeoSemantico(Scope scope,List< CompilingError> listaerrores)
        {
            if (Value == null)
            {
                if (!scope.Variables.ContainsKey(NombreCentroArco)) { listaerrores.Add(new CompilingError(new CodeLocation(), ErrorCode.Invalid, "")); return false; }
                if (!scope.Variables.ContainsKey(NombrePuntoInicial)) { listaerrores.Add(new CompilingError(new CodeLocation(), ErrorCode.Invalid, "")); return false; }
                if (!scope.Variables.ContainsKey(NombrePuntoFinal)) { listaerrores.Add(new CompilingError(new CodeLocation(), ErrorCode.Invalid, "")); return false; }
                if (!scope.Variables.ContainsKey(NombreRadioArco)) { listaerrores.Add(new CompilingError(new CodeLocation(), ErrorCode.Invalid, "")); return false; }
            }
            else
            {
                if (scope.Variables.ContainsKey(NombreVariable)) return false;
            }
            return true;
        }

        public override objetos Evaluar(Scope scope, Canvas canvas)
        {
            if (Value != null) return Value;
            else
            {
            Centro=(Punto) scope.Objetos[NombreCentroArco];
                PuntoInicial = (Punto)Metodos_Importantes.BuscarEnScopeObjetos(NombrePuntoInicial,scope);
                PuntoFinal = (Punto)Metodos_Importantes.BuscarEnScopeObjetos(NombrePuntoFinal,scope);
                Radio = (Medida)Metodos_Importantes.BuscarEnScopeObjetos(NombreRadioArco,scope);
                Value = new Arco(PuntoInicial, PuntoFinal, Centro, Radio);
                return Value;
            }
            throw new Exception();

        }
    }
}
