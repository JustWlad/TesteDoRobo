using System;
using System.Collections.Generic; // adicionado para se poder usar List

namespace TesteDoRobov3
{         
    class Program
    {
        static void Main(string[] args)
        {
            VerificarDados V = new VerificarDados(); // instância feita para poder usar as propriedades da classe verificar dados
            Robo R = new Robo();                     // instância feita para poder usar as propriedades da classe robô

            int x = -1;                              // valor numérico da largura da horta -> referência ao plano cartesiano
            int y = -1;                              // valor numérico do comprimento da horta -> referência ao plano cartesiano
            int n = -1;                              // valor numérico do número de canteiros existentes
            int xR = -1;                              // posição x do robô
            int yR = -1;                              // posição y do robô
            int permissao = 0;                       // valor utilizado para futuros loops
            string vRobo;                            // visão do robo
            
            // Informações da horta
            do                                       // loop para garantir os parâmetros desejados
            {
                Console.WriteLine("\nInsira a largura x (número inteiro), sentido oeste->leste, da horta: "); // orientações ao usuário
                string largura = Console.ReadLine();                                                         // declaração de variável apenas para leitura de dados
                int.TryParse(largura, out x);                                                               // conversão do dado lido, em string, para seu valor numérico, em int, se este for válido.
            } while (x < 0);                                                                               // garante que o loop continue até o dado inserido for um número inteiro positivo.

            do
            {
                Console.WriteLine("\nInsira o comprimento y(número inteiro), sentido sul->norte, da horta: ");
                string comprimento = Console.ReadLine();
                int.TryParse(comprimento, out y);
            } while (y < 0);

            do
            {
                Console.WriteLine("\nInserir o número de canteiros: ");
                string canteiros = Console.ReadLine();
                int.TryParse(canteiros, out n);
            } while (n <= 0);                                             // <=0 para garantir que não haja "0" canteiros.

            int[] xC = new int[n];                                        // uso de array para garantir que a memória reserve espaço suficiente para guardar o valor numérico x das posições dos n canteiros
            int[] yC = new int[n];                                        // uso de array para garantir que a memória reserve espaço suficiente para guardar o valor numérico y das posições dos n canteiros

            // Informações dos canteiros
            for (int i = 1; i <= n; i++)                                  // loop para garantir a leitura dos locais xy dos n canteiros
            {
                do
                {
                    Console.WriteLine($"\nInserir posição x, oeste->leste, do canteiro {i}: ");
                    string xCanteiro = Console.ReadLine();
                    permissao = V.verificar(xCanteiro, x);                                                     // recepção do retorno de verificar dados
                    if (permissao == 1) xC[i - 1] = int.Parse(xCanteiro);                                      // ação se estiver tudo ok
                    if (permissao == 2) Console.WriteLine("\nO canteiro deve estar dentro da área da horta."); // ação para extrapolação dos limites
                    if (permissao == 3) Console.WriteLine("\nO valor digitado deve ser um número inteiro.");   // ação para dado não numérico

                } while (permissao != 1);

                permissao = 0;             // permissão resetada para ser usada novamente

                do
                {
                    Console.WriteLine($"\nInserir posição y, sul->norte, do canteiro {i}: ");
                    string yCanteiro = Console.ReadLine();
                    permissao = V.verificar(yCanteiro, y);
                    if (permissao == 1) yC[i - 1] = int.Parse(yCanteiro);
                    if (permissao == 2) Console.WriteLine("\nO canteiro deve estar dentro da área da horta.");
                    if (permissao == 3) Console.WriteLine("\nO valor digitado deve ser um número inteiro.");

                } while (permissao != 1);
                permissao = 0;
            }

            // Informações do robô
            do
            {
                Console.WriteLine("\nInserir a posição x do robô: ");
                string xRobo = Console.ReadLine();
                permissao = V.verificar(xRobo, x);
                if (permissao == 1) xR = int.Parse(xRobo);
                if (permissao == 2) Console.WriteLine("\nO robô deve estar dentro da área da horta.");
                if (permissao == 3) Console.WriteLine("\nO valor digitado deve ser um número inteiro.");

            } while (permissao != 1);

            permissao = 0;

            do
            {
                Console.WriteLine("\nInserir a posição y do robô: ");
                string yRobo = Console.ReadLine();
                permissao = V.verificar(yRobo, y);
                if (permissao == 1) yR = int.Parse(yRobo);
                if (permissao == 2) Console.WriteLine("\nO robô deve estar dentro da área da horta.");
                if (permissao == 3) Console.WriteLine("\nO valor digitado deve ser um número inteiro.");

            } while (permissao != 1);

            permissao = 0;

            do //Evita erros de digitação
            {
                Console.WriteLine("\nPra onde o robô tá virado? Norte, sul, leste ou oeste?"); vRobo = Console.ReadLine();
                vRobo = vRobo.ToLower();
                if (vRobo != "norte" && vRobo != "sul" && vRobo != "leste" && vRobo != "oeste") Console.WriteLine("\nNão entendi...");
                else permissao = 1;

            } while (permissao != 1);


            // Informações sobre a irrigação
            List<int> cIrrigar = new List<int>();  // "contagem de irrigar" - uso de list para o usuário decidir livremente quantas irrigações quer realizar

            for (int a = 0; a != 4;)               //verifica se o usuário seguiu todas as instruções
            {
                Console.WriteLine("\nDigite o número do canteiro que deseja irrigar e aperte enter. Se não deseja irrigar mais nenhum canteiro, digite 0");
                string press = Console.ReadLine();
                a = V.verificar(press, n);
                if (a == 1 && press != "0") cIrrigar.Add(Int32.Parse(press));
                if (a == 2) Console.WriteLine($"\nValores não aceitos, existem só {n} canteiros.");
                if (a == 3) Console.WriteLine("\nDigite números válidos e pressione 0 quando acabar");
                if (press == "0") a = 4;
            }
            if (cIrrigar.Count == 1) cIrrigar[0] = 1;      // impede o programa dar erro se o usuário só digitar "0"

            // Movimentação do robô
            int[] xD = new int[cIrrigar.Count];            // prepara a variável xD de acordo com o número de irrigações pedidas
            int[] yD = new int[cIrrigar.Count];            // prepara a variável yD de acordo com o número de irrigações pedidas

            for (int i = 0; i < cIrrigar.Count; i++)       // garante que o robô realize o número de irrigações pedidas
            {
                xD[i] = xC[cIrrigar[i] - 1] - xR;          // "Distância x" - garante o quanto o robô tem que se mover, em x, para chegar no canteiro indicado por cIrrigar -> "Final menos inicial"
                yD[i] = yC[cIrrigar[i] - 1] - yR;          // "Distância y" - garante o quanto o robô tem que se mover, em y, para chegar no canteiro indicado por cIrrigar -> "Final menos inicial"


                if (xD[i] == 0 && yD[i] == 0) R.irrigar(); // garante que o robô realize a ação de irrigar se estiver no canteiro

                if (xD[i] < 0)                             // se a distância é negativa, no eixo x, o robô precisa se movimentar para oeste
                {
                    switch (vRobo)                         // garante que o robô vire para oeste dependendo pra onde está sua visão
                    {
                        case "norte":
                            R.esquerda();
                            break;

                        case "sul":
                            R.direita();
                            break;

                        case "leste":
                            R.volta();
                            break;

                        case "oeste":
                            break;
                    }

                    while (xD[i] != 0)                         // enquanto a distância no eixo x for diferente de 0, o robô deve se mover
                    {
                        R.mover();
                        xR--;                                  // robô se mexeu
                        xD[i] = xC[cIrrigar[i] - 1] - xR;
                    }
                    if (yD[i] == 0) R.irrigar();               // se xD e yD = 0, entao o robô está na horta                   
                    vRobo = "oeste";                           // atualiza para onde a visão do robô está virada
                }
                else if (xD[i] > 0)                            // se a distância é posivita no eixo x, então o robô deve se mover para leste
                {
                    switch (vRobo)
                    {
                        case "norte":
                            R.direita();
                            break;

                        case "sul":
                            R.esquerda();
                            break;

                        case "leste":
                            break;

                        case "oeste":
                            R.volta();
                            break;
                    }

                    while (xD[i] != 0)
                    {
                        R.mover();
                        xR++;
                        xD[i] = xC[cIrrigar[i] - 1] - xR;
                    }
                    if (yD[i] == 0) R.irrigar();
                    vRobo = "leste";
                }

                if (yD[i] < 0)                               // se a distância é negativa no eixo y, o robô precisa se mover para o sul
                {
                    switch (vRobo)
                    {
                        case "norte":
                            R.volta();
                            break;

                        case "sul":
                            break;

                        case "leste":
                            R.direita();
                            break;

                        case "oeste":
                            R.esquerda();
                            break;
                    }

                    while (yD[i] != 0)
                    {
                        R.mover();
                        yR--;
                        yD[i] = yC[cIrrigar[i] - 1] - yR;
                    }
                    if (yD[i] == 0) R.irrigar();
                    vRobo = "sul";
                }
                else if (yD[i] > 0)                              // se a distância é positiva no eixo y, o robô deve se mover para o norte
                {
                    switch (vRobo)
                    {
                        case "norte":
                            break;

                        case "sul":
                            R.volta();
                            break;

                        case "leste":
                            R.esquerda();
                            break;

                        case "oeste":
                            R.direita();
                            break;
                    }

                    while (yD[i] != 0)
                    {
                        R.mover();
                        yR++;
                        yD[i] = yC[cIrrigar[i] - 1] - yR;
                    }
                    if (yD[i] == 0) R.irrigar();
                    vRobo = "norte";
                }
            }
        }
    }
}