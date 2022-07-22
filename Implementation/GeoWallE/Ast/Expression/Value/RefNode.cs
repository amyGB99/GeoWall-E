using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Compiling;

namespace GeoWallE.Ast.Expression.Value
{
    public class RefNode : ValueNode
    {
        #region Variables
        string NombreVariable;
        #endregion
        public RefNode(string nombre)
        {
            NombreVariable = nombre;
            Text Nombre = new Text(nombre);
            Value = Nombre;

        }
        public override TiposParaChequeo TiposEnChequeo { get; set; }
        public override ExpressionTypes Types
        {
            get
            {
                return ExpressionTypes.refe;
            }
        }
        public override bool ChequeoSemantico(Scope scope,List< CompilingError> listaerrores)
        {
            if (!Metodos_Importantes.EstaEnelScopePadreVariables(scope,NombreVariable)) { listaerrores.Add(new CompilingError(new CodeLocation(),ErrorCode.Invalid,"la variable no esta creada")); return false; }
            TiposEnChequeo = Metodos_Importantes.Sutipo(scope, NombreVariable);
            return true;
        }
        public override objetos Evaluar(Scope scope, Canvas canvas)
        {
         Value= Metodos_Importantes.BuscarEnScopeObjetos(NombreVariable,scope);
            return Value;
        }
    }
}
