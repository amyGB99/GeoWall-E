using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Compiling;
using GeoWallE.Ast.Expression;
using GeoWallE.Ast;
using GeoWallE.Ast.Expression.Binary;
using GeoWallE.Ast.Expression.Unitary;
using GeoWallE.Ast.Expression.Value;
using GeoWallE.Ast.Instruction;
using GeoWallE.Ast.Expression.Ternary;
using GeoWallE;
using System.IO;



namespace GeoWallE
{
    public class Parsing
    {
        #region Propiedades
        public  List<Token> ArrayToken { get; private set; }
        public int ContadorPrincipal { get; set; }
        public Stack<System.Drawing.Color> Colores { get; set; }
        public List<CompilingError> ListaErrores { get; set; }
        #endregion
        #region ConstructorClaseParsing
        public Parsing(List<Token> token, List<CompilingError> listaerores)
        {
            Colores = new Stack<System.Drawing.Color>();
            Colores.Push(System.Drawing.Color.Black);
            ArrayToken = token;
            ContadorPrincipal = 0;
            ListaErrores = listaerores;
        }
        #endregion
        #region Parse
        #region ParseInstrucciones
        public ProgramNode ParsingProgramNode(int hastadonde)
        {
            List<Ast.InstruccionNode> Listainstrucciones = new List<Ast.InstruccionNode>();
            if(ArrayToken[ContadorPrincipal].Value== TokenValues.Import)
            {
                var instrucciones = ParsingImport();
                foreach (var instruccion in instrucciones )
                {
                    Listainstrucciones.Add(instruccion); ;
                }
            }
            while (ContadorPrincipal < hastadonde)
            {
                Ast.InstruccionNode instruccion = ParsingInstruccionNode();
                Listainstrucciones.Add(instruccion);
            }
            return new ProgramNode(Listainstrucciones);
        }
        public InstruccionNode ParsingInstruccionNode()
        {
            Token token = ArrayToken[ContadorPrincipal];
            if (token.Value == TokenValues.Underscore)
            {
                return ParsingIdentifierInstruccion();
            }
            if (token.Type == TokenType.Keyword)
            {
                return ParsingKeywordInstruccion();
            }
            if (token.Type == TokenType.Identifier)
            {
                return ParsingIdentifierInstruccion();
            }
            
            ListaErrores.Add(new CompilingError(new CodeLocation(), ErrorCode.Invalid, ""));
            ContadorPrincipal++;
            return null;
        }
        public List<InstruccionNode> ParsingImport()
        {
            string nombre = ArrayToken[ContadorPrincipal + 1].Value;
            ContadorPrincipal += 3;
            if (!File.Exists(nombre))
            {
                ListaErrores.Add(new CompilingError(new CodeLocation(), ErrorCode.Invalid, "El archivonoexixte"));
                    return null;
            }
            else
            {
                var listatoKen = Compiling.Lexical.GetTokens(nombre, File.ReadAllText(nombre), ListaErrores);
                Parsing nuevoparse = new Parsing(listatoKen, ListaErrores);
                ProgramNode program = nuevoparse.ParsingProgramNode(listatoKen.Count);
                return program.Instrucciones;
            }
        }
        public InstruccionNode ParsingKeywordInstruccion()
        {

            if (ArrayToken[ContadorPrincipal].Value == TokenValues.Color)
            {
                return ParsingColor();

            }
            if (ArrayToken[ContadorPrincipal].Value == TokenValues.Restore)
            {
                ContadorPrincipal += 2;
                return ParsingRestore();
            }
            if (ArrayToken[ContadorPrincipal].Value == TokenValues.Draw)
            {
                return ParsingDrawNode();
            }
            if (ArrayToken[ContadorPrincipal].Value == TokenValues.Point)
            {
                int posiciondelcontador = SaberLaPoisicionFinalINstruccion();
                string nombre = ArrayToken[ContadorPrincipal + 1].Value;
                var punto = ParsingPoint();//el contador queda en el punto y coma esto ocurrre en el parse de todas las figuras
                ContadorPrincipal = posiciondelcontador + 1;
                return new DeclarationNode(punto, nombre);
            }
            if (ArrayToken[ContadorPrincipal].Value == TokenValues.Line)
            {
                int posiciondelcontador = SaberLaPoisicionFinalINstruccion();
                string nombre = ArrayToken[ContadorPrincipal + 1].Value;
                var recta = ParsingLine();
                ContadorPrincipal = posiciondelcontador + 1;
                return new DeclarationNode(recta, nombre);
            }
            if (ArrayToken[ContadorPrincipal].Value == TokenValues.Arc)
            {
                int posiciondelcontador = SaberLaPoisicionFinalINstruccion();
                string nombre = ArrayToken[ContadorPrincipal + 1].Value;
                var arco = ParsingArc();
                ContadorPrincipal = posiciondelcontador + 1;
                return new DeclarationNode(arco, nombre);
            }
            if (ArrayToken[ContadorPrincipal].Value == TokenValues.Circle)
            {
                int posiciondelcontador = SaberLaPoisicionFinalINstruccion();
                string nombre = ArrayToken[ContadorPrincipal + 1].Value;
                var circunferencia = ParsingCircle();
                ContadorPrincipal = posiciondelcontador + 1;
                return new DeclarationNode(circunferencia, nombre);
            }
            if (ArrayToken[ContadorPrincipal].Value == TokenValues.Segment)
            {
                int posiciondelcontador = SaberLaPoisicionFinalINstruccion();
                string nombre = ArrayToken[ContadorPrincipal + 1].Value;
                var segmento = ParsingSegment();
                ContadorPrincipal = posiciondelcontador + 1;
                return new DeclarationNode(segmento, nombre);
            }
            if (ArrayToken[ContadorPrincipal].Value == TokenValues.Ray)
            {
                int posiciondelcontador = SaberLaPoisicionFinalINstruccion();
                string nombre = ArrayToken[ContadorPrincipal + 1].Value;
                var rayo = ParsingRay();
                ContadorPrincipal = posiciondelcontador + 1;
                return new DeclarationNode(rayo, nombre);
            }
            ListaErrores.Add(new CompilingError(new CodeLocation(), ErrorCode.Invalid, ""));
            ContadorPrincipal++;
            return null;
        }
        public InstruccionNode ParsingIdentifierInstruccion()
        {
            if (ArrayToken[ContadorPrincipal + 1].Value != TokenValues.OpenBracket && ArrayToken[ContadorPrincipal + 1].Value != TokenValues.ValueSeparator && ArrayToken[ContadorPrincipal+1].Value== TokenValues.Assign)
            {
                string nombre = ArrayToken[ContadorPrincipal].Value;
                ContadorPrincipal += 2;
                int pos = SaberLaPoisicionFinalINstruccion();
                var i = ParsingExpressionNode(pos);
                ContadorPrincipal = pos + 1;
                return new DeclarationNode(i, nombre);
            }
            if (ArrayToken[ContadorPrincipal + 1].Value == TokenValues.OpenBracket)
            {
                return ParsingFuncionInstruction();
            }
            if (ArrayToken[ContadorPrincipal + 1].Value == TokenValues.ValueSeparator)
            {
                return ParsingMathNode();
            }
            ListaErrores.Add(new CompilingError(new CodeLocation(), ErrorCode.Invalid, "Falta algo en el codigo"));
            int posicion = SaberLaPoisicionFinalINstruccion();
            ContadorPrincipal = posicion + 1;
            return null;
        }
        public InstruccionNode ParsingDrawNode()
        {
            int posciciondelcontador = SaberLaPoisicionFinalINstruccion();
            if (ArrayToken[ContadorPrincipal + 1].Type == TokenType.Identifier && ArrayToken[ContadorPrincipal + 2].Value != TokenValues.OpenBracket)
            {
                string a = ArrayToken[ContadorPrincipal + 1].Value;
                ContadorPrincipal = posciciondelcontador + 1;
                return new DrawNode(a);
            }
            if (ArrayToken[ContadorPrincipal + 1].Type == TokenType.Keyword)
            {
                ContadorPrincipal++;//aqui paarsea desde el tipos de la figura
                var figura = ParsingExpressionNode(posciciondelcontador);
                ContadorPrincipal = posciciondelcontador + 1;
                return new DrawNode(figura);
            }

            if (ArrayToken[ContadorPrincipal + 1].Value == TokenValues.OpenCurlyBraces)//de la forma draw{}
            {
                int terminalallave = TerminalaLLavefinal();
                ContadorPrincipal++;
                var i = ParsingExpressionNode(terminalallave);
                ContadorPrincipal = posciciondelcontador + 1;
                return new DrawNode(i);
            }
            if (ArrayToken[ContadorPrincipal + 1].Type == TokenType.Identifier && ArrayToken[ContadorPrincipal + 2].Value == TokenValues.OpenBracket)
            {
                ContadorPrincipal++;
                var i = ParsingExpressionNode(posciciondelcontador);
                ContadorPrincipal = posciciondelcontador + 1;
                return new DrawNode(i);
            }
            throw new Exception();

        }
        public InstruccionNode ParsingColor()
        {
            int finalinstruccion = SaberLaPoisicionFinalINstruccion();
            if (ArrayToken[ContadorPrincipal + 2].Value == TokenValues.StatementSeparator)
            {
                if (ArrayToken[ContadorPrincipal + 1].Value == TokenValues.Red)
                {
                    Colores.Push(System.Drawing.Color.Red);
                    ContadorPrincipal += 3;
                    return new ColorNode(ArrayToken[ContadorPrincipal - 2].Value);
                }
                if (ArrayToken[ContadorPrincipal + 1].Value == TokenValues.Yellow)
                {
                    Colores.Push(System.Drawing.Color.Yellow);
                    ContadorPrincipal += 3;
                    return new ColorNode(ArrayToken[ContadorPrincipal - 2].Value);
                }
                if (ArrayToken[ContadorPrincipal + 1].Value == TokenValues.Black)
                {
                    Colores.Push(System.Drawing.Color.Black);
                    ContadorPrincipal += 3;
                    return new ColorNode(ArrayToken[ContadorPrincipal - 2].Value);
                }
                if (ArrayToken[ContadorPrincipal + 1].Value == TokenValues.White)
                {
                    Colores.Push(System.Drawing.Color.White);
                    ContadorPrincipal += 3;
                    return new ColorNode(ArrayToken[ContadorPrincipal - 2].Value);
                }
                if (ArrayToken[ContadorPrincipal + 1].Value == TokenValues.Gray)
                {
                    Colores.Push(System.Drawing.Color.Gray);
                    ContadorPrincipal += 3;
                    return new ColorNode(ArrayToken[ContadorPrincipal - 2].Value);
                }
                if (ArrayToken[ContadorPrincipal + 1].Value == TokenValues.Green)
                {
                    Colores.Push(System.Drawing.Color.Green);
                    ContadorPrincipal += 3;
                    return new ColorNode(ArrayToken[ContadorPrincipal - 2].Value);
                }
                if (ArrayToken[ContadorPrincipal + 1].Value == TokenValues.Blue)
                {
                    Colores.Push(System.Drawing.Color.Blue);
                    ContadorPrincipal += 3;
                    return new ColorNode(ArrayToken[ContadorPrincipal - 2].Value);
                }
                if (ArrayToken[ContadorPrincipal + 1].Value == TokenValues.Magenta)
                {
                    Colores.Push(System.Drawing.Color.Magenta);
                    ContadorPrincipal += 3;
                    return new ColorNode(ArrayToken[ContadorPrincipal - 2].Value);
                }
            }
            ListaErrores.Add(new CompilingError(new CodeLocation(), ErrorCode.Invalid, ""));
            ContadorPrincipal = finalinstruccion + 1;
            return null;
        }
        public InstruccionNode ParsingRestore()
        {
            Colores.Pop();
            return new RestoreNode(Colores.Peek());
        }
        public InstruccionNode ParsingFuncionInstruction()
        {
            int parentesisfinalparametros;
            string nombre = ArrayToken[ContadorPrincipal].Value;
            ContadorPrincipal++;
            ContadorPrincipal++;//aqui el contador va a caer en los parametros
            List<string> parametros = new List<string>();
            parentesisfinalparametros = FinalParentesis();
            for (int recorrer = ContadorPrincipal; recorrer < parentesisfinalparametros; recorrer++)
            {
                if (ArrayToken[recorrer].Value != TokenValues.ValueSeparator)
                    parametros.Add(ArrayToken[recorrer].Value);
            }
            ContadorPrincipal = parentesisfinalparametros + 2;//esto es para que el contador caiga  despues del signo igual
            int posicionfinal = SaberLaPoisicionFinalINstruccion();
            ExpressionNode expresion = null;
            expresion = ParsingExpressionNode(posicionfinal);
            ContadorPrincipal = posicionfinal + 1;
            return new FuncionInstructionNode(parametros, expresion, nombre);
        }
        public InstruccionNode ParsingMathNode()
        {
            int posicion = SaberLaPoisicionFinalINstruccion();
            int posiciondeligual = DondeEstaelIgual(posicion);
            List<string> variables = new List<string>();
            List<string> lasecuencia = new List<string>();
            for (int recorrer = ContadorPrincipal; recorrer < posiciondeligual; recorrer++)
            {
                if (ArrayToken[recorrer].Value != TokenValues.ValueSeparator)
                {
                    variables.Add(ArrayToken[recorrer].Value);
                }
            }
              ContadorPrincipal = posiciondeligual; 
            if (ArrayToken[ContadorPrincipal +1 ].Value == TokenValues.OpenCurlyBraces)
            {
                int terminalallave = TerminalaLLavefinal();
                ContadorPrincipal++;
                var expresion = ParsingExpressionNode(terminalallave);
                ContadorPrincipal = posicion + 1;
                return new MathNode(variables, expresion);
            }
            else
            {
                ContadorPrincipal++;
                var expresion = ParsingExpressionNode(posicion);
                ContadorPrincipal = posicion + 1;
                return new MathNode(variables, expresion);
            }
        }
        #endregion
        #region ParseExpresiones
        public ExpressionNode ParsingKeywordExpression()
        {

            if (ArrayToken[ContadorPrincipal].Value == TokenValues.Ray)
            {
                return ParsingRay();
            }
            if (ArrayToken[ContadorPrincipal].Value == TokenValues.Line)
            {
                return ParsingLine();
            }
            if (ArrayToken[ContadorPrincipal].Value == TokenValues.Circle)
            {
                return ParsingCircle();
            }
            if (ArrayToken[ContadorPrincipal].Value == TokenValues.Segment)
            {
                return ParsingSegment();
            }
            if (ArrayToken[ContadorPrincipal].Value == TokenValues.Arc)
            {
                return ParsingArc();
            }
            if (ArrayToken[ContadorPrincipal].Value == TokenValues.Measure)
            {
                return ParsingMeasureNode();
            }
            if (ArrayToken[ContadorPrincipal].Value == TokenValues.If)
            {
                return ParsingCondicionalNode();
            }
            if (ArrayToken[ContadorPrincipal].Value == TokenValues.Let)
            {
                return ParsingLetNode();
            }
            if (ArrayToken[ContadorPrincipal].Value == TokenValues.intersect)
            {
                return ParsingIntersectNode();
            }
            ListaErrores.Add(new CompilingError(new CodeLocation(), ErrorCode.Invalid, "error en el parse "));
            ContadorPrincipal++;
            return null;
        }
        public ExpressionNode ParsingIdentifierExpression()
        {
            if (ArrayToken[ContadorPrincipal + 1].Value == TokenValues.OpenBracket)//Funciones
            {
                return ParsingFuncionExpression();
            }
            else // constante
            {
                string nombre = ArrayToken[ContadorPrincipal].Value;
                ContadorPrincipal += 1;
                return new RefNode(nombre);
            }
        }
        public ExpressionNode ParsingExpressionNode(int posicion)
        {
            Stack<ExpressionNode> salida = AlgoritmoShuntinYand(posicion);
            return LLenarArbol(salida);
        }
        public LetNode ParsingLetNode()
        {

            int pos = TerminaLet();
            ContadorPrincipal++;
            var i = ParsingProgramNode(pos);
            List<InstruccionNode> listalet = new List<InstruccionNode>();
            foreach (var instruccioneslet in i.Instrucciones)
            {
                listalet.Add(instruccioneslet);
            }
            ContadorPrincipal = pos;
            pos = TerminaIn();
            ContadorPrincipal++;
            var j = ParsingExpressionNode(pos);
            ContadorPrincipal = pos +1;
            return new LetNode(listalet, j);


        }
        public ConditionalNode ParsingCondicionalNode()
        {
            int pos = Terminaif();
            ContadorPrincipal++;
            var i = ParsingExpressionNode(pos);
            ContadorPrincipal = pos;
            pos = TerminaThen();
            ContadorPrincipal++;
            var j = ParsingExpressionNode(pos);
            ContadorPrincipal++;
            pos = TerminaElse();
            var k = ParsingExpressionNode(pos);
            ContadorPrincipal = pos;
            return new ConditionalNode(i, j, k);
        }
        public IntersectNode ParsingIntersectNode()
        {
            ContadorPrincipal++;
            int posiciondelacoma = AntesComaSoloParaLasFiguras();
            ContadorPrincipal++;
            var figura1 = ParsingExpressionNode(posiciondelacoma);
            int posiciondelparentesis = FinalParentesis();
            ContadorPrincipal++;
            var figura2 = ParsingExpressionNode(posiciondelparentesis);
            ContadorPrincipal = posiciondelparentesis + 1;
            return new IntersectNode(((Text)(figura1.Value)).Nombre, ((Text)(figura2.Value)).Nombre);
        }
        public ExpressionNode ParsingFuncionExpression()
        {
            string nombre = ArrayToken[ContadorPrincipal].Value;
            ContadorPrincipal++;
            ContadorPrincipal++;// el contador cae despues del parentesis
            int pos = FinalParentesis();
            List<ExpressionNode> parametros = new List<ExpressionNode>();
            for (int recorrer = ContadorPrincipal; recorrer < pos; recorrer++)
            {

                if (ArrayToken[recorrer].Value != TokenValues.ValueSeparator)
                {
                    int posiciondelascomas;
                    ExpressionNode parametro = null;
                    posiciondelascomas = EncontrarComaEnElArcoYFuncExpresion(pos);
                    if (posiciondelascomas == -1)
                    {
                        parametro = ParsingExpressionNode(pos);
                        recorrer = pos;
                        ContadorPrincipal++;

                    }
                    else
                    {

                        parametro = ParsingExpressionNode(posiciondelascomas);
                        recorrer = posiciondelascomas - 1;
                        ContadorPrincipal++;
                    }
                    parametros.Add(parametro);
                }

            }
            return new FuncionExpressionNode(nombre, parametros);
        }
        public ExpressionNode ParsingSequence(int posicion)
        {
            string dondeempieza = null;
            string dondetermina = null;
            ExpressionNode expresion = null;
           ContadorPrincipal++;// para que caiga despues de la llave;
            if (ArrayToken[ContadorPrincipal].Type == TokenType.Number)
            {
                dondeempieza = ArrayToken[ContadorPrincipal].Value;
                for (int i = ContadorPrincipal + 1; i < posicion; i++)
                {
                    if (ArrayToken[i].Value == TokenValues.Dots)
                    {
                        if (ArrayToken[i + 1].Type == TokenType.Number)
                        {
                            dondetermina = ArrayToken[i + 1].Value;
                            expresion = new SequenceNode(dondeempieza, dondetermina);
                            break;
                        }
                        else if (ArrayToken[i + 1].Value == TokenValues.ClosedCurlyBraces)
                        {
                            expresion = ParsingInfinitySequence(dondeempieza);
                            break;
                        }
                    }
                    else
                    {
                        expresion = ParsingSequenceNode(posicion);
                        break;
                    }
                }
            }
            else
            {
                expresion = ParsingSequenceNode(posicion);
            }
            ContadorPrincipal = posicion + 1;
            return expresion;
        }
        public SequenceNode ParsingSequenceNode(int posicion)
        {
            List<ExpressionNode> expresione = new List<ExpressionNode>();
            for (int recorrer = ContadorPrincipal; recorrer < posicion; recorrer++)
            {
                if (ArrayToken[ContadorPrincipal].Value != TokenValues.ClosedCurlyBraces && ArrayToken[ContadorPrincipal].Value != TokenValues.OpenCurlyBraces)
                {
                    int posi = Encontrarlacomaparasecuencia(posicion);
                    if (posi != -1)
                    {
                        ExpressionNode expresion = ParsingExpressionNode(posi);
                        expresione.Add(expresion);
                        ContadorPrincipal++;
                        recorrer = ContadorPrincipal - 1;
                        continue;
                    }
                    posi = TerminaLLave();
                    ExpressionNode expresiom = ParsingExpressionNode(posi);
                    expresione.Add(expresiom);
                    ContadorPrincipal++;
                    break;
                }
                ContadorPrincipal++; 
            }
            ContadorPrincipal = posicion ;
            return new SequenceNode(expresione);
        }
        public InfinitySequenceNode ParsingInfinitySequence(string empieza)
        {
            return new InfinitySequenceNode(empieza);
        }

