using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Compiling;

namespace GeoWallE.Ast
{
   public class ProgramNode:AstNode
    {
        public List<InstruccionNode> Instrucciones { get; set; }
        public ProgramNode(List<InstruccionNode> listainstrucciones)
        {
            this.Instrucciones = listainstrucciones;
        }
        public override bool ChequeoSemantico(Scope scope,  List<CompilingError> listaerrores)
        {
            foreach (var instructionNode in Instrucciones)
            {
                if (!instructionNode.ChequeoSemantico(scope,listaerrores)) return false;
            }
            return true;
        }
        public void Ejecutar(Scope scope, Canvas canvas)
        {
            foreach(var a in Instrucciones)
            {
                a.Ejecutar(scope,canvas);
            }
        }
    }
}
