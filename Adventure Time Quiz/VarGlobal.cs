using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace Adventure_Time_Quiz
{
    class VarGlobal
    {
        //VETTORE IN CUI CONSERVO GLI ID PER EVITARE CHE SI FACCIANO DUE VOLTE LE STESSE DOMANDE
        public static int[] vettore = new int[105];
        //VARIABILE PER IL PUNTEGGIO
        public static int punteggio = 0;
        //VARIABILE PER IL PUNTEGGIOT
        public static int punteggiot = 0;
        //Variabile indice dom
        public static int dom = 0;
        //Variabile indice domt
        public static int domt = 0;
        //Variabile modalità
        public static int mod = 0;
        //punti duello
        public static int punti1 = 0;

        public static int punti2 = 0;


        public static int A = 0;
        public static int B = 0;
        public static int C = 0;
        public static int D = 0;
        public static int E = 0;

    }
}
