using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Compiling;
using GeoWallE;
using System.Collections;

namespace GeoWallE
{
    #region ClaseEInterfaces
    public  interface MetodosComunes
    {
        void Dibujar(Canvas canvas);
    }
    public interface objetos:MetodosComunes
    {
    }
    public class Figura : objetos, MetodosComunes
    {
        public virtual void Dibujar(Canvas canvas)
        { }
        
    }
    #endregion
    #region NumeroYMedidaYText
    public class Text:objetos
    {
        public string Nombre;
       public Text(string nombre)
        {
            Nombre = nombre;
        }

        public void Dibujar(Canvas canvas)
        {
            
        }
    }
    public class Numero : objetos
    {
        public Numero(double a)
        {
            valor = a;
        }
        public double valor { get; set; }

        public void Dibujar(Canvas canvas)
        {
            
        }
    }
    public class Medida : objetos
    {
        public double medida { get; private set; }
        public Punto Punto1;
        public Punto Punto2;
        public Medida()
        {
            Random Random = new Random();
            Stopwatch crono = new Stopwatch();
            crono.Start();
            while (crono.ElapsedMilliseconds < 25)
                medida = Random.Next(1, 200);
        }
        public Medida(Punto punto1, Punto punto2)
        {
            Punto1 = punto1;
            Punto2 = punto2;
            medida = (int)Punto1.DistanciaPuntos(Punto1, Punto2);
        }

        public void Dibujar(Canvas canvas)
        {
            
        }
    }
    #endregion
    #region FigurasLenguaje
    public class Undefined : objetos
    {
        public void Dibujar(Canvas canvas)
        {
            
        }
    }
    public class Segmento : Recta
    {
        public Segmento(string nombre):base(nombre)
        {
            Nombre = nombre;
            Punto1 = new Punto("");
            Punto2 = new Punto("");
        }
        public Segmento(Punto punto1, Punto punto2,string nombre) : base(punto1, punto2, nombre)
        {
            Nombre = nombre;
            Punto1 = punto1;
            Punto2 = punto2;
        }
        public override void Dibujar(Canvas canvas)
        {
            canvas.DrawSegment(new System.Drawing.Point((int)Punto1.CoordenadaX, (int)Punto1.CoordenadaY), new System.Drawing.Point((int)Punto2.CoordenadaX, (int)Punto2.CoordenadaY), Nombre);
        }
    }
    public class Rayo : Recta
    {
        public Rayo(string nombre) :base(nombre)//para cuando digan ray p1;
        {
            Nombre = nombre;
            Punto1 = new Punto("");
            Punto2 = new Punto("");
        }
        public Rayo(Punto punto1, Punto punto2,string nombre) : base(punto1, punto2, nombre)
        {
            Nombre = nombre;
            Punto1 = punto1;
            Punto2 = punto2;
        }
        public override void Dibujar(Canvas canvas)
        {
            canvas.DrawRay(new System.Drawing.Point((int)Punto1.CoordenadaX, (int)Punto1.CoordenadaY), new System.Drawing.Point((int)Punto2.CoordenadaX, (int)Punto2.CoordenadaY), Nombre);
        }
    }
    public class Recta : Figura
    {
        public Punto Punto1 { get; set; }
        public Punto Punto2 { get; set; }
        public string Nombre;
        public Recta(string nombre)
        {
            Nombre = nombre;
            Punto1 = new Punto("");
            Punto2 = new Punto("");
        }
        public Recta(Punto punto1, Punto punto2,string nombre)
        {
            Nombre = nombre;
            this.Punto1 = punto1;
            this.Punto2 = punto2;
        }
        public override void Dibujar(Canvas canvas)
        {
            canvas.DrawLine(new System.Drawing.Point((int)Punto1.CoordenadaX, (int)Punto1.CoordenadaY), new System.Drawing.Point((int)Punto2.CoordenadaX, (int)Punto2.CoordenadaY), Nombre);
        }
        public double PendienteDeLaRectaPorPuntos(Punto punto1, Punto punto2)//pendiente de la recta mediante dos puntos
        {
            double pendientedelarecta = (punto1.CoordenadaY - punto2.CoordenadaY) / (punto1.CoordenadaX - punto2.CoordenadaX);
            return pendientedelarecta;
        }
        public double[] EcuacionDeLaRecta(Punto punto1, Punto punto2)//Ecuacion de la forma y= mx +n
        {
            double pendienteDeLaRecta = PendienteDeLaRectaPorPuntos(punto1, punto2);
            double interceptoconejeY = punto2.CoordenadaY -( pendienteDeLaRecta * punto2.CoordenadaX);
            double[] ecuacionrecta = new double[4];
            ecuacionrecta[0] = 1;//corresponde al lugar de la y
            ecuacionrecta[1] = pendienteDeLaRecta; //corresponde con el valor de la pendiente de la recta m
            ecuacionrecta[2] = 1; // corresponde con la x
            ecuacionrecta[3] = interceptoconejeY; // corresponde con el intercepto con el eje y
            return ecuacionrecta;
        }

    }
    public class Punto : Figura
    {
        string Nombre;
        public double CoordenadaX { get; set; }
        public double CoordenadaY { get; set; }
        public Punto(double coordenadaX, double coordenadaY)
        {
            CoordenadaX = coordenadaX;
            CoordenadaY = coordenadaY;
        } //para cuando me escriben point pi(x,y);
        public Punto(string nombre)
        {
            Nombre = nombre;
            Stopwatch a = new Stopwatch();//esto es para que no me de dos puntos con el mismo valor
            a.Start();
            Random Aleatorio = new Random();
            while (a.ElapsedMilliseconds < 25) ;
            CoordenadaX = Aleatorio.Next(-1, 500);
            CoordenadaY = Aleatorio.Next(-1, 500);
        }//para cuando me escriben point p1;
        public double DistanciaPuntos(Punto Punto1, Punto Punto2)//distancia entre dos puntos
        {
            double distanciapuntos = 0;
            distanciapuntos = Math.Sqrt(Math.Pow(Punto1.CoordenadaX - Punto2.CoordenadaX, 2) + Math.Pow(Punto1.CoordenadaY - Punto2.CoordenadaY, 2));
            return distanciapuntos;

        }
        public Punto PuntoMedio(Punto punto1, Punto punto2)
        {
            double coordenadaXdelPuntomedio = (punto1.CoordenadaX + punto2.CoordenadaX) / 2;
            double coordenadaYdelPuntomedio = (punto1.CoordenadaY + punto2.CoordenadaY) / 2;
            Punto puntomedio = new Punto(coordenadaXdelPuntomedio, coordenadaYdelPuntomedio);
            return puntomedio;

        }
        public override void Dibujar(Canvas canvas)
        {
            canvas.DrawPoint(new System.Drawing.Point((int)CoordenadaX, (int)CoordenadaY), Nombre);
        }
    }
    public class Circunferencia : Figura
    {
        public string Nombre;
        public Punto Centro { get; private set; }
        public Medida MedidaRadio { get; private set; }
        public Circunferencia(Punto centro, Medida medida)
        {
            Centro = centro;
            MedidaRadio = medida;
        }
        public Circunferencia(string nombre)
        {
            Nombre = nombre;
            Centro = new Punto("");
            MedidaRadio = new Medida();
        }
        public override void Dibujar(Canvas canvas)
        {
            canvas.DrawCircle(new System.Drawing.Point((int)Centro.CoordenadaX, (int)Centro.CoordenadaY), (int)MedidaRadio.medida,Nombre);
        }
    }
    public class Arco : Circunferencia
    {
        public Punto Punto1 { get; private set; }
        public Punto Punto2 { get; private set; }
        public Medida MedidaRa { get; private set; }
        public Arco(Punto punto1, Punto punto2, Punto centro, Medida radio) : base(centro, radio)
        {
            Punto1 = punto1;
            Punto2 = punto2;
            MedidaRa = radio;
        }
        public Arco (string nombre):base(nombre)
        {
            Nombre = nombre;
            Punto1 = new Punto("");
            Punto2 = new Punto("");

        }
        public override void Dibujar(Canvas canvas)
        {
            canvas.DrawArc(new System.Drawing.Point((int)Centro.CoordenadaX, (int)Centro.CoordenadaY), new System.Drawing.Point((int)Punto1.CoordenadaX, (int)Punto1.CoordenadaY), new System.Drawing.Point((int)Punto2.CoordenadaX, (int)Punto2.CoordenadaY) , (int)MedidaRadio.medida,Nombre);
        }
    }
    #endregion
    #region Secuencias
    public class Secuencia : objetos, MetodosComunes
    {
       
