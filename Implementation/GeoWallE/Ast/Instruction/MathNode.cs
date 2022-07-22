using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeoWallE.Ast;
using GeoWallE.Ast.Expression.Value;
using Compiling;
using GeoWallE.Ast.Expression;

namespace GeoWallE.Ast.Instruction
{
    public class MathNode : InstruccionNode
    {
        public List<string> VariablesAAsignar;
        public ExpressionNode Expresion;
        public MathNode(List<string> variables, ExpressionNode expresion)
        {
            VariablesAAsignar = variables;
            Expresion = expresion;
        }
        public override bool ChequeoSemantico(Scope scope, List<CompilingError> listaerrores)
        {
            for (int recorrer = 0; recorrer < VariablesAAsignar.Count; recorrer++)
            {
                if (Metodos_Importantes.EstaEnelScopePadreVariables(scope, VariablesAAsignar[recorrer]))
                {
                    listaerrores.Add(new CompilingError(new CodeLocation(), ErrorCode.Invalid, "Ya esta en el scope"));
                    return false;
                }
            }
            for (int recorrer = 0; recorrer < VariablesAAsignar.Count; recorrer++)
            {
                if(VariablesAAsignar[recorrer]!= "Underscore")
                scope.Variables.Add(VariablesAAsignar[recorrer], null);
            }
            return true;
        }
        public override void Ejecutar(Scope scope, Canvas canvas)
        {
            if (Expresion is SequenceNode)
                IFesSecuenciaFinita(scope, canvas);
            else if (Expresion is InfinitySequenceNode)
                IfesSecuenciaInfinita(scope, canvas);
            else if (Expresion is IntersectNode)
            {
                IFessIntersecteNode(scope, canvas);
            }
        }
        public void IFesSecuenciaFinita(Scope scope, Canvas canvas)
        {
            int contador = 0;
            var secuencia = Expresion.Evaluar(scope, canvas);
            if (secuencia is SecuenciaFinita)
            {
                if (((SecuenciaFinita)secuencia).Secuencia.Count >= VariablesAAsignar.Count)
                {
                    for( int i= 0; i<VariablesAAsignar.Count; i++)
                    {
                        if(VariablesAAsignar[i]!= "Underscore")
                        {
                            scope.Objetos.Add(VariablesAAsignar[i],((SecuenciaFinita)secuencia).Secuencia[i]);
                        }
                    }
                }
                else
                {
                    for(int i= 0; i< ((SecuenciaFinita)secuencia).Secuencia.Count;i++ )
                    {
                        if (VariablesAAsignar[i] != "Underscore")
                        {
                            contador++;
                            scope.Objetos.Add(VariablesAAsignar[i], ((SecuenciaFinita)secuencia).Secuencia[i]);
                        }
                        else
                        {
                            contador++;
                        }
                    }
                    for(int i= contador; i < VariablesAAsignar.Count; i++)
                    {
                        scope.Objetos.Add(VariablesAAsignar[i], new Undefined());
                    }

                }
            }
        }
        public void IfesSecuenciaInfinita(Scope scope, Canvas canvas)
        {
            int contador = 0;
            List<Numero> secuenciaauxiliar = new List<Numero>();
            SecuenciaInfinita Secue = ((SecuenciaInfinita)Expresion.Evaluar(scope,canvas));
            foreach(var elementos in Secue.Secue)
            {
              if(contador == VariablesAAsignar.Count)
                    break;
                else
                { 
                    secuenciaauxiliar.Add(elementos);
                    contador++;
                }
            }
            for( int i= 0; i < VariablesAAsignar.Count; i++)
            {
                if (VariablesAAsignar[i] != "Underscore")
                    scope.Objetos.Add(VariablesAAsignar[i], secuenciaauxiliar[i]);
            }
         }
        public void IFessIntersecteNode(Scope scope, Canvas canvas)
        {
            int contador = 0;
            var secuencia = Expresion.Evaluar(scope, canvas);
            if (((SecuenciaFinita)secuencia).Secuencia.Count >= VariablesAAsignar.Count)
            {
                for (int i = 0; i < VariablesAAsignar.Count; i++)
                {
                    if (VariablesAAsignar[i] != "Underscore")
                    {
                        scope.Objetos.Add(VariablesAAsignar[i], ((SecuenciaFinita)secuencia).Secuencia[i]);
                    }
                }
            }
            else
            {
                for (int i = 0; i < ((SecuenciaFinita)secuencia).Secuencia.Count; i++)
                {
                    if (VariablesAAsignar[i] != "Underscore")
                    {
                        contador++;
                        scope.Objetos.Add(VariablesAAsignar[i], ((SecuenciaFinita)secuencia).Secuencia[i]);
                    }
                    else
                    {
                        contador++;
                    }
                }
                for (int i = contador; i < VariablesAAsignar.Count; i++)
                {
                    if (VariablesAAsignar[i] != "Underscore")
                        scope.Objetos.Add(VariablesAAsignar[i], new Undefined());
                }

            }
        }
    }
}
