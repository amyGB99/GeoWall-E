using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeoWallE.Ast.Expression;
using Compiling;

namespace GeoWallE.Ast.Instruction
{
    public class DrawNode : InstruccionNode
    {
        public ExpressionNode Figura { get; set; }
        public string NombreFigura;
        public DrawNode(ExpressionNode figura)
        {
            Figura = figura;
        }
        public DrawNode(string a)
        {
            NombreFigura = a;
        }
        public override bool ChequeoSemantico(Scope scope, List<CompilingError> listaerrores)
        {
            if (NombreFigura != null)
            {
                if (!scope.Variables.ContainsKey(NombreFigura)) { listaerrores.Add(new CompilingError(new CodeLocation(), ErrorCode.Invalid, "")); return false; }
                return true;
            }
            else
            {
                if (!Figura.ChequeoSemantico(scope, listaerrores)) { listaerrores.Add(new CompilingError(new CodeLocation(), ErrorCode.Invalid, "")); return false; }
                return true;
            }

        }
        public override void Ejecutar(Scope scope, Canvas canvas)
        {
            if (NombreFigura != null)
            {
                objetos objeto = Metodos_Importantes.BuscarEnScopeObjetos(NombreFigura, scope);
                if (objeto is Figura)
                    ((Figura)objeto).Dibujar(canvas);
                else if (objeto is SecuenciaFinita)
                    ((SecuenciaFinita)objeto).Dibujar(canvas);
                else
                {
                    objetos objetos = Figura.Evaluar(scope, canvas);
                    objeto.Dibujar(canvas);
                }
            }
            else
            {
                if (Figura.Types == ExpressionTypes.ValueNode)
                {
                    if (Figura.TiposEnChequeo == TiposParaChequeo.Secuencia)
                    {
                        SecuenciaFinita secuencia = (SecuenciaFinita)Figura.Evaluar(scope, canvas);
                        secuencia.Dibujar(canvas);
                    }
                    if (Figura.TiposEnChequeo == TiposParaChequeo.Figura)
                    {
                        Figura figura = (Figura)Figura.Evaluar(scope, canvas);
                        figura.Dibujar(canvas);
                    }
                }
                else
                {
                    objetos objeto = Figura.Evaluar(scope, canvas);
                    objeto.Dibujar(canvas);
                }
            }
        }
    }
    }

