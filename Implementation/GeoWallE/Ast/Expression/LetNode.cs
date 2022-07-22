using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Compiling;

namespace GeoWallE.Ast.Expression
{
        public   class LetNode : ExpressionNode
    {
        public List<InstruccionNode> Let;
        public ExpressionNode In;
        public Scope NuevoScope;
        public LetNode( List<InstruccionNode> let, ExpressionNode expresion)
        {
            In = expresion;
            Let = let;
            NuevoScope = new Scope(null);
            TiposEnChequeo = TiposParaChequeo.let;
        }
        public  override ExpressionTypes Types
        {
            get
            {
                return ExpressionTypes.let;
            }
        }
        public override TiposParaChequeo TiposEnChequeo { get; set; }
        public override bool ChequeoSemantico(Scope scope, List<CompilingError> listaerrores)
        {
            foreach (var key in scope.Variables.Keys)
                NuevoScope.Variables.Add(key, scope.Variables[key]);
            foreach (var key in scope.Objetos.Keys)
                NuevoScope.Objetos.Add(key, scope.Objetos[key]);
            foreach (var key in scope.ExpresionesFunciones.Keys)
                NuevoScope.ExpresionesFunciones.Add(key, scope.ExpresionesFunciones[key]);
            foreach (var key in scope.Parametros.Keys)
                NuevoScope.Parametros.Add(key, scope.Parametros[key]);
            for (int recorrer = 0; recorrer < Let.Count; recorrer++)
            {
                if (!Let[recorrer].ChequeoSemantico(NuevoScope, listaerrores))
                { listaerrores.Add(new CompilingError(new CodeLocation(), ErrorCode.Invalid, "")); return false; }
                
            }
            if (!In.ChequeoSemantico(NuevoScope, listaerrores))
            { listaerrores.Add(new CompilingError(new CodeLocation(), ErrorCode.Invalid, "")); return false; }
            return true;
        }
        public override objetos Evaluar(Scope scope, Canvas canvas)
        {
            NuevoScope = new Scope(null);
            foreach (var key in scope.Variables.Keys)
                NuevoScope.Variables.Add(key, scope.Variables[key]);
            foreach (var key in scope.Objetos.Keys)
                NuevoScope.Objetos.Add(key, scope.Objetos[key]);
            foreach (var key in scope.ExpresionesFunciones.Keys)
                NuevoScope.ExpresionesFunciones.Add(key, scope.ExpresionesFunciones[key]);
            foreach (var key in scope.Parametros.Keys)
                NuevoScope.Parametros.Add(key, scope.Parametros[key]);
                for (int recorrer = 0; recorrer < Let.Count; recorrer++)
            {

                Let[recorrer].Ejecutar(NuevoScope, canvas);
            }

            return  Value=  In.Evaluar(NuevoScope,canvas);
        }
    }
}
