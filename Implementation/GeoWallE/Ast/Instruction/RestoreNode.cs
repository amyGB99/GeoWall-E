using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Compiling;

namespace GeoWallE.Ast.Instruction
{
    public class RestoreNode : InstruccionNode
    {
        public System.Drawing.Color Color;
        public RestoreNode(System.Drawing.Color color)
        {
            Color = color;
        }
        public override bool ChequeoSemantico(Scope scope,List< CompilingError> listaerrores)
        {
            return true;
        }

        public override void Ejecutar(Scope scope,Canvas canvas)
        {
            canvas.SetColor(Color);
        }
    }
}
