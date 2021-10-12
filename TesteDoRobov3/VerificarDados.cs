using System;
using System.Collections.Generic;
using System.Text;

namespace TesteDoRobov3
{
    public class VerificarDados   // classe criada para verificar se os dados inseridos atendem aos parâmetros exigidos
    {
        public int verificar(string dado, int limites)
        {
            int vdado;          // valor numérico do dado inserido
            int retorno = 23;        // valor que desejo retornar

            bool result = int.TryParse(dado, out vdado);         // verifica se o dado é um número
            if (result == true)
            {
                if (vdado >= 0 && vdado <= limites) retorno = 1; // retornar 1 para tudo ok
                else retorno = 2;                                // retornar 2 para extrapolação dos limites
            }
            else retorno = 3;                                    // retornar 3 para dado não numérico
            return retorno;
        }
    }
}
