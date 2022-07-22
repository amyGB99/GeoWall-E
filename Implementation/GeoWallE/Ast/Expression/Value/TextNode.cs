using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Compiling;

namespace GeoWallE.Ast.Expression.Value
{
    public class TextNode : ValueNode
    {
        public TextNode(string texto)
        {
            Value = new Text( texto);
            TiposEnChequeo = TiposParaChequeo.Figura;
        }
        public override TiposParaChequeo TiposEnChequeo { get; set; }
        public override ExpressionTypes Types
        {
            get
            {
                throw new NotImplementedException();
            }
        }
        public override bool ChequeoSemantico(Scope scope, List< CompilingError> listaerrores)
        {
            return true; 
        }
        public override objetos Evaluar(Scope scope, Canvas canvas)
        {
            return Value;
        }
    }
}