        public virtual void Dibujar(Canvas canvas)
        {
            
        }
    }
    #region SecuenciaInfinita
    public class SecuenciaInfinita : Secuencia
    {
        public MiIEnumerable Secue { get; set; }
    }
    public class SecuenciaInfinitaNumeros:SecuenciaInfinita
    {
        public MiIEnumerable SecuenciaNumeros;
        public SecuenciaInfinitaNumeros(int inicio)
        {
            SecuenciaNumeros = new MiIEnumerable(inicio);
            Secue = SecuenciaNumeros;
        }
    }
    public class SecuenciaInfinitaPuntos
    {
    }
   
    #endregion
    #region SecuenciaFinita
    public class SecuenciaFinita :Secuencia
    {
        public List<objetos> Secuencia { get;  set; }
        public int Empieza;
        public int Termina;
        public SecuenciaFinita(List<objetos> secuencia)
        {
            Secuencia = secuencia;
        }
        public SecuenciaFinita(string dondeempieza, string dondetermina)
        {
            List<objetos> secue = new List<objetos>();
            Empieza = int.Parse(dondeempieza);
            Termina = int.Parse(dondetermina);
            for (int recorrer = Empieza; recorrer <= Termina; recorrer++)
            {
                secue.Add(new Numero(recorrer));
            }
            Secuencia = secue;
        }
    public override void Dibujar(Canvas canvas)
        {
            foreach(var elementos in Secuencia)
            {
                elementos.Dibujar(canvas);
            }
        }
    }
   
  
    public class SecuenciaUndefine 
    {
    }
    #endregion
    #region MetodosDeSecuencias

    public class MiIEnumerable : IEnumerable<Numero>
    {
        public double Inicio;
        public MiIEnumerable(int inicio)
        {
            Inicio = inicio;
        }
        public IEnumerator<Numero> GetEnumerator()
        {
            while (true)
            {
                yield return new Numero(Inicio);
                Inicio++;
            } 
          
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
             return GetEnumerator();
        }
    }
    
    #endregion
    #endregion
    #region Intersecciones
    public static class  Intersecciones
    {
       
