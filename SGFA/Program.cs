using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGFA
{
    internal class Program
    {
        // Lista onde ficam guardados os veiculos
        static List<Veiculo> frota = new List<Veiculo>();
        static void Main(string[] args)
        {
            int opcao = 0;

            while (opcao != 6)
            {
                Console.WriteLine();
                Console.WriteLine("=================================");
                Console.WriteLine("     SISTEMA DE GESTAO DE FROTA");
                Console.WriteLine("=================================");
                Console.WriteLine("1 - Adicionar veiculo");
                Console.WriteLine("2 - Mostrar veiculos");
                Console.WriteLine("3 - Abastecer veiculo");
                Console.WriteLine("4 - Realizar viagem");
                Console.WriteLine("5 - Consultar um veiculo");
                Console.WriteLine("6 - Sair");
                Console.WriteLine("=================================");

                Console.Write("Escolhe uma opcao: ");

                // Verificar se a opcao e um numero
                if (!int.TryParse(Console.ReadLine(), out opcao))
                {
                    Console.WriteLine("ERRO: Escolhe um numero!");
                    continue;
                }

                switch (opcao)
                {
                    case 1:
                        AdicionarVeiculo();
                        break;

                    case 2:
                        MostrarVeiculos();
                        break;

                    case 3:
                        AbastecerVeiculo();
                        break;

                    case 4:
                        FazerViagem();
                        break;

                    case 5:
                        ConsultarVeiculo();
                        break;

                    case 6:
                        Console.WriteLine("A sair do programa...");
                        break;

                    default:
                        Console.WriteLine("Opcao invalida!");
                        break;
                }
            }
        }

        static void AdicionarVeiculo()
        {
            try
            {
                Console.Write("Introduz a matricula: ");
                string matricula = Console.ReadLine();

                Console.Write("Introduz a capacidade do deposito (litros): ");

                if (!int.TryParse(Console.ReadLine(), out int capacidade))
                {
                    Console.WriteLine("ERRO: Capacidade invalida!");
                    return;
                }

                // Criar o veiculo
                Veiculo novoVeiculo = new Veiculo(matricula, capacidade);

                // Ligar o evento ao metodo de alerta
                novoVeiculo.ReservaAtingida += AlertaCombustivel;

                // Guardar o veiculo na lista
                frota.Add(novoVeiculo);

                Console.WriteLine("Veiculo adicionado com sucesso!");
            }
            catch (ArgumentException erro)
            {
                Console.WriteLine("ERRO: " + erro.Message);
            }
        }

        // Mostrar todos os veiculos
        static void MostrarVeiculos()
        {
            if (frota.Count == 0)
            {
                Console.WriteLine("Ainda nao existem veiculos.");
                return;
            }

            Console.WriteLine();
            Console.WriteLine("------ LISTA DE VEICULOS ------");

            for (int i = 0; i < frota.Count; i++)
            {
                Console.WriteLine("Veiculo numero: " + (i + 1));
                Console.WriteLine("Matricula: " + frota[i].Matricula);
                Console.WriteLine("Quilometragem: " + frota[i].Quilometragem + " km");
                Console.WriteLine("Combustivel: " + frota[i].NivelCombustivel + " litros");
                Console.WriteLine("Capacidade: " + frota[i].CapacidadeDeposito + " litros");
                Console.WriteLine("-------------------------------");
            }
        }

        // Abastecer um veiculo
        static void AbastecerVeiculo()
        {
            Veiculo veiculo = EscolherVeiculo();

            if (veiculo == null)
            {
                return;
            }

            Console.Write("Quantos litros queres colocar? ");

            if (!double.TryParse(Console.ReadLine(), out double litros))
            {
                Console.WriteLine("ERRO: Quantidade invalida!");
                return;
            }

            veiculo.Abastecer(litros);
        }

        // Fazer uma viagem
        static void FazerViagem()
        {
            Veiculo veiculo = EscolherVeiculo();

            if (veiculo == null)
            {
                return;
            }

            Console.Write("Quantos quilometros queres percorrer? ");

            if (!int.TryParse(Console.ReadLine(), out int km))
            {
                Console.WriteLine("ERRO: Distancia invalida!");
                return;
            }

            veiculo.RealizarViagem(km);
        }

        // Consultar um veiculo
        static void ConsultarVeiculo()
        {
            Veiculo veiculo = EscolherVeiculo();

            if (veiculo == null)
            {
                return;
            }

            Console.WriteLine();
            Console.WriteLine("------ INFORMACAO DO VEICULO ------");
            Console.WriteLine("Matricula: " + veiculo.Matricula);
            Console.WriteLine("Quilometragem: " + veiculo.Quilometragem + " km");
            Console.WriteLine("Combustivel: " + veiculo.NivelCombustivel + " litros");
            Console.WriteLine("Capacidade: " + veiculo.CapacidadeDeposito + " litros");
        }

        // Escolher um veiculo da lista
        static Veiculo EscolherVeiculo()
        {
            if (frota.Count == 0)
            {
                Console.WriteLine("Nao existem veiculos na frota!");
                return null;
            }

            MostrarVeiculos();

            Console.Write("Escolhe o numero do veiculo: ");

            if (!int.TryParse(Console.ReadLine(), out int numero))
            {
                Console.WriteLine("Numero invalido!");
                return null;
            }

            if (numero < 1 || numero > frota.Count)
            {
                Console.WriteLine("Esse veiculo nao existe!");
                return null;
            }

            return frota[numero - 1];
        }

        // Metodo chamado quando o evento e disparado
        static void AlertaCombustivel(object sender, EventArgs e)
        {
            Console.ForegroundColor = ConsoleColor.Red;

            Console.WriteLine();
            Console.WriteLine("!!!!!!!!!! ALERTA !!!!!!!!!!");
            Console.WriteLine("ATENCAO: O veiculo esta na reserva!");
            Console.WriteLine("O combustivel esta abaixo dos 10%!");
            Console.WriteLine("!!!!!!!!!!!!!!!!!!!!!!!!!!!!");

            // Voltar a cor normal
            Console.ResetColor();
        }
    }
}
