using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGFA
{
    internal class Veiculo
    {
        private int _quilometragem;
        private double _nivelCombustivel;

        public string Matricula { get; set; }
        public int CapacidadeDeposito { get; set; }

        // Propriedade da quilometragem
        public int Quilometragem
        {
            get
            {
                return _quilometragem;
            }
            set
            {
                // A quilometragem nao pode diminuir
                if (value >= _quilometragem)
                {
                    _quilometragem = value;
                }
                else
                {
                    Console.WriteLine("ERRO: A quilometragem nao pode diminuir!");
                }
            }
        }

        // Propriedade do nivel de combustivel
        public double NivelCombustivel
        {
            get
            {
                return _nivelCombustivel;
            }
            set
            {
                // Verificar se o combustivel e valido
                if (value < 0 || value > CapacidadeDeposito)
                {
                    Console.WriteLine("ERRO: Nivel de combustivel invalido!");
                }
                else
                {
                    _nivelCombustivel = value;

                    // Verificar se o combustivel esta abaixo de 10%
                    if (_nivelCombustivel < CapacidadeDeposito * 0.10)
                    {
                        ReservaAtingida?.Invoke(this, EventArgs.Empty);
                    }
                }
            }
        }

        // Evento de alerta de combustivel
        public event EventHandler ReservaAtingida;

        public Veiculo(string matricula, int capacidadeDeposito)
        {
            if (string.IsNullOrWhiteSpace(matricula))
            {
                throw new ArgumentException("A matricula e obrigatoria.");
            }

            if (capacidadeDeposito <= 0)
            {
                throw new ArgumentException("A capacidade deve ser maior que zero.");
            }

            Matricula = matricula;
            CapacidadeDeposito = capacidadeDeposito;

            _quilometragem = 0;

            // O deposito comeca cheio
            _nivelCombustivel = capacidadeDeposito;
        }

        // Metodo para abastecer
        public void Abastecer(double litros)
        {
            if (litros <= 0)
            {
                Console.WriteLine("ERRO: Deve colocar uma quantidade positiva!");
                return;
            }

            double novoNivel = NivelCombustivel + litros;

            if (novoNivel > CapacidadeDeposito)
            {
                Console.WriteLine("AVISO: O deposito ficou cheio. Nao foi possivel colocar todo o combustivel.");

                NivelCombustivel = CapacidadeDeposito;
            }
            else
            {
                NivelCombustivel = novoNivel;
                Console.WriteLine("Abastecimento realizado com sucesso!");
            }
        }

        // Metodo para realizar uma viagem
        public bool RealizarViagem(int kmDistancia)
        {
            if (kmDistancia <= 0)
            {
                Console.WriteLine("ERRO: A distancia tem de ser maior que zero!");
                return false;
            }

            double combustivelNecessario = kmDistancia * 0.07;

            // Verificar se existe combustivel suficiente
            if (NivelCombustivel >= combustivelNecessario)
            {
                Quilometragem = Quilometragem + kmDistancia;

                NivelCombustivel = NivelCombustivel - combustivelNecessario;

                Console.WriteLine("Viagem realizada com sucesso!");

                return true;
            }
            else
            {
                Console.WriteLine("ERRO: Combustivel insuficiente!");

                return false;
            }
        }
    }
}
