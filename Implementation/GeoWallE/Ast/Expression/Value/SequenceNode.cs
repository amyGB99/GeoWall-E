using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Compiling;

namespace GeoWallE.Ast.Expression.Value
{
    public class SequenceNode : ValueNode
    {
        #region Variables
        public List<ExpressionNode> Secuencia;
        #endregion
        public override ExpressionTypes Types
        {
            get
            {
                return ExpressionTypes.ValueNode;
            }
        }
        public override TiposParaChequeo TiposEnChequeo { get; set; }
        public SequenceNode(List<ExpressionNode> expresion)
        {
            Secuencia = expresion;
            TiposEnChequeo = TiposParaChequeo.Secuencia;
        }
        public SequenceNode(string empieza, string termina)//para los numeros
        {
            Value = new SecuenciaFinita(empieza, termina);
        }
        public override bool ChequeoSemantico(Scope scope,List< CompilingError> listaerrores)
        {
            //foreach (var elementos in Secuencia)
            //{
            //    if (elementos is RefNode)
            //    { if (!(scope.Variables.ContainsKey()) { listaerrores.Add(new CompilingError(new CodeLocation(), ErrorCode.Invalid, "Las variables no estan creadas")); return false; }
            //    }
            //}
            return true;
        }
        public override objetos Evaluar(Scope scope, Canvas canvas)
        {
            if (Value != null) return Value;// es una secuencia de la forma {1..2}
            else
            {
                List<objetos> secue = new List<objetos>();
                for (int recorrer = 0; recorrer < Secuencia.Count; recorrer++)
                    secue.Add(Secuencia[recorrer].Evaluar(scope, canvas));
                Value = new SecuenciaFinita(secue);
                return Value;
            }
        }
    }
}
