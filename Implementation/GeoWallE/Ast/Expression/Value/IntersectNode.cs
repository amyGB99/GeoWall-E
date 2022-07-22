using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Compiling;
using GeoWallE;

namespace GeoWallE.Ast.Expression.Value
{
    public class IntersectNode:ExpressionNode
    {
        #region Variables
        string Figura1;
        string Figura2;
        #endregion
        public IntersectNode(string figura1, string figura2)
        {
            Figura1 = figura1;
            Figura2 = figura2;
            TiposEnChequeo = TiposParaChequeo.Secuencia;
        }
        public override TiposParaChequeo TiposEnChequeo { get; set; }
        public override ExpressionTypes Types
        {
            get
            {
                return ExpressionTypes.ValueNode;
            }
        }
        public override bool ChequeoSemantico(Scope scope, List<CompilingError> listaerrores)
        {
            if (!scope.Variables.ContainsKey(Figura1)) { listaerrores.Add(new CompilingError(new CodeLocation(), ErrorCode.Invalid, "")); return false; }
            if (!scope.Variables.ContainsKey(Figura2)) { listaerrores.Add(new CompilingError(new CodeLocation(), ErrorCode.Invalid, "")); return false; }
            return true;
        }
        public override objetos Evaluar(Scope scope, Canvas canvas)
        {
            if (Value != null) return Value;
            else
            {
                if (scope.Objetos[Figura1] is Rayo && scope.Objetos[Figura2] is Rayo)
                    Value = Intersecciones.interceptarRayos((Rayo)scope.Objetos[Figura1], (Rayo)scope.Objetos[Figura2]);//rayo y rayo
                else if (scope.Objetos[Figura1] is Segmento && scope.Objetos[Figura2] is Segmento)
                    Value = Intersecciones.interceptarsegmentos((Segmento)scope.Objetos[Figura1], (Segmento)scope.Objetos[Figura2]);//segmento y segmento
                else if (scope.Objetos[Figura1] is Arco && scope.Objetos[Figura2] is Arco)
                    Value = Intersecciones.interceptarArcoYarco((Arco)scope.Objetos[Figura1], (Arco)scope.Objetos[Figura2]);//arco y arco
                else if (scope.Objetos[Figura1] is Circunferencia && scope.Objetos[Figura2] is Arco)
                    Value = Intersecciones.interceptarCircunferenciaArco((Circunferencia)scope.Objetos[Figura1], (Arco)scope.Objetos[Figura2]);//circunferencia y arco
                else if (scope.Objetos[Figura1] is Arco && scope.Objetos[Figura2] is Circunferencia)
                    Value = Intersecciones.interceptarArcoYCircunferencia((Arco)scope.Objetos[Figura1], (Circunferencia)scope.Objetos[Figura2]);//arco y circunferencia
                else if (scope.Objetos[Figura1] is Circunferencia && scope.Objetos[Figura2] is Circunferencia)
                    Value = Intersecciones.interceptarCircunferenciaYCircunferencia((Circunferencia)scope.Objetos[Figura1], (Circunferencia)scope.Objetos[Figura2]); //circunferencia y circunferencia
                else if (scope.Objetos[Figura1] is Arco && scope.Objetos[Figura2] is Segmento)
                    Value = Intersecciones.interceptarArcoYSegmento((Arco)scope.Objetos[Figura1], (Segmento)scope.Objetos[Figura2]);//arco y sgmento
                else if (scope.Objetos[Figura1] is Arco && scope.Objetos[Figura2] is Rayo)
                    Value = Intersecciones.interceptarArcoYRayo((Arco)scope.Objetos[Figura1], (Rayo)scope.Objetos[Figura2]);//arco y rayo
                else if (scope.Objetos[Figura1] is Rayo && scope.Objetos[Figura2] is Arco)
                    Value = Intersecciones.interceptarRayoYArco((Rayo)scope.Objetos[Figura1], (Arco)scope.Objetos[Figura2]);//rayo y arco
                else if (scope.Objetos[Figura1] is Segmento && scope.Objetos[Figura2] is Arco)
                    Value = Intersecciones.interceptarSegmentoYarco((Segmento)scope.Objetos[Figura1], (Arco)scope.Objetos[Figura2]);//segmento y arco
                else if (scope.Objetos[Figura1] is Arco && scope.Objetos[Figura2] is Recta)
                    Value = Intersecciones.interceptarArcoYRecta((Arco)scope.Objetos[Figura1], (Recta)scope.Objetos[Figura2]);//arco y recta
                else if (scope.Objetos[Figura1] is Recta && scope.Objetos[Figura2] is Arco)
                    Value = Intersecciones.interceptarRectaYArco((Recta)scope.Objetos[Figura1], (Arco)scope.Objetos[Figura2]);// recta y arco
                else if (scope.Objetos[Figura1] is Rayo && scope.Objetos[Figura2] is Circunferencia)
                    Value = Intersecciones.interceptarRayoYcircunferencia((Rayo)scope.Objetos[Figura1], (Circunferencia)scope.Objetos[Figura2]);//rayo y circunferencia
                else if (scope.Objetos[Figura1] is Circunferencia && scope.Objetos[Figura2] is Rayo)
                    Value = Intersecciones.interceptarCircunferenciaYRayo((Circunferencia)scope.Objetos[Figura1], (Rayo)scope.Objetos[Figura2]);//circunferencia y rayo
                else if (scope.Objetos[Figura1] is Segmento && scope.Objetos[Figura2] is Circunferencia)
                    Value = Intersecciones.interceptarSegmentoYCircunferencia((Segmento)scope.Objetos[Figura1], (Circunferencia)scope.Objetos[Figura2]);//segmento y circunferencia
                else if (scope.Objetos[Figura1] is Circunferencia && scope.Objetos[Figura2] is Segmento)
                    Value = Intersecciones.interceptarCircunferenciaYSegmento((Circunferencia)scope.Objetos[Figura1], (Segmento)scope.Objetos[Figura2]);//circunferencia y segmento
                else if (scope.Objetos[Figura1] is Circunferencia && scope.Objetos[Figura2] is Recta)
                    Value = Intersecciones.interceptarCircunferenciaYRecta((Circunferencia)scope.Objetos[Figura1], (Recta)scope.Objetos[Figura2]); // circunferencia y recta
                else if (scope.Objetos[Figura1] is Recta && scope.Objetos[Figura2] is Circunferencia)
                    Value = Intersecciones.interceptarRectaYCircunferencia((Recta)scope.Objetos[Figura1], (Circunferencia)scope.Objetos[Figura2]);//recta y circunferencia
                else if (scope.Objetos[Figura1] is Rayo && scope.Objetos[Figura2] is Segmento)
                    Value = Intersecciones.interceptarRayoYSegmento((Rayo)scope.Objetos[Figura1], (Segmento)scope.Objetos[Figura2]);//rayo y segmento
                else if (scope.Objetos[Figura1] is Segmento && scope.Objetos[Figura2] is Rayo)
                    Value = Intersecciones.interceptarSegmentoYRayo((Segmento)scope.Objetos[Figura1], (Rayo)scope.Objetos[Figura2]);//segmento y rayo
                else if (scope.Objetos[Figura1] is Recta && scope.Objetos[Figura2] is Segmento)
                    Value = Intersecciones.interceptarRectaYSegmento((Recta)scope.Objetos[Figura1], (Segmento)scope.Objetos[Figura2]);//recta y segmento
                else if (scope.Objetos[Figura1] is Segmento && scope.Objetos[Figura2] is Recta)
                    Value = Intersecciones.interceptarSegmentoYRecta((Segmento)scope.Objetos[Figura1], (Recta)scope.Objetos[Figura2]);
                else if (scope.Objetos[Figura1] is Recta && scope.Objetos[Figura2] is Rayo)
                    Value = Intersecciones.interceptarRectaYrayo((Recta)scope.Objetos[Figura1], (Rayo)scope.Objetos[Figura2]);//recta y rayo
                else if (scope.Objetos[Figura1] is Rayo && scope.Objetos[Figura2] is Recta)
                    Value = Intersecciones.interceptarRayoYRecta((Rayo)scope.Objetos[Figura1], (Recta)scope.Objetos[Figura2]);//rayo y recta
                else if (scope.Objetos[Figura1] is Recta && scope.Objetos[Figura2] is Recta)
                    Value = Intersecciones.interceptarrectas((Recta)scope.Objetos[Figura1], (Recta)scope.Objetos[Figura2]); //recta y recta

                return Value;
            }
            throw new Exception();
        }
    }
}
