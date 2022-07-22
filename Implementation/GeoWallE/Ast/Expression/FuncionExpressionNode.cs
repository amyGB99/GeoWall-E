using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Compiling;
using GeoWallE.Ast.Expression.Value;
using GeoWallE;

namespace GeoWallE.Ast.Expression
{
    public class FuncionExpressionNode : ExpressionNode
    {
        public string Nombre;
        public List<ExpressionNode> Parametros;
        public Scope NuevoScope;
        public List<string> Param;
        public FuncionExpressionNode(string nombre, List<ExpressionNode> parametros)
        {
            Nombre = nombre;
            Parametros = parametros;
            Param = new List<string>();
            TiposEnChequeo = TiposParaChequeo.Text;
        }
        public override ExpressionTypes Types
        {
            get
            {
                return ExpressionTypes.funcion;
            }
        }
        public override TiposParaChequeo TiposEnChequeo { get; set; }
        public override bool ChequeoSemantico(Scope scope, List<CompilingError> listaerrores)
        {
            if (Metodos_Importantes.EstaEnelScopePadreVariables(scope, Nombre))
                return true;
            if (Metodos_Importantes.EstaEnelScopePadreParametros(scope, Nombre) && Metodos_Importantes.CantidadDeParametro(NuevoScope,Parametros,Nombre))
                return true;
            if (Metodos_Importantes.EstaEnelScopePadreExpresionesFunciaones(scope, Nombre))
                return true;
            listaerrores.Add(new CompilingError(new CodeLocation(), ErrorCode.Invalid, ""));
            return false;
        }
        public override objetos Evaluar(Scope scope, Canvas canvas)
        {
            Param = Metodos_Importantes.BuscarEnScopeParametros(Nombre, scope);
            NuevoScope = new Scope(scope);
            for (int j = 0; j < Parametros.Count; j++)
            {
                NuevoScope.Objetos.Add(Param[j], Parametros[j].Evaluar(NuevoScope,canvas));
            }
            List<string> parametros = new List<string>();
            Value = Metodos_Importantes.BuscarEnScopeExpresionesFunciones(Nombre,NuevoScope).Evaluar(NuevoScope,canvas);
            return Value;

        }
     
    }
}
