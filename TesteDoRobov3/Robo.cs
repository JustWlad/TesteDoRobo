using System;
using System.Threading;             // adicionado para poder usar o delay
using System.Collections.Generic;
using System.Text;

namespace TesteDoRobov3
{
    public class Robo                            // declaração da classe relacionada ao robô
    {
        public void mover()                      // método relacionado à movimentação
        {
            Console.WriteLine("Robo Move(M)");
            Thread.Sleep(1000);                  // delay adicionado apenas para efeitos visuais no cmd
        }
        public void irrigar()
        {
            Console.WriteLine("Irrigar(I)");
            Thread.Sleep(1000);
        }

        public void direita()                    // métodos relacionados à direção
        {
            Console.WriteLine("Robo vira pra Direita(D)");
            Thread.Sleep(1000);
        }
        public void esquerda()
        {
            Console.WriteLine("Robo vida pra Esquerda(E)");
            Thread.Sleep(1000);
        }
        public void volta()
        {
            Console.WriteLine("Robo vira duas vezes pra Direita(DD)");
            Thread.Sleep(1000);
        }
    }
}
