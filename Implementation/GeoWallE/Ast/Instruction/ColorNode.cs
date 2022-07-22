using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Compiling;

namespace GeoWallE.Ast.Instruction
{
    public class ColorNode : InstruccionNode
    {
        public string Color;
        public ColorNode(string color)
        {
            Color = color;
        }
        public override bool ChequeoSemantico(Scope scope, List<CompilingError> listaerrores)
        {
            return true;
        }

        public override void Ejecutar(Scope scope,Canvas canvas)
        {
            if (Equals(Color, "Black"))
                canvas.SetColor(System.Drawing.Color.Black);
            if (Equals(Color, "Red"))
                canvas.SetColor(System.Drawing.Color.Red);
            if (Equals(Color, "Yellow"))
                canvas.SetColor(System.Drawing.Color.Yellow);
            if (Equals(Color, "Blue"))
                canvas.SetColor(System.Drawing.Color.Blue);
            if (Equals(Color, "Magenta"))
                canvas.SetColor(System.Drawing.Color.Magenta);
            if(Equals(Color,"Cyan"))
               canvas.SetColor(System.Drawing.Color.Cyan);
            if (Equals(Color, "cyan"))
                canvas.SetColor(System.Drawing.Color.Cyan);
            if (Equals(Color, "Green"))
                canvas.SetColor(System.Drawing.Color.Green);
            if (Equals(Color, "Gray"))
                canvas.SetColor(System.Drawing.Color.Gray);

        }
    }
}
