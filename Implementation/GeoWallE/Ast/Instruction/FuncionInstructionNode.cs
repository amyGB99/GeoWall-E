using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Compiling;

namespace GeoWallE.Ast.Instruction
{
    public class FuncionInstructionNode : InstruccionNode
    {
        #region Variables
        public List<string> ListaParametros;
        public ExpressionNode Expresion;
        public string Nombre;
        public Scope Nuevoscope;
        #endregion
        public FuncionInstructionNode(List<string> parametros, ExpressionNode expresion, string nombre)
        {
            ListaParametros = parametros;
            Expresion = expresion;
            Nombre = nombre;
        }
        public override bool ChequeoSemantico(Scope scope, List<CompilingError> listaerrores)
        {
            if (Metodos_Importantes.EstaEnelScopePadreVariables(scope, Nombre)) { listaerrores.Add(new CompilingError(new CodeLocation(), ErrorCode.Invalid, "")); return false; }
            scope.Parametros.Add(Nombre, ListaParametros);
            scope.ExpresionesFunciones.Add(Nombre, Expresion);
            scope.Variables.Add(Nombre, null);
            
            //for (int j = 0; j < ListaParametros.Count; j++)
            //{
            //    scope.Variables.Add(ListaParametros[j], null);
            //}

                return true;
        }
        public override void Ejecutar(Scope scope, Canvas canvas)
        {

        }
    }
}