        #endregion
        #region ParseFigurasDeMiLenguaje
        public PointNode ParsingPoint()
        {

            if (ArrayToken[ContadorPrincipal + 1].Type == TokenType.Identifier)
            {
                if (ArrayToken[ContadorPrincipal + 2].Value == TokenValues.StatementSeparator)
                {
                    string punto = ArrayToken[ContadorPrincipal + 1].Value;
                    ContadorPrincipal += 2;
                    return new PointNode(punto);
                }
                ListaErrores.Add(new CompilingError(new CodeLocation(), ErrorCode.Invalid, ""));
                return null;
            }
            ListaErrores.Add(new CompilingError(new CodeLocation(), ErrorCode.Invalid, ""));
            return null;
        }
        public SegmentNode ParsingSegment()
        {
            if (ArrayToken[ContadorPrincipal + 1].Type == TokenType.Identifier)
            {
                if (ArrayToken[ContadorPrincipal + 2].Value == TokenValues.StatementSeparator)
                {
                    string segmento = ArrayToken[ContadorPrincipal + 1].Value;
                    ContadorPrincipal += 2;
                    return new SegmentNode(segmento);
                }
                ListaErrores.Add(new CompilingError(new CodeLocation(), ErrorCode.Invalid, ""));
                return null;
            }
            else
            {//Va a quedar el contador despues del parentisis
                ContadorPrincipal++;
                int pos = AntesComaSoloParaLasFiguras();
                ContadorPrincipal++;
                var i = ParsingExpressionNode(pos);
                pos = FinalParentesis();
                ContadorPrincipal++;
                var j = ParsingExpressionNode(pos);
                ContadorPrincipal = pos + 1;
                return new SegmentNode(((Text)i.Value).Nombre, ((Text)j.Value).Nombre);
            }
        }
        public RayNode ParsingRay()
        {
            if (ArrayToken[ContadorPrincipal + 1].Type == TokenType.Identifier)
            {
                if (ArrayToken[ContadorPrincipal + 2].Value == TokenValues.StatementSeparator)
                {
                    string rayo = ArrayToken[ContadorPrincipal + 1].Value;
                    ContadorPrincipal += 2;
                    return new RayNode(rayo);
                }
                ListaErrores.Add(new CompilingError(new CodeLocation(), ErrorCode.Invalid, ""));
                return null;
            }
            else
            {//Va a quedar el contador despues del parentisis
                ContadorPrincipal++;
                int pos = AntesComaSoloParaLasFiguras();
                ContadorPrincipal++;
                var i = ParsingExpressionNode(pos);
                pos = FinalParentesis();
                ContadorPrincipal++;
                var j = ParsingExpressionNode(pos);
                ContadorPrincipal = pos + 1;
                return new RayNode(((Text)i.Value).Nombre, ((Text)j.Value).Nombre);
            }
        }
        public CircleNode ParsingCircle()
        {
            if (ArrayToken[ContadorPrincipal + 1].Type == TokenType.Identifier)
            {
                if (ArrayToken[ContadorPrincipal + 2].Value == TokenValues.StatementSeparator)
                {
                    string circunferencia = ArrayToken[ContadorPrincipal + 1].Value;
                    ContadorPrincipal += 2;
                    return new CircleNode(circunferencia);
                }
                ListaErrores.Add(new CompilingError(new CodeLocation(), ErrorCode.Invalid, ""));
                return null;
            }
            else
            {//Va a quedar el contador despues del parentisis
                ContadorPrincipal++;
                int pos = AntesComaSoloParaLasFiguras();
                ContadorPrincipal++;
                var i = ParsingExpressionNode(pos);
                pos = FinalParentesis();
                ContadorPrincipal++;
                var j = ParsingExpressionNode(pos);
                ContadorPrincipal = pos + 1;
                return new CircleNode(((Text)i.Value).Nombre, ((Text)j.Value).Nombre);
            }
        }
        public LineNode ParsingLine()
        {
            if (ArrayToken[ContadorPrincipal + 1].Type == TokenType.Identifier)
            {
                if (ArrayToken[ContadorPrincipal + 2].Value == TokenValues.StatementSeparator)
                {
                    string linea = ArrayToken[ContadorPrincipal + 1].Value;
                    ContadorPrincipal += 2;
                    return new LineNode(linea);
                }
                ListaErrores.Add(new CompilingError(new CodeLocation(), ErrorCode.Invalid, ""));
                return null;
            }
            else
            {//Va a quedar el contador despues del parentisis
                ContadorPrincipal++;
                int pos = AntesComaSoloParaLasFiguras();
                ContadorPrincipal++;
                var i = ParsingExpressionNode(pos);
                pos = FinalParentesis();
                ContadorPrincipal++;
                var j = ParsingExpressionNode(pos);
                ContadorPrincipal = pos + 1;
                return new LineNode(((Text)i.Value).Nombre, ((Text)j.Value).Nombre);
            }
        }
        public ArcNode ParsingArc()
        {
            if (ArrayToken[ContadorPrincipal + 1].Type == TokenType.Identifier)
            {
                if (ArrayToken[ContadorPrincipal + 2].Value == TokenValues.StatementSeparator)
                {
                    string arco = ArrayToken[ContadorPrincipal + 1].Value;
                    ContadorPrincipal += 2;
                    return new ArcNode(arco);
                }
                ListaErrores.Add(new CompilingError(new CodeLocation(), ErrorCode.Invalid, ""));
                return null;
            }
            else
            {
                //aumento dos veces el contador pues me hace falta que caiga despues del parentesis para que busque el final
                ContadorPrincipal += 1;
                ContadorPrincipal += 1;
                int posiciondelparentesisfinal = FinalParentesis();
                int posiciondelascomas = EncontrarComaEnElArcoYFuncExpresion(posiciondelparentesisfinal);
                var centro = ParsingExpressionNode(posiciondelascomas);
                ContadorPrincipal++;
                posiciondelascomas = EncontrarComaEnElArcoYFuncExpresion(posiciondelparentesisfinal);
                var punto1 = ParsingExpressionNode(posiciondelascomas);
                ContadorPrincipal++;
                posiciondelascomas = EncontrarComaEnElArcoYFuncExpresion(posiciondelparentesisfinal);
                var punto2 = ParsingExpressionNode(posiciondelascomas);
                ContadorPrincipal++;
                var radio = ParsingExpressionNode(posiciondelparentesisfinal);
                ContadorPrincipal = posiciondelparentesisfinal + 1;
                return new ArcNode(((Text)centro.Value).Nombre, ((Text)punto1.Value).Nombre, ((Text)punto2.Value).Nombre, ((Text)radio.Value).Nombre);
            }
        }
        public MeasureNode ParsingMeasureNode()
        {
            ContadorPrincipal += 1;
            int pos = AntesComaSoloParaLasFiguras();
            ContadorPrincipal++;
            var i = ParsingExpressionNode(pos);
            pos = FinalParentesis();
            ContadorPrincipal++;
            var j = ParsingExpressionNode(pos);
            ContadorPrincipal = pos + 1;
            return new MeasureNode(((Text)i.Value).Nombre, ((Text)j.Value).Nombre);
        }
        #endregion
        #endregion
        #region MetodosUtiles
        #region MetodosRelacionadosConElAlgoritmoShuntingYand
        public Stack<ExpressionNode> AlgoritmoShuntinYand(int posicion)
        {
            Stack<Token> Operadores = new Stack<Token>();
            Stack<ExpressionNode> Miembros = new Stack<ExpressionNode>();
            while (ContadorPrincipal < posicion)
            {
                if (ArrayToken[ContadorPrincipal].Value != TokenValues.Sub && ArrayToken[ContadorPrincipal].Value != TokenValues.Add && ArrayToken[ContadorPrincipal].Value != TokenValues.Mul
                    && ArrayToken[ContadorPrincipal].Value != TokenValues.Div && ArrayToken[ContadorPrincipal].Value != TokenValues.ClosedBracket && ArrayToken[ContadorPrincipal].Value != TokenValues.OpenBracket && ArrayToken[ContadorPrincipal].Value != TokenValues.And
                    && ArrayToken[ContadorPrincipal].Value != TokenValues.GreaterOrEquals && ArrayToken[ContadorPrincipal].Value != TokenValues.Sub
                    && ArrayToken[ContadorPrincipal].Value != TokenValues.Less && ArrayToken[ContadorPrincipal].Value != TokenValues.LessOrEquals
                    && ArrayToken[ContadorPrincipal].Value != TokenValues.Greater && ArrayToken[ContadorPrincipal].Value != TokenValues.Equals
                    && ArrayToken[ContadorPrincipal].Value != TokenValues.Or && ArrayToken[ContadorPrincipal].Value != TokenValues.NotEquals
                    && ArrayToken[ContadorPrincipal].Value != TokenValues.Not && ArrayToken[ContadorPrincipal].Value != TokenValues.Mod)
                {
                    ExpressionNode e = null;
                    if (ArrayToken[ContadorPrincipal].Type == TokenType.Number)
                    {
                        e = new NumberNode(new Numero(double.Parse(ArrayToken[ContadorPrincipal].Value)));
                        ContadorPrincipal++;
                        Miembros.Push(e);
                        continue;
                    }
                    if (ArrayToken[ContadorPrincipal].Type == TokenType.Identifier)
                    {
                        e = ParsingIdentifierExpression();
                        Miembros.Push(e);
                        continue;
                    }
                    else if (ArrayToken[ContadorPrincipal].Type == TokenType.Keyword)
                    {
                        e = ParsingKeywordExpression();
                        Miembros.Push(e);
                        continue;
                    }
                    else if (ArrayToken[ContadorPrincipal].Type == TokenType.Text)
                    {
                        e = new TextNode(ArrayToken[ContadorPrincipal].Value);
                        ContadorPrincipal++;
                        Miembros.Push(e);
                        continue;
                    }
                    else if (ArrayToken[ContadorPrincipal].Value == TokenValues.OpenCurlyBraces)
                    {
                        //     int pos = TerminaLLave();
                        e = ParsingSequence(posicion);
                        Miembros.Push(e);
                        continue;
                    }
                    Miembros.Push(e);
                }
                else if (ArrayToken[ContadorPrincipal].Value == TokenValues.OpenBracket)
                {
                    Operadores.Push(ArrayToken[ContadorPrincipal]);
                    ContadorPrincipal++;
                    continue;
                }
                else if (ArrayToken[ContadorPrincipal].Value == TokenValues.ClosedBracket)
                {
                    while (Operadores.Peek().Value != TokenValues.OpenBracket)
                    {
                        ExpressionNode e = null;
                        e = Math(Operadores.Pop());
                        Miembros.Push(e);
                        continue;

                    }
                    ContadorPrincipal++;
                    Operadores.Pop();

                }
                else
                {
                    ExpressionNode e = null;
                    if (Operadores.Count > 0)
                        while (  Operadores.Count > 0 && ArrayToken[ContadorPrincipal].Value != TokenValues.OpenBracket && Operadores.Peek().Value != TokenValues.OpenBracket && !Preferencia(ArrayToken[ContadorPrincipal], Operadores.Peek()) )
                        {
                            e = Math(Operadores.Peek());
                            Operadores.Pop();
                            Miembros.Push(e);
                            continue;
                        }
                    Operadores.Push(ArrayToken[ContadorPrincipal]);
                    ContadorPrincipal++;
                }
            }
            while (Operadores.Count > 0)
            {
                ExpressionNode e = null;
                e = Math(Operadores.Peek());
                Operadores.Pop();
                Miembros.Push(e);
                continue;

            }
            return Miembros;
        }
        public bool Preferencia(Token operador1, Token operador2)
        {
            if (operador1.Type == TokenType.Number || operador1.Type == TokenType.Identifier || operador1.Type == TokenType.Keyword)
                return true;
            if ((operador1.Value == TokenValues.Mod || operador1.Value == TokenValues.Div ||
                operador1.Value == TokenValues.Sub || operador1.Value == TokenValues.Add ||
                operador1.Value == TokenValues.Equals || operador1.Value == TokenValues.NotEquals ||
                operador1.Value == TokenValues.GreaterOrEquals || operador1.Value == TokenValues.Less ||
                operador1.Value == TokenValues.LessOrEquals) && operador2.Value == TokenValues.Not)
                return true;
            if ((operador1.Value == TokenValues.Mod || operador1.Value == TokenValues.Div ||
                operador1.Value == TokenValues.Sub || operador1.Value == TokenValues.Add ||
                operador1.Value == TokenValues.Equals || operador1.Value == TokenValues.NotEquals ||
                operador1.Value == TokenValues.GreaterOrEquals || operador1.Value == TokenValues.Less ||
                operador1.Value == TokenValues.LessOrEquals) && operador2.Value == TokenValues.And)
                return true;
            if ((operador1.Value == TokenValues.Mod || operador1.Value == TokenValues.Div ||
                operador1.Value == TokenValues.Sub || operador1.Value == TokenValues.Add ||
                operador1.Value == TokenValues.Equals || operador1.Value == TokenValues.NotEquals ||
                operador1.Value == TokenValues.GreaterOrEquals || operador1.Value == TokenValues.Less ||
                operador1.Value == TokenValues.LessOrEquals) && operador2.Value == TokenValues.Or)
                return true;
            if ((operador1.Value == TokenValues.Mod || operador1.Value == TokenValues.Div ||
                operador1.Value == TokenValues.Sub || operador1.Value == TokenValues.Add) && operador2.Value == TokenValues.LessOrEquals)
                return true;
            if ((operador1.Value == TokenValues.Mod || operador1.Value == TokenValues.Div ||
                operador1.Value == TokenValues.Sub || operador1.Value == TokenValues.Add) && operador2.Value == TokenValues.Less)
                return true;
            if ((operador1.Value == TokenValues.Mod || operador1.Value == TokenValues.Div ||
                operador1.Value == TokenValues.Sub || operador1.Value == TokenValues.Add) && operador2.Value == TokenValues.Greater)
                return true;
            if ((operador1.Value == TokenValues.Mod || operador1.Value == TokenValues.Div ||
                operador1.Value == TokenValues.Sub || operador1.Value == TokenValues.Add) && operador2.Value == TokenValues.GreaterOrEquals)
                return true;
            if ((operador1.Value == TokenValues.Mod || operador1.Value == TokenValues.Div ||
                operador1.Value == TokenValues.Sub || operador1.Value == TokenValues.Add) && operador2.Value == TokenValues.Equals)
                return true;
            if ((operador1.Value == TokenValues.Mod || operador1.Value == TokenValues.Div ||
                operador1.Value == TokenValues.Sub || operador1.Value == TokenValues.Add) && operador2.Value == TokenValues.NotEquals)
                return true;
            if ((operador1.Value == TokenValues.Mul || operador1.Value == TokenValues.Div) && operador2.Value == TokenValues.Sub)
                return true;
            if ((operador1.Value == TokenValues.Mul || operador1.Value == TokenValues.Div) && operador2.Value == TokenValues.Add)
                return true;
            return false;
        }
        public ExpressionNode Math(Token objeto)
        {
            if (objeto.Value == TokenValues.And)
                return new AndNode();
            if (objeto.Value == TokenValues.Add)
                return new AdditionNode();
            if (objeto.Value == TokenValues.Equals)
                return new EqualsNode();
            if (objeto.Value == TokenValues.Not)
                return new NotEqualsNode();
            if (objeto.Value == TokenValues.Less)
                return new LessNode();
            if (objeto.Value == TokenValues.LessOrEquals)
                return new LessOrEqualsNode();
            if (objeto.Value == TokenValues.Greater)
                return new GreaterNode();
            if (objeto.Value == TokenValues.GreaterOrEquals)
                return new GreaterOrEqualsNode();
            if (objeto.Value == TokenValues.Or)
                return new OrNode();
            if (objeto.Value == TokenValues.NotEquals)
                return new NotEqualsNode();
            if (objeto.Value == TokenValues.Div)
                return new DivisionNode();
            if (objeto.Value == TokenValues.Sub)
                return new SubtractionNode();
            if (objeto.Value == TokenValues.Mul)
                return new MultiplicationNode();
            if (objeto.Value == TokenValues.Mod)
                return new ModulsNode();
            ListaErrores.Add(new CompilingError(new CodeLocation(), ErrorCode.Invalid, " La operacion no es posible "));
            return null;
        }
        public ExpressionNode LLenarArbol(Stack<ExpressionNode> salida)
        {
            if (SiEsBinaria(salida.Peek()))
            {
                BinaryNode a;
                a = (BinaryNode)salida.Pop();
                a.ParteDerecha = LLenarArbol(salida);
                a.ParteIsquierda = LLenarArbol(salida);
                return a;
            }
            if (SiEsUnitaria(salida.Peek()))
            {
                UnitaryNode a;
                a = (UnitaryNode)salida.Pop();
                a.ParteDerecha = LLenarArbol(salida);
                return a;
            }
            else
            {
                return salida.Pop();
            }
        }
        public bool SiEsBinaria(ExpressionNode a)
        {
             if (a.Types == ExpressionTypes.BinaryNode)
                return true;
            return false;
        }
        public bool SiEsUnitaria(ExpressionNode a)
        {
            if (a.Types == ExpressionTypes.UnitaryNode) return true;
            return false;
        }
        #endregion
        public int SaberLaPoisicionFinalINstruccion()//empieza contando el let
        {
            int cantLet = 0;
            int cantidadif = 0;
            int cantllaves = 0;
            for (int i = ContadorPrincipal; i < ArrayToken.Count; i++)
            {
                if (ArrayToken[i].Value == TokenValues.Let)
                    cantLet++;
                if (ArrayToken[i].Value == TokenValues.In)
                    cantLet--;
                if (ArrayToken[i].Value == TokenValues.If)
                    cantidadif++;
                if (ArrayToken[i].Value == TokenValues.Else)
                    cantidadif--;
                if (ArrayToken[i].Value == TokenValues.OpenCurlyBraces)
                    cantllaves++;
                if (ArrayToken[i].Value == TokenValues.OpenCurlyBraces)
                    cantllaves--;
                if (cantLet == 0 && cantidadif == 0 && cantllaves == 0 && ArrayToken[i].Value == TokenValues.StatementSeparator)
                    return i;

            }
            return -1;
        }
        public int AntesComaSoloParaLasFiguras()
        {
            int contadorparentesisabierto = 0;
            for (int recorrer = ContadorPrincipal; recorrer < ArrayToken.Count; recorrer++)
            {
                if (ArrayToken[recorrer].Value == TokenValues.OpenBracket)
                    contadorparentesisabierto++;
                if (ArrayToken[recorrer].Value == TokenValues.ClosedBracket)
                    contadorparentesisabierto--;
                if (contadorparentesisabierto == 1 && ArrayToken[recorrer].Value == TokenValues.ValueSeparator)
                    return recorrer;
            }
            throw new Exception();
        }//empieza en el parentesis y se acaba cuando la cantidad de par=1 y hay una coma
        public int FinalParentesis()
        {
            int contadorparentesis = 0;
            for (int recorrer = ContadorPrincipal; recorrer < ArrayToken.Count; recorrer++)
            {
                if (ArrayToken[recorrer].Value == TokenValues.OpenBracket)
                    contadorparentesis++;
                if (ArrayToken[recorrer].Value == TokenValues.ClosedBracket)
                    contadorparentesis--;
                if (contadorparentesis == -1 && ArrayToken[recorrer].Value == TokenValues.ClosedBracket)
                    return recorrer;
            }
            return -1;
        }//empieza en la coma y busca el parentesis cerrado que no tiene par
        public int EncontrarComaEnElArcoYFuncExpresion(int pos)
        {
            for (int recorrer = ContadorPrincipal; recorrer < pos; recorrer++)
            {
                if (ArrayToken[recorrer].Value == TokenValues.ValueSeparator)
                    return recorrer;
            }
            return -1;
        }//esto es solo para el arco para parsear los puntos del arco y parsear parm de funcion expresion
        public int TerminaLet()
        {
            int contadorlet = 0;
            for (int recorrer = ContadorPrincipal; recorrer < ArrayToken.Count; recorrer++)
            {
                if (ArrayToken[recorrer].Value == TokenValues.Let)
                    contadorlet++;
                if (ArrayToken[recorrer].Value == TokenValues.In)
                    contadorlet--;
                if (contadorlet == 0 && ArrayToken[recorrer].Value == TokenValues.In)
                    return recorrer;
            }
            throw new Exception();
        }//empieza en el let y termina cuando encuentra un int y el contador de let= 0
        public int TerminaIn()
        {
            int contadorlet = 0;
            for (int recorrer = ContadorPrincipal + 1; recorrer < ArrayToken.Count; recorrer++)
            {
                if (ArrayToken[recorrer].Value == TokenValues.Let)
                    contadorlet++;
                if (ArrayToken[recorrer].Value == TokenValues.In)
                    contadorlet--;
                if (contadorlet == 0 && ArrayToken[recorrer].Value == TokenValues.StatementSeparator)
                    return recorrer;
            }
            throw new Exception();
        }//empieza en el in y termina cuando encuentra un punto y coma y el contador de let es 0
        public int Terminaif()
        {
            int contadorthen = 0;
            for (int recorrer = ContadorPrincipal; recorrer < ArrayToken.Count; recorrer++)
            {
                if (ArrayToken[recorrer].Value == TokenValues.If)
                    contadorthen++;
                if (ArrayToken[recorrer].Value == TokenValues.Then)
                    contadorthen--;
                if (contadorthen == 0 && ArrayToken[recorrer].Value == TokenValues.Then)
                    return recorrer;
            }
            throw new Exception();
        }//empieza en el if y termina cuando encuentra un then y el contador de then esta en cero
        public int TerminaThen()
        {
            int contadorthen = 0;
            for (int recorrer = ContadorPrincipal; recorrer < ArrayToken.Count; recorrer++)
            {
                if (ArrayToken[recorrer].Value == TokenValues.Then)
                    contadorthen++;
                if (ArrayToken[recorrer].Value == TokenValues.Else)
                    contadorthen--;
                if (contadorthen == 0 && ArrayToken[recorrer].Value == TokenValues.Else)
                    return recorrer;
            }
            throw new Exception();
        }//empieza en el then y termina cuando encuentra un else y el contador de then =o
        public int TerminaElse()//empieza en el else y termina en el punto y coma si el contador de else =0
        {
            int cantLet = 0;
            int cantElse = 0;
            for (int i = ContadorPrincipal; i < ArrayToken.Count; i++)
            {
                if (ArrayToken[i].Value == TokenValues.Let)
                    cantLet++;
                if (ArrayToken[i].Value == TokenValues.In)
                    cantLet--;
                if (ArrayToken[i].Value == TokenValues.If)
                    cantElse++;
                if (ArrayToken[i].Value == TokenValues.Else)
                    cantElse--;
                else if (cantElse == 0 && cantLet == 0 && ArrayToken[i].Value == TokenValues.StatementSeparator)
                    return i;
            }
            throw new Exception("No encuentro el else ayudame");
        }
        public int Encontrarlacomaparasecuencia(int posicion)
        {
            int contadorparentesisabierto = 0;
            int cantLet = 0;
            int cantidadif = 0;
            for (int recorrer = ContadorPrincipal; recorrer < posicion; recorrer++)
            {
                if (ArrayToken[recorrer].Value == TokenValues.Let)
                    cantLet++;
                if (ArrayToken[recorrer].Value == TokenValues.In)
                    cantLet--;
                if (ArrayToken[recorrer].Value == TokenValues.If)
                    cantidadif++;
                if (ArrayToken[recorrer].Value == TokenValues.Else)
                    cantidadif--;
                if (ArrayToken[recorrer].Value == TokenValues.OpenBracket)
                    contadorparentesisabierto++;
                if (ArrayToken[recorrer].Value == TokenValues.ClosedBracket)
                    contadorparentesisabierto--;
                if (contadorparentesisabierto == 0 && ArrayToken[recorrer].Value == TokenValues.ValueSeparator && cantLet == 0 && cantidadif == 0  )
                    return recorrer;
            }
            return -1;
        }
        public int DondeEstaelIgual(int posicion)
        {
            for (int recorrer = ContadorPrincipal; recorrer < posicion; recorrer++)
            {
                if (ArrayToken[recorrer].Value == TokenValues.Assign)
                    return recorrer;
            }
            ListaErrores.Add(new CompilingError(new CodeLocation(), ErrorCode.Invalid, ""));
            return -1;
        }//para hacer el math
        public int TerminaLLave()
        {
            int cantidadllaves = 0;
            for (int recorrer = ContadorPrincipal; recorrer < ArrayToken.Count; recorrer++)
            {
                if (ArrayToken[recorrer].Value == TokenValues.ClosedCurlyBraces)
                    cantidadllaves--;
                if (cantidadllaves == -1 && ArrayToken[recorrer].Value == TokenValues.ClosedCurlyBraces)
                    return recorrer;
            }
            return -1;
        }//empieza antes de la llave y es para saber donde termnina la primera llave
        public int TerminalaLLavefinal()
        {
            int cantidadllaves = 0;
            for (int recorrer = ContadorPrincipal; recorrer < ArrayToken.Count; recorrer++)
            {
                if (ArrayToken[recorrer].Value == TokenValues.OpenCurlyBraces)
                    cantidadllaves++;
                if (ArrayToken[recorrer].Value == TokenValues.ClosedCurlyBraces)
                    cantidadllaves--;
                if (cantidadllaves == 0 && ArrayToken[recorrer].Value == TokenValues.ClosedCurlyBraces)
                    return recorrer;
            }
            throw new Exception();
        }//empieza en la coma y busca la llave final
        #endregion
    }     
}
