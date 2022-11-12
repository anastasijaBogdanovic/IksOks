using IksOks.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IksOksLab
{
    public class GameLogic : IGameLogic
    {
        public Potez VratiPotez(ContextIksOks c, int dubina)
        {
            if (dubina == 0 || c.GetListaMogucihPoteza().Count == 0)
            {
                c.Value = Evaluate(c);
                return null;
            }

            if (c.NaPotezu == 1)
            {
                int bestVrednost = -10000;
                Potez bestPotez = null;
                foreach (Potez potez in c.GetListaMogucihPoteza())
                {
                    Potez p = VratiPotez(potez.NarednoStanje, dubina--);
                    if (potez.NarednoStanje.Value > bestVrednost)
                    {
                        bestVrednost = potez.NarednoStanje.Value;
                        bestPotez = potez;
                    }

                }
                c.Value = bestVrednost;
                return bestPotez;

            }

            else
            {
                int bestVrednost = 10000;
                Potez bestPotez = null;
                foreach (Potez potez in c.GetListaMogucihPoteza())
                {
                    Potez p = VratiPotez(potez.NarednoStanje, dubina--);
                    if (potez.NarednoStanje.Value < bestVrednost)
                    {
                        bestVrednost = potez.NarednoStanje.Value;
                        bestPotez = potez;
                    }

                }
                c.Value = bestVrednost;
                return bestPotez;
            }
        }
        public int Evaluate(ContextIksOks c)
        {
            int n = 0;

            if (c.TrenutnoStanje.DaLiJeKraj(out n))
            {
                if (n == 1)
                    return 1000;
                else return -1000;
            }

            else
            {
                
               int vrednost = 0;

                for(int i=0;i<3;i++)
                {
                    vrednost += Jedan(c.TrenutnoStanje.Polje(i, 0), c.TrenutnoStanje.Polje(i, 1), c.TrenutnoStanje.Polje(i, 2),c.NaPotezu);
                    vrednost += Jedan(c.TrenutnoStanje.Polje(0,i), c.TrenutnoStanje.Polje(1,i), c.TrenutnoStanje.Polje(2,i),c.NaPotezu);

                }
                vrednost += Jedan(c.TrenutnoStanje.Polje(0,0), c.TrenutnoStanje.Polje(1,1), c.TrenutnoStanje.Polje(2,2),c.NaPotezu);
                vrednost += Jedan(c.TrenutnoStanje.Polje(0,2), c.TrenutnoStanje.Polje(1,1), c.TrenutnoStanje.Polje(2,0),c.NaPotezu);

                return vrednost;

            }

        }


        public int Jedan(int a, int b, int c, int potez)
        {
            int k = a * 100 + b * 10 + c;

            switch (k)
            {
                case 0:
                    {
                        if (potez == 1)
                            return 3;
                        return 1;
                    }
                case 100:
                case 10:
                case 1:
                    {
                        if (potez == 1)
                            return 30;
                        else return 10;
                        
                    }
                case 200:
                case 20:
                case 2:
                    {
                        if (potez == 2)
                            return -30;
                       else  return -10;
                        
                    }
                case 110:
                case 101:
                case 11:
                    {
                        if (potez == 1)
                            return 300;
                       else  return 100;
                        
                    }
                case 220:
                case 202:
                case 22:
                    {
                        if (potez == 2)
                            return -300;
                        return -100;
                        
                    }
                default: return 0;
            }
        }
    }
}