        #region  MetodosAuxiliares
        public static bool PerteneceAlaFigura( Punto punto,double XMayor, double XMenor )
        {
            if (punto.CoordenadaX >= XMenor && punto.CoordenadaX <= XMayor) 
                return true;
            return false;
        }
        public static double QuienTieneLaMayorX(Punto punto1, Punto punto2) {
            if (punto1.CoordenadaX < punto2.CoordenadaX) return punto2.CoordenadaX;
            return punto1.CoordenadaX;
        }
        public static double QuienTieneLaMenorX(Punto punto1, Punto punto2)
        {
            if (punto1.CoordenadaX > punto2.CoordenadaX) return punto2.CoordenadaX;
            return punto1.CoordenadaX;
        }
        public static bool SoloParaelrayo(Punto punto1, Punto punto2)
        {
            if (punto1.CoordenadaX == QuienTieneLaMayorX(punto1, punto2)) 
            return true;
            return false;
        }
        public static bool PertenecealRayo(Punto puntoaver,Punto Punto1, Punto Punto2 )
        {
            if(SoloParaelrayo(Punto1,Punto2))
            {
                if (puntoaver.CoordenadaX < Punto1.CoordenadaX) return true;
                return false;
            }
            else
            {
                if (puntoaver.CoordenadaX > Punto1.CoordenadaX) return true;
                return false;
            }
           
            
        }
        public static bool PuntoPerteneceaRecta(double [] recta, Punto punto)
        {
            double y = recta[1] * punto.CoordenadaX + recta[3];
            if (y == punto.CoordenadaY) return true;
            return false;
        }
        public static bool SabersiPertenecealRayo(Rayo rayo, Punto punto)
        {
            double pendiente = -(1 / rayo.PendienteDeLaRectaPorPuntos(rayo.Punto1, rayo.Punto2));
            double interceptoconejeY = rayo.Punto2.CoordenadaY - (pendiente * rayo.Punto2.CoordenadaX);
            double[] ecuacionrecta = new double[4];
            ecuacionrecta[0] = 1;//corresponde al lugar de la y
            ecuacionrecta[1] = pendiente; //corresponde con el valor de la pendiente de la recta m
            ecuacionrecta[2] = 1; // corresponde con la x
            ecuacionrecta[3] = interceptoconejeY; // corresponde con el intercepto con el eje y
            if (PuntoPerteneceaRecta(ecuacionrecta, punto)) return true;
            return false;
          
        }
        #endregion
        #region Intersecciones
        public static double BuscarAngulo(Punto puntoinicialvecto1, Punto punto2vector1, Punto puntoinicialVector2, Punto punto2Vector2, double Norma1, double Norma2)
        {
            double Xvector1 = punto2vector1.CoordenadaX - puntoinicialvecto1.CoordenadaX;
            double Yvector1 = punto2vector1.CoordenadaY - puntoinicialvecto1.CoordenadaY;
            double Xvector2 = punto2Vector2.CoordenadaX - puntoinicialVector2.CoordenadaX;
            double Yvector2 = punto2Vector2.CoordenadaY - puntoinicialVector2.CoordenadaY;
            double productoescalar = ((Xvector1 * Xvector2) + (Yvector1 * Yvector2));
            double productonorma = Norma1 * Norma2;
            double angulo= ( (Math.Acos(productoescalar / productonorma) * 180)/Math.PI);
            double ejez = Xvector1 * Yvector2 - Xvector2 * Yvector1;
            if (ejez >= 0) angulo = 360 - angulo;
            return angulo;
        }
        public static SecuenciaFinita interceptarrectas(Recta recta1, Recta recta2)
        {
          
        double[] EcuacionRecta1= recta1.EcuacionDeLaRecta(recta1.Punto1, recta1.Punto2); // de la forma y=mx+n; la y y la x en el array tienen valor 1
        double[] EcuacionRecta2 = recta2.EcuacionDeLaRecta(recta2.Punto1, recta2.Punto2);
        double PendienteRecta1=  recta1.PendienteDeLaRectaPorPuntos(recta1.Punto1, recta1.Punto2);
        double PendienteRecta2= recta2.PendienteDeLaRectaPorPuntos(recta2.Punto1, recta2.Punto2);
        List<objetos> puntos = new List<objetos>();
            if (PendienteRecta1 == PendienteRecta2)
            {
                return new SecuenciaFinita(puntos);
            }
            else if (EcuacionRecta1[3] == EcuacionRecta2[3])
            {
                puntos.Add(new Punto(0, EcuacionRecta1[3]));
                return new SecuenciaFinita(puntos);
            }
            else
            {
                double Coordenadax = (-(EcuacionRecta1[3] - EcuacionRecta2[3])) / (PendienteRecta1 - PendienteRecta2);
                double Coordenaday = PendienteRecta1 * Coordenadax + EcuacionRecta1[3];
                puntos.Add(new Punto(Coordenadax, Coordenaday));
                return new SecuenciaFinita(puntos);
            }
           
        }
        public static SecuenciaFinita interceptarsegmentos(Segmento segmento1, Segmento segmento2)
        {
            double[] EcuacionSegmento1 = segmento1.EcuacionDeLaRecta(segmento1.Punto1, segmento1.Punto2);
            double[] EcuacionSegmento2 = segmento2.EcuacionDeLaRecta(segmento2.Punto1, segmento2.Punto2);
            double Pendientesegmento1 = segmento1.PendienteDeLaRectaPorPuntos(segmento1.Punto1, segmento1.Punto2);
            double Pendientesegmento2 = segmento2.PendienteDeLaRectaPorPuntos(segmento2.Punto1, segmento2.Punto2);
            List<objetos> puntos = new List<objetos>();
            if (Pendientesegmento1 == Pendientesegmento2)
            {
                return new SecuenciaFinita(puntos);
            }
            else if (EcuacionSegmento1[3] == EcuacionSegmento2[3])
            {
                double mayorXsegmento1 = QuienTieneLaMayorX(segmento1.Punto1, segmento1.Punto2);
                double MayorXsegmento2 = QuienTieneLaMayorX(segmento2.Punto1, segmento2.Punto2);
                double MenorXsegmento1 = QuienTieneLaMenorX(segmento1.Punto1, segmento1.Punto2);
                double MenorXsegmento2 = QuienTieneLaMenorX(segmento2.Punto1, segmento2.Punto2);
                Punto punto = new Punto(0, EcuacionSegmento1[3]);
                if (PerteneceAlaFigura(punto, mayorXsegmento1, MenorXsegmento1) && PerteneceAlaFigura(punto, MayorXsegmento2, MenorXsegmento2))
                {
                    puntos.Add(punto);
                    return new SecuenciaFinita(puntos);
                }
                return new SecuenciaFinita(puntos);

            }
            else
            {
                double Coordenadax = (-1 * (EcuacionSegmento1[3] - EcuacionSegmento2[3])) / (Pendientesegmento1 - Pendientesegmento2);
                double Coordenaday = Pendientesegmento1 * Coordenadax + EcuacionSegmento1[3];
                double MayorXsegmento1 = QuienTieneLaMayorX(segmento1.Punto1, segmento1.Punto2);
                double MayorxSegmento2 = QuienTieneLaMayorX(segmento2.Punto1, segmento2.Punto2);
                double MenorXsegmento1 = QuienTieneLaMenorX(segmento1.Punto1, segmento1.Punto2);
                double MayorXsegmento2 = QuienTieneLaMenorX(segmento2.Punto1, segmento2.Punto2);
                Punto punto = new Punto(Coordenadax, Coordenaday);
                if (PerteneceAlaFigura(punto, MayorXsegmento1, MenorXsegmento1) && PerteneceAlaFigura(punto, MayorxSegmento2, MayorXsegmento2))
                {
                    puntos.Add(punto);
                    return new SecuenciaFinita(puntos);
                }
                else
                {
                    return new SecuenciaFinita(puntos);
                }

            }

           
        }
        public static SecuenciaFinita interceptarRayos(Rayo rayo1, Rayo rayo2)
        {
            double[] EcuacionRayo1 = rayo1.EcuacionDeLaRecta(rayo1.Punto1, rayo1.Punto2);
            double[] EcuacionRayo2 = rayo2.EcuacionDeLaRecta(rayo2.Punto1, rayo2.Punto2);
            double PendienteRayo1 = rayo1.PendienteDeLaRectaPorPuntos(rayo1.Punto1, rayo1.Punto2);
            double PendienteRayo2 = rayo2.PendienteDeLaRectaPorPuntos(rayo2.Punto1, rayo2.Punto2);
            List<objetos> puntos = new List<objetos>();
            if (PendienteRayo1 == PendienteRayo2)
            {
                return new SecuenciaFinita(puntos);
            }
            else if (EcuacionRayo1[3] == EcuacionRayo2[3])
            {

                double menorXrayo1 = QuienTieneLaMenorX(rayo1.Punto1, rayo1.Punto2);
                double menorXrayo2 = QuienTieneLaMenorX(rayo2.Punto1, rayo2.Punto2);
                Punto punto = new Punto(0, EcuacionRayo1[3]);
                if (PertenecealRayo(punto,rayo1.Punto1, rayo1.Punto2) && PertenecealRayo(punto,rayo2.Punto1, rayo2.Punto2))
                {
                    puntos.Add(punto);
                    return new SecuenciaFinita(puntos);
                }
                return new SecuenciaFinita(puntos);

            }
            else
            {
                double Coordenadax = (-(EcuacionRayo1[3] - EcuacionRayo2[3])) / (PendienteRayo1 - PendienteRayo2);
                double Coordenaday = PendienteRayo1 * Coordenadax + EcuacionRayo1[3];
                Punto punto = new Punto(Coordenadax, Coordenaday);
                if (PertenecealRayo(punto,rayo1.Punto1, rayo1.Punto2) && PertenecealRayo(punto,rayo2.Punto1,rayo2.Punto2))
                {
                    puntos.Add(punto);
                    return new SecuenciaFinita(puntos);
                }
                else
                {
                    return new SecuenciaFinita(puntos);
                }

            }

          
        }
        public static SecuenciaFinita interceptarRayoYRecta(Rayo rayo, Recta recta)
        {
            double[] EcuacionRecta = recta.EcuacionDeLaRecta(recta.Punto1, recta.Punto2);
            double[] EcuacionRayo = rayo.EcuacionDeLaRecta(rayo.Punto1, rayo.Punto2);
            double pendienterecta = recta.PendienteDeLaRectaPorPuntos(recta.Punto1, recta.Punto2);
            double pendienterayo = rayo.PendienteDeLaRectaPorPuntos(rayo.Punto1, rayo.Punto2);
            List<objetos> puntos = new List<objetos>();
            if (pendienterecta == pendienterayo)
            {
                return new SecuenciaFinita(puntos);
            }
            else if (EcuacionRecta[3] == EcuacionRayo[3])
            {
                Punto punto = new Punto(0, EcuacionRecta[3]);
                if (PertenecealRayo(punto, rayo.Punto1,rayo.Punto2))
                {
                    puntos.Add(punto);
                    return new SecuenciaFinita(puntos);
                }
                return new SecuenciaFinita(puntos);

            }
            else
            {
                double Coordenadax = (-1 * (EcuacionRecta[3] - EcuacionRayo[3])) / (pendienterecta - pendienterayo);
                double Coordenaday = pendienterecta * Coordenadax + EcuacionRecta[3];
                Punto punto = new Punto(Coordenadax, Coordenaday);
                if (PertenecealRayo(punto, rayo.Punto1, rayo.Punto2))
                {
                    puntos.Add(punto);
                    return new SecuenciaFinita(puntos);
                }
                else
                {
                    return new SecuenciaFinita(puntos);
                }
            }
        }
        public static SecuenciaFinita interceptarRectaYrayo(Recta recta, Rayo rayo)
        {
            return interceptarRayoYRecta(rayo, recta);
        }
        public static SecuenciaFinita interceptarRectaYSegmento(Recta recta, Segmento segmento)
        {
            double[] EcuacionSegmento = recta.EcuacionDeLaRecta(recta.Punto1, recta.Punto2);
            double[] EcuacionSegmento2 = segmento.EcuacionDeLaRecta(segmento.Punto1, segmento.Punto2);
            double pendienterecta = recta.PendienteDeLaRectaPorPuntos(recta.Punto1, recta.Punto2);
            double pendientesegmento = segmento.PendienteDeLaRectaPorPuntos(segmento.Punto1, segmento.Punto2);
            List<objetos> puntos = new List<objetos>();
            if (pendienterecta == pendientesegmento)
            {
                return new SecuenciaFinita(puntos);
            }
            else if (EcuacionSegmento[3] == EcuacionSegmento2[3])
            {

                double MayorXsegmento = QuienTieneLaMayorX(segmento.Punto1, segmento.Punto2);
                double MenorXsegmento = QuienTieneLaMenorX(segmento.Punto1, segmento.Punto2);
                Punto punto = new Punto(0, EcuacionSegmento[3]);
                if (PerteneceAlaFigura(punto, MayorXsegmento, MenorXsegmento))
                {
                    puntos.Add(punto);
                    return new SecuenciaFinita(puntos);
                }
                return new SecuenciaFinita(puntos);

            }
            else
            {
                double Coordenadax = (-1 * (EcuacionSegmento[3] - EcuacionSegmento2[3])) / (pendienterecta - pendientesegmento);
                double Coordenaday = pendienterecta * Coordenadax + EcuacionSegmento[3];
                double mayorXsegmento = QuienTieneLaMayorX(segmento.Punto1, segmento.Punto2);
                double menorXsegmento = QuienTieneLaMenorX(segmento.Punto1, segmento.Punto2);
                Punto punto = new Punto(Coordenadax, Coordenaday);
                if (PerteneceAlaFigura(punto, mayorXsegmento, menorXsegmento))
                {
                    puntos.Add(punto);
                    return new SecuenciaFinita(puntos);
                }
                else
                {
                    return new SecuenciaFinita(puntos);
                }
            }
        }
        public static SecuenciaFinita interceptarSegmentoYRecta(Segmento segmento,Recta recta)
        {
            return interceptarRectaYSegmento(recta, segmento);
        }
        public static SecuenciaFinita interceptarRayoYSegmento(Rayo rayo, Segmento segmento)
        {
            double[] EcuacionRayo1 = rayo.EcuacionDeLaRecta(rayo.Punto1, rayo.Punto2);
            double[] EcuacionRayo2 = segmento.EcuacionDeLaRecta(segmento.Punto1, segmento.Punto2);
            double PendienteRayo1 = rayo.PendienteDeLaRectaPorPuntos(rayo.Punto1, rayo.Punto2);
            double PendienteRayo2 = segmento.PendienteDeLaRectaPorPuntos(segmento.Punto1, segmento.Punto2);
            List<objetos> puntos = new List<objetos>();
            if (PendienteRayo1 == PendienteRayo2)
            {
                return new SecuenciaFinita(puntos);
            }
            else if (EcuacionRayo1[3] == EcuacionRayo2[3])
            {

                double mayorXsegmento = QuienTieneLaMayorX(segmento.Punto1, segmento.Punto2);
                double menorXsegmento = QuienTieneLaMenorX(segmento.Punto1, segmento.Punto2);
                Punto punto = new Punto(0, EcuacionRayo1[3]);
                if (PertenecealRayo(punto, rayo.Punto1, rayo.Punto2) && PerteneceAlaFigura(punto,mayorXsegmento,menorXsegmento))
                    {
                    puntos.Add(punto);
                    return new SecuenciaFinita(puntos);
                }
                return new SecuenciaFinita(puntos);

            }
            else
            {
                double Coordenadax = (-(EcuacionRayo1[3] - EcuacionRayo2[3])) / (PendienteRayo1 - PendienteRayo2);
                double Coordenaday = PendienteRayo1 * Coordenadax + EcuacionRayo1[3];
                Punto punto = new Punto(Coordenadax, Coordenaday);
                double mayorXsegmento = QuienTieneLaMayorX(segmento.Punto1, segmento.Punto2);
                double menorXsegmento = QuienTieneLaMenorX(segmento.Punto1, segmento.Punto2);
                if (PertenecealRayo(punto, rayo.Punto1, rayo.Punto2) && PerteneceAlaFigura(punto, mayorXsegmento, menorXsegmento))
                {
                    puntos.Add(punto);
                    return new SecuenciaFinita(puntos);
                }
                else
                {
                    return new SecuenciaFinita(puntos);
                }

            }
        }
        public static SecuenciaFinita interceptarSegmentoYRayo(Segmento segmento, Rayo rayo)
        {
            return interceptarRayoYSegmento(rayo, segmento);
        }
        public static SecuenciaFinita interceptarCircunferenciaYRecta(Circunferencia circunferencia, Recta recta)
        {
            double radio = circunferencia.MedidaRadio.medida;
            Punto centro = circunferencia.Centro;
            double[] ecuaciondelarecta = recta.EcuacionDeLaRecta(recta.Punto1, recta.Punto2);
            double interceptoejey = ecuaciondelarecta[3];
            double pendientedelarecta = recta.PendienteDeLaRectaPorPuntos(recta.Punto1, recta.Punto2);
            double A = 1 + Math.Pow(pendientedelarecta, 2);
            double B = -2 * centro.CoordenadaX + 2 * pendientedelarecta * interceptoejey - 2 * pendientedelarecta * centro.CoordenadaY;
            double C = Math.Pow(centro.CoordenadaX, 2) + Math.Pow(centro.CoordenadaY, 2) - Math.Pow(radio, 2) + Math.Pow(interceptoejey, 2)
                - 2 * interceptoejey * centro.CoordenadaY;
            double discriminante = Math.Pow(B, 2) - 4 * A * C;
            List<objetos> secuencia = new List<objetos>();
            if (discriminante == 0)
            {
                double X = -B / (2 * A);
                double Y = pendientedelarecta * X + interceptoejey;
                Punto intercepto = new Punto(X, Y);
                secuencia.Add(intercepto);
                return new SecuenciaFinita(secuencia);
            }
            else if(discriminante < 0)
            {
                return new SecuenciaFinita(secuencia);
            }
            else
            {
                double X1 = (-B + Math.Sqrt(discriminante)) /( 2 * A);
                double X2= (-B - Math.Sqrt(discriminante)) / (2 * A);
                double Y1= pendientedelarecta * X1 + interceptoejey;
                double Y2 = pendientedelarecta * X2 + interceptoejey;
                Punto punto1 = new Punto(X1, Y1);
                Punto punto2 = new Punto(X2, Y2);
                    secuencia.Add(punto1);
                    secuencia.Add(punto2);
                return new SecuenciaFinita(secuencia);
            }
        }
        public static SecuenciaFinita interceptarRectaYCircunferencia(Recta recta, Circunferencia circunferencia)
        {
            return interceptarCircunferenciaYRecta(circunferencia, recta);
        }
        public static SecuenciaFinita interceptarRayoYcircunferencia(Rayo rayo, Circunferencia circunferencia)
        {
           double radio = circunferencia.MedidaRadio.medida;
            Punto centro = circunferencia.Centro;
            double[] ecuaciondelarecta = rayo.EcuacionDeLaRecta(rayo.Punto1, rayo.Punto2);
            double interceptoejey = ecuaciondelarecta[3];
            double pendientedelarecta = rayo.PendienteDeLaRectaPorPuntos(rayo.Punto1, rayo.Punto2);
            double A = 1 + Math.Pow(pendientedelarecta, 2);
            double B = -2 * centro.CoordenadaX + 2 * pendientedelarecta * interceptoejey - 2 * pendientedelarecta * centro.CoordenadaY;
            double C = Math.Pow(centro.CoordenadaX, 2) + Math.Pow(centro.CoordenadaY, 2) - Math.Pow(radio, 2) + Math.Pow(interceptoejey, 2)
                - 2 * interceptoejey * centro.CoordenadaY;
            double discriminante = Math.Pow(B, 2) - 4 * A * C;
            List<objetos> secuencia = new List<objetos>();
            if (discriminante ==0)
            {
                double X = -B / (2 * A);
                double Y = pendientedelarecta * X + interceptoejey;
                Punto intercepto = new Punto(X, Y);
               
                if (PertenecealRayo(intercepto, rayo.Punto1, rayo.Punto2))
                {
                    secuencia.Add(intercepto);
                    return new SecuenciaFinita(secuencia);
                }
                else return new SecuenciaFinita(secuencia);
            }
            else if (discriminante < 0)
            {
                return new SecuenciaFinita(secuencia);//secuencia undifine
            }
            else
            {
                double X1 = (-B + Math.Sqrt(discriminante)) / (2 * A);
                double X2 = (-B - Math.Sqrt(discriminante)) / (2 * A);
                double Y1 = pendientedelarecta * X1 + interceptoejey;
                double Y2 = pendientedelarecta * X2 + interceptoejey;
                Punto punto1 = new Punto(X1, Y1);
                Punto punto2 = new Punto(X2, Y2);
                if(PertenecealRayo(punto1,rayo.Punto1,rayo.Punto2))
                {
                    secuencia.Add(punto1);
                }
                if(PertenecealRayo(punto2,rayo.Punto1,rayo.Punto2))
                {
                    secuencia.Add(punto2);
                }
                if (secuencia.Count != 0)
                    return new SecuenciaFinita(secuencia);
                else return new SecuenciaFinita(secuencia);//secuencia undifine
            }
        }
        public static SecuenciaFinita interceptarCircunferenciaYRayo(Circunferencia circunferencia, Rayo rayo)
        {
            return interceptarRayoYcircunferencia(rayo, circunferencia);
        }
        public static SecuenciaFinita interceptarSegmentoYCircunferencia(Segmento segmento, Circunferencia circunferencia)
        {
            double radio = circunferencia.MedidaRadio.medida;
            Punto centro = circunferencia.Centro;
            double[] ecuaciondelarecta = segmento.EcuacionDeLaRecta(segmento.Punto1, segmento.Punto2);
            double interceptoejey = ecuaciondelarecta[3];
            double pendientedelarecta = segmento.PendienteDeLaRectaPorPuntos(segmento.Punto1, segmento.Punto2);
            double A = 1 + Math.Pow(pendientedelarecta, 2);
            double B = -2 * centro.CoordenadaX + 2 * pendientedelarecta * interceptoejey - 2 * pendientedelarecta * centro.CoordenadaY;
            double C = Math.Pow(centro.CoordenadaX, 2) + Math.Pow(centro.CoordenadaY, 2) - Math.Pow(radio, 2) + Math.Pow(interceptoejey, 2)
                - 2 * interceptoejey * centro.CoordenadaY;
            double discriminante = Math.Pow(B, 2) - 4 * A * C;
            List<objetos> secuencia = new List<objetos>();
            if (discriminante == 0)
            {
                double X = -B / (2 * A);
                double Y = pendientedelarecta * X + interceptoejey;
                Punto intercepto = new Punto(X, Y);
              
                double MayorXsegmento = QuienTieneLaMayorX(segmento.Punto1, segmento.Punto2);
                double MenorXsegmento = QuienTieneLaMenorX(segmento.Punto1, segmento.Punto2);
                if (PerteneceAlaFigura(intercepto, MayorXsegmento, MenorXsegmento))
                {
                    secuencia.Add(intercepto);
                    return new SecuenciaFinita(secuencia);
                }
                return new SecuenciaFinita(secuencia); //secuencia undifined
            }

            else if (discriminante < 0)
            {
                return new SecuenciaFinita(secuencia);//secuencia undifined
            }
            else
            {
                double X1 = (-B + Math.Sqrt(discriminante)) / (2 * A);
                double X2 = (-B - Math.Sqrt(discriminante)) / (2 * A);
                double Y1 = pendientedelarecta * X1 + interceptoejey;
                double Y2 = pendientedelarecta * X2 + interceptoejey;
                Punto punto1 = new Punto(X1, Y1);
                Punto punto2 = new Punto(X2, Y2);
                double MayorXsegmento = QuienTieneLaMayorX(segmento.Punto1, segmento.Punto2);
                double MenorXsegmento = QuienTieneLaMenorX(segmento.Punto1, segmento.Punto2);
                if (PerteneceAlaFigura(punto1, MayorXsegmento, MenorXsegmento))
                {
                    secuencia.Add(punto1);
                }
                if (PerteneceAlaFigura(punto2, MayorXsegmento, MenorXsegmento))
                    secuencia.Add(punto2);
                if (secuencia.Count != 0)
                    return new SecuenciaFinita(secuencia);
                else return new SecuenciaFinita(secuencia);//secuencia undifined
            }
            }
        public static SecuenciaFinita interceptarCircunferenciaYSegmento(Circunferencia circunferencia, Segmento segmento)
        {
            return interceptarSegmentoYCircunferencia(segmento, circunferencia);
        }
        public static SecuenciaFinita interceptarCircunferenciaYCircunferencia(Circunferencia circunferencia1, Circunferencia circunferencia2)
        {
            //ecuacion de la forma Ax +By + C
            double A = -2 * circunferencia1.Centro.CoordenadaX + 2 * circunferencia2.Centro.CoordenadaX;
            double B = -2 * circunferencia1.Centro.CoordenadaY + 2 * circunferencia2.Centro.CoordenadaY;
            double C = Math.Pow(circunferencia1.Centro.CoordenadaX, 2) + Math.Pow(circunferencia1.Centro.CoordenadaY, 2)
                - Math.Pow(circunferencia1.MedidaRadio.medida, 2) - Math.Pow(circunferencia2.Centro.CoordenadaX, 2)
                - Math.Pow(circunferencia2.Centro.CoordenadaY, 2) + Math.Pow(circunferencia2.MedidaRadio.medida, 2);
            double pendiente;
            List<objetos> secuencia = new List<objetos>();
            if (circunferencia1.Centro == circunferencia2.Centro && circunferencia1.MedidaRadio == circunferencia1.MedidaRadio) return new SecuenciaFinita(secuencia);//secuencia undifined
            if (circunferencia1.Centro == circunferencia2.Centro) return new SecuenciaFinita(secuencia);//secuencia undifined;
            if (B== (double)0)
            {
                pendiente = 0;
                double a = 1;
                double b = -2 * circunferencia1.Centro.CoordenadaY;
                double c = Math.Pow(circunferencia1.Centro.CoordenadaY, 2) + (Math.Pow(C, 2)) / (Math.Pow(A, 2)) + ( 2*C * circunferencia1.Centro.CoordenadaX) / A
                    + Math.Pow(circunferencia1.Centro.CoordenadaX, 2) - Math.Pow(circunferencia1.MedidaRadio.medida, 2);
                double discriminante = Math.Pow(b, 2) - 4 * a * c;
                if (discriminante ==0)
                {
                    double Y = -b / (2 * a);
                    double X = -C / A;
                    Punto intercepto = new Punto(X, Y);
                    secuencia.Add(intercepto);
                    return new SecuenciaFinita(secuencia);
                }
                else if (discriminante > (double)0)
                {
                    double Y1 = (-b + Math.Sqrt(discriminante)) / (2 * a);
                    double Y2 = (-b - Math.Sqrt(discriminante)) / (2 * a);
                    double X1 = -C / A;
                    double X2 = -C / A;
                    Punto punto1 = new Punto(X1, Y1);
                    Punto punto2 = new Punto(X2, Y2);
                    secuencia.Add(punto1);
                    secuencia.Add(punto2);
                    return new SecuenciaFinita(secuencia);
                }
                else if (discriminante < 0)
                    return new SecuenciaFinita(secuencia);//secuencia undifined
            }
            else
            {
                if(A==(double)0)
                {
                    pendiente = 0;
                    double a = 1;
                    double b = -2 * circunferencia1.Centro.CoordenadaX;
                    double c = Math.Pow(circunferencia1.Centro.CoordenadaX, 2) + (Math.Pow(C, 2)) / (Math.Pow(B, 2)) + ( 2*C * circunferencia1.Centro.CoordenadaY) / B
                        + Math.Pow(circunferencia1.Centro.CoordenadaY, 2) - Math.Pow(circunferencia1.MedidaRadio.medida, 2);
                    double discriminante = Math.Pow(b, 2) - 4 * a * c;

                    if (discriminante ==(double) 0)
                    {
                        double X = -b / (2 * a);
                        double Y = -C / B;
                        Punto intercepto = new Punto(X, Y);
                        secuencia.Add(intercepto);
                        return new SecuenciaFinita(secuencia);
                    }
                    else if (discriminante > 0)
                    {
                        double X1 = (-b + Math.Sqrt(discriminante)) / (2 * a);
                        double X2 = (-b - Math.Sqrt(discriminante)) / (2 * a);
                        double Y1 = -C / B;
                        double Y2 = -C / B;
                        Punto punto1 = new Punto(X1, Y1);
                        Punto punto2 = new Punto(X2, Y2);
                        secuencia.Add(punto1);
                        secuencia.Add(punto2);
                        return new SecuenciaFinita(secuencia);
                    }
                    else if (discriminante < 0) return new SecuenciaFinita(secuencia);
                }
                else
                {
                    double a = 1 + Math.Pow(A, 2) / Math.Pow(B, 2);
                    double b = -2 * circunferencia1.Centro.CoordenadaX + (2 * A * C) / Math.Pow(B, 2) + (2 * A * circunferencia1.Centro.CoordenadaY) / B;
                    double c = Math.Pow(circunferencia1.Centro.CoordenadaX, 2) + Math.Pow(circunferencia1.Centro.CoordenadaY, 2) - Math.Pow(circunferencia1.MedidaRadio.medida, 2)
                   + 2 * C * circunferencia1.Centro.CoordenadaY / B + Math.Pow(C, 2) / Math.Pow(B, 2);
                   double discriminante= Math.Pow(b, 2) - 4 * a * c;
                    pendiente = (-A) / B;
                    double interceptoejeY = -C / B;

                    if (discriminante > 0)
                    {
                        double X1 = (-b + Math.Sqrt(discriminante)) / (2 * a);
                        double X2 = (-b - Math.Sqrt(discriminante)) / (2 * a);
                        double Y1 = pendiente * X1 + interceptoejeY;
                        double Y2 = pendiente * X2 + interceptoejeY;
                        Punto punto1 = new Punto(X1, Y1);
                        Punto punto2 = new Punto(X2, Y2);
                        secuencia.Add(punto1);
                        secuencia.Add(punto2);
                        return new SecuenciaFinita(secuencia);
                    }
                    else if (discriminante == 0)
                    {
                        double X = -b / (2 * a);
                        double Y = pendiente + X + interceptoejeY;
                        Punto punto = new Punto(X, Y);
                        secuencia.Add(punto);
                        return new SecuenciaFinita(secuencia);
                    }
                    else return new SecuenciaFinita(secuencia);//secuencia undifined
                }
            }
            throw new Exception();
        }
        public static SecuenciaFinita interceptarArcoYSegmento(Arco arco, Segmento segmento) {
            
            Circunferencia circunferencia = new Circunferencia(arco.Centro, arco.MedidaRadio);
            SecuenciaFinita secuencia = interceptarSegmentoYCircunferencia(segmento, circunferencia);
            double norma1 = arco.Centro.DistanciaPuntos(arco.Centro, arco.Punto1);
            double norma2 = arco.Centro.DistanciaPuntos(arco.Centro, arco.Punto2);
            double angulo = BuscarAngulo(arco.Centro, arco.Punto1, arco.Centro, arco.Punto2, norma1, norma2);
            List<objetos> secue = new List<objetos>();
            if (secuencia.Secuencia.Count != 0)
            {
                foreach (Punto punto in secuencia.Secuencia)
                {
                    double norma = punto.DistanciaPuntos(arco.Centro, punto);
                    double angulo2 = BuscarAngulo(arco.Centro, arco.Punto1, arco.Centro, punto, norma1, norma);
                    if (angulo2 <= (double)angulo)
                        secue.Add(punto);
                }
            }
                return new SecuenciaFinita(secue);
        }
        public static SecuenciaFinita interceptarSegmentoYarco(Segmento segmento, Arco arco) { return interceptarArcoYSegmento(arco, segmento); }
        public static SecuenciaFinita interceptarArcoYCircunferencia(Arco arco, Circunferencia circunferencia)
        {
            Circunferencia  circunferencia2 = new Circunferencia(arco.Centro, arco.MedidaRadio);
            SecuenciaFinita secuencia = interceptarCircunferenciaYCircunferencia(circunferencia, circunferencia2);
            double norma1 = arco.Centro.DistanciaPuntos(arco.Centro, arco.Punto1);
            double norma2 = arco.Centro.DistanciaPuntos(arco.Centro, arco.Punto2);
            double angulo = BuscarAngulo(arco.Centro, arco.Punto1, arco.Centro, arco.Punto2, norma1, norma2);
            List<objetos> secue = new List<objetos>();
            if (secuencia.Secuencia.Count != 0)
            {
                foreach (Punto punto in secuencia.Secuencia)
                {
                    double norma = punto.DistanciaPuntos(arco.Centro, punto);
                    double angulo2 = BuscarAngulo(arco.Centro, arco.Punto1, arco.Centro, punto, norma1, norma);
                    if (angulo2 <= (double)angulo)
                    {
                        secue.Add(punto);
                    }
                }
            }
           
            return new SecuenciaFinita(secue);

        }
        public static SecuenciaFinita interceptarCircunferenciaArco(Circunferencia circunferencia, Arco arco) { return interceptarArcoYCircunferencia(arco, circunferencia); }
        public static SecuenciaFinita interceptarArcoYRecta(Arco arco, Recta recta)
        {
            Circunferencia circunferencia2 = new Circunferencia(arco.Centro, arco.MedidaRadio);
            SecuenciaFinita secuencia = interceptarRectaYCircunferencia(recta, circunferencia2);
            double norma1 = arco.Centro.DistanciaPuntos(arco.Centro, arco.Punto1);
            double norma2 = arco.Centro.DistanciaPuntos(arco.Centro, arco.Punto2);
            double angulo = BuscarAngulo(arco.Centro, arco.Punto1, arco.Centro, arco.Punto2, norma1, norma2);
            List<objetos> secue = new List<objetos>();
            if (secuencia.Secuencia.Count != 0)
            {
                foreach (Punto punto in secuencia.Secuencia)
                {
                    double norma = punto.DistanciaPuntos(arco.Centro, punto);
                    double angulo2 = BuscarAngulo(arco.Centro, arco.Punto1, arco.Centro, punto, norma1, norma);
                    if (angulo2 <= (double)angulo)
                    {
                        secue.Add(punto);
                    }
                }
            }
            return new SecuenciaFinita(secue);

        }
        public static SecuenciaFinita interceptarRectaYArco(Recta recta, Arco arco) {
            return interceptarArcoYRecta(arco, recta); }
        public static SecuenciaFinita interceptarArcoYRayo(Arco arco, Rayo rayo)
        {
            Circunferencia circunferencia2 = new Circunferencia(arco.Centro, arco.MedidaRadio);
            SecuenciaFinita secuencia =interceptarRayoYcircunferencia(rayo, circunferencia2);
            double norma1 = arco.Centro.DistanciaPuntos(arco.Centro, arco.Punto1);
            double norma2 = arco.Centro.DistanciaPuntos(arco.Centro, arco.Punto2);
            double angulo = BuscarAngulo(arco.Centro, arco.Punto1, arco.Centro, arco.Punto2, norma1, norma2);
            List<objetos> secue = new List<objetos>();
            if (secuencia.Secuencia.Count != 0)
            {
                foreach (Punto punto in secuencia.Secuencia)
                {
                    double norma = punto.DistanciaPuntos(arco.Centro, punto);
                    double angulo2 = BuscarAngulo(arco.Centro, arco.Punto1, arco.Centro, punto, norma1, norma);
                    if (angulo2 <= (double)angulo)
                    {
                        secue.Add(punto);
                    }
                }
            }
            return new SecuenciaFinita(secue);


        }
        public static SecuenciaFinita interceptarRayoYArco(Rayo rayo, Arco arco) { return interceptarArcoYRayo(arco, rayo); }
        public static SecuenciaFinita interceptarArcoYarco(Arco arco1, Arco arco2)
        {
            Circunferencia circunferencia1 = new Circunferencia(arco1.Centro, arco1.MedidaRadio);
            Circunferencia circunferencia2 = new Circunferencia(arco2.Centro, arco2.MedidaRadio);
            SecuenciaFinita secuencia = interceptarCircunferenciaYCircunferencia(circunferencia1, circunferencia2);
            double norma1arco1 = arco1.Centro.DistanciaPuntos(arco1.Centro, arco1.Punto1);
            double norma2arco1= arco1.Centro.DistanciaPuntos(arco1.Centro, arco1.Punto2);
            double norma1arco2 = arco2.Centro.DistanciaPuntos(arco2.Centro, arco2.Punto1);
            double norma2arco2 = arco2.Centro.DistanciaPuntos(arco2.Centro, arco2.Punto2);
            double angulo1 = BuscarAngulo(arco1.Centro, arco1.Punto1, arco1.Centro, arco1.Punto2, norma1arco1, norma2arco1);
            double angulo2 = BuscarAngulo(arco2.Centro, arco2.Punto1, arco2.Centro, arco2.Punto2, norma1arco2, norma2arco2);
            List<objetos> secue = new List<objetos>();
            if (secuencia.Secuencia.Count != 0)
            {
                foreach (Punto punto in secuencia.Secuencia)
                {
                    double norma1arco = punto.DistanciaPuntos(arco1.Centro, punto);
                    double norma2arco = punto.DistanciaPuntos(arco2.Centro, punto);
                    double anguloauxiliar1 = BuscarAngulo(arco1.Centro, arco1.Punto1, arco1.Centro, punto, norma1arco, norma1arco1);
                    double angulo2auxiliar2 = BuscarAngulo(arco2.Centro, arco2.Punto1, arco2.Centro, punto, norma2arco, norma1arco2);
                    if (anguloauxiliar1 <= (double)angulo1 && angulo2auxiliar2 <= (double)angulo2)
                    {
                        secue.Add(punto);
                    }
                }
            }
            
             return new SecuenciaFinita(secue);
        }
        #endregion
    }

    #endregion



}
