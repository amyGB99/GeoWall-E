using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeoWallE;
using GeoWallE.Ast.Expression.Value;
using GeoWallE.Ast.Expression;
using GeoWallE.Ast;
using Compiling;
namespace GeoWallE
{
    public static class Metodos_Importantes
    {
        #region SaberSiEstaLaVariableEnElScope
        public static bool EstaEnelScopePadreVariables(Scope scope, string Nombre)
        {
            if (scope.Variables.ContainsKey(Nombre))
                return true;
            else if (scope.Padre != null)
            {
                if (scope.Padre.Variables.ContainsKey(Nombre))
                    return true;
                return EstaEnelScopePadreVariables(scope.Padre, Nombre);
            }
            return false;

        }
        public static bool EstaEnelScopePadreParametros(Scope scope, string Nombre)
        {
            if (scope.Variables.ContainsKey(Nombre))
                return true;
            else if (scope.Padre != null)
            {
                if (scope.Padre.Variables.ContainsKey(Nombre))
                    return true;
                return EstaEnelScopePadreParametros(scope.Padre, Nombre);
            }
            return false;

        }
        public static bool EstaEnelScopePadreObjetos(Scope scope, string Nombre)
        {
            if (scope.Variables.ContainsKey(Nombre))
                return true;
            else if (scope.Padre != null)
            {
                if (scope.Padre.Variables.ContainsKey(Nombre))
                    return true;
                return EstaEnelScopePadreObjetos(scope.Padre, Nombre);
            }
            return false;

        }
        public static bool EstaEnelScopePadreExpresionesFunciaones(Scope scope, string Nombre)
        {
            if (scope.Variables.ContainsKey(Nombre))
                return true;
            else if (scope.Padre != null)
            {
                if (scope.Padre.Variables.ContainsKey(Nombre))
                    return true;
                return EstaEnelScopePadreExpresionesFunciaones(scope.Padre, Nombre);
            }
            return false;

        }
        #endregion
        #region MetodosQueDevuelvenLosValoresdelScope
        public static ExpressionNode BuscarEnScopeVariables(string Nombre, Scope scope)
        {
            if (scope.Variables.ContainsKey(Nombre))
            {
                return scope.Variables[Nombre];
            }
            else if (scope.Padre != null)
            {
                if (scope.Padre.Variables.ContainsKey(Nombre))
                    return scope.Padre.Variables[Nombre];
                else return BuscarEnScopeVariables(Nombre, scope.Padre);
            }
            return null;
        }
        public static List<string> BuscarEnScopeParametros(string Nombre, Scope scope)
        {
            if (scope.Parametros.ContainsKey(Nombre))
            {
                return scope.Parametros[Nombre];
            }
            else if (scope.Padre != null)
            {
                if (scope.Padre.Parametros.ContainsKey(Nombre))
                    return scope.Padre.Parametros[Nombre];
                else return BuscarEnScopeParametros(Nombre, scope.Padre);
            }
            return null;
        }
        public static ExpressionNode BuscarEnScopeExpresionesFunciones(string Nombre, Scope scope)
        {
            if (scope.ExpresionesFunciones.ContainsKey(Nombre))
            {
                return scope.ExpresionesFunciones[Nombre];
            }
            else if (scope.Padre != null)
            {
                if (scope.Padre.ExpresionesFunciones.ContainsKey(Nombre))
                    return scope.Padre.ExpresionesFunciones[Nombre];
                else return BuscarEnScopeExpresionesFunciones(Nombre, scope.Padre);
            }
            return null;
        }
        public static objetos BuscarEnScopeObjetos(string Nombre, Scope scope)
        {
            if (scope.Objetos.ContainsKey(Nombre))
            {
                return scope.Objetos[Nombre];
            }
            else if (scope.Padre != null)
            {
                if (scope.Padre.Objetos.ContainsKey(Nombre))
                    return scope.Padre.Objetos[Nombre];
                else return BuscarEnScopeObjetos(Nombre, scope.Padre);
            }
            return null;
        }
        #endregion
        #region Metodosauxiliares
        public static bool CantidadDeParametro(Scope scope, List<ExpressionNode> parametros, string Nombre)
        {
            List<string> losParametrosoriginales = new List<string>();
            losParametrosoriginales = BuscarEnScopeParametros(Nombre, scope);
            if (losParametrosoriginales.Count == parametros.Count) return true;
            return false;

        }
        public static TiposParaChequeo  Sutipo(Scope scope, string Nombre)
        {
            if (scope.Variables.ContainsKey(Nombre))
            {
                foreach (var llaves in scope.Variables.Keys)
                {
                    if (scope.Variables[llaves] is NumberNode)
                        return TiposParaChequeo.Number;
                    if (scope.Variables[llaves] is SequenceNode)
                        return TiposParaChequeo.Secuencia;
                    if (scope.Variables[llaves] is MeasureNode)
                        return TiposParaChequeo.Number;
                    if (scope.Variables[llaves] is TextNode)
                        return TiposParaChequeo.Text;
                    if (scope.Variables[llaves] is ValueNode)
                        return TiposParaChequeo.Figura;
                    if (scope.Variables[llaves] is LetNode)
                        return ((LetNode)scope.Variables[llaves]).In.TiposEnChequeo;
                }
            }
            else if (scope.Padre != null)
            {
                if (scope.Padre.Variables.ContainsKey(Nombre))
                {
                    foreach (var llaves in scope.Padre.Variables.Keys)
                    {
                        if (scope.Padre.Variables[llaves] is NumberNode)
                            return TiposParaChequeo.Number;
                        if (scope.Padre.Variables[llaves] is SequenceNode)
                            return TiposParaChequeo.Secuencia;
                        if (scope.Padre.Variables[llaves] is MeasureNode)
                            return TiposParaChequeo.Number;
                        if (scope.Padre.Variables[llaves] is TextNode)
                            return TiposParaChequeo.Text;
                        if (scope.Padre.Variables[llaves] is ValueNode)
                            return TiposParaChequeo.Figura;
                    }
                    return Sutipo(scope.Padre, Nombre);
                }
            }
            throw new Exception();
        }
        #endregion
    }
}
