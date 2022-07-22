using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeoWallE.Ast.Expression.Value;
using GeoWallE.Ast.Expression;
using Compiling;

namespace GeoWallE.Ast.Instruction
{
    public class DeclarationNode : InstruccionNode
    {
        public ExpressionNode Expresion { get; set; }
        public string Nombre { get; set; }
        public DeclarationNode(ExpressionNode expresion, string nombre)
        {
            Expresion = expresion;
            Nombre = nombre;
        }
        public override bool ChequeoSemantico(Scope scope, List<CompilingError> listaerrores)
        {
            if (scope.Variables.ContainsKey(Nombre))
            {
                { listaerrores.Add(new CompilingError(new CodeLocation(), ErrorCode.Invalid, "")); return false; }
            }
            else
            {
                if (!Expresion.ChequeoSemantico(scope,listaerrores))
                {
                    { listaerrores.Add(new CompilingError(new CodeLocation(), ErrorCode.Invalid, "")); return false; }
                }
                
                scope.Variables.Add(Nombre, Expresion); return true;
            }
        }     

        public override void Ejecutar(Scope scope, Canvas canvas)
        {
          scope.Objetos.Add(Nombre, Expresion.Evaluar(scope,canvas));
        }
    }
}
