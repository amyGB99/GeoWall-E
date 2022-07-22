using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeoWallE.Ast.Expression;
using GeoWallE.Ast;
using GeoWallE.Ast.Expression.Value;


namespace GeoWallE
{
  public  class Scope
    {
        public Scope Padre { get; set; }

        public  Dictionary<string, ExpressionNode> Variables { get;  set; }
        public  Dictionary<string, objetos> Objetos { get;  set; }
        public Dictionary<string,ExpressionNode> ExpresionesFunciones { get; set; }
        public Dictionary<string,List<string>> Parametros { get; set; }

        public Scope(Scope Padre)
        {
            Variables = new Dictionary<string, ExpressionNode>();
            Objetos = new Dictionary<string, objetos >();
            ExpresionesFunciones = new Dictionary<string, ExpressionNode>();
            Parametros = new Dictionary<string, List<string>>();
            this.Padre = Padre;
        }
    }
}
