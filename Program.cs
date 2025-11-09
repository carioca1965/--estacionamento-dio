using EstacionamentoDIO.Services;

Console.OutputEncoding = System.Text.Encoding.UTF8;
Console.WriteLine("=== Estacionamento DIO - Projeto Pronto ===\n");

// Valores de exemplo
decimal valorInicial = 5.00m;
decimal valorHora = 2.50m;

var estacionamento = new EstacionamentoService(valorInicial, valorHora);

while (true)
{
    Console.WriteLine("\nEscolha uma opção:");
    Console.WriteLine("1 - Adicionar veículo");
    Console.WriteLine("2 - Remover veículo");
    Console.WriteLine("3 - Listar veículos");
    Console.WriteLine("0 - Sair");
    Console.Write("Opção: ");
    var opc = Console.ReadLine();

    switch (opc)
    {
        case "1":
            Console.Write("Digite a placa do veículo: ");
            var placaAdicionar = Console.ReadLine();
            if (estacionamento.AdicionarVeiculo(placaAdicionar!))
                Console.WriteLine($"Veículo {placaAdicionar!.ToUpper()} adicionado!");
            else
                Console.WriteLine("Erro ao adicionar. Placa inválida ou já cadastrada.");
            break;

        case "2":
            Console.Write("Digite a placa para remover: ");
            var placaRemover = Console.ReadLine();
            var resultado = estacionamento.RemoverVeiculo(placaRemover!);
            if (!resultado.sucesso)
                Console.WriteLine("Veículo não encontrado.");
            else
            {
                Console.WriteLine($"Valor a pagar: {resultado.valorTotal:C}");
            }
            break;

        case "3":
            var lista = estacionamento.ListarVeiculos();
            if (!lista.Any())
                Console.WriteLine("Não há veículos.");
            else
                foreach (var v in lista)
                    Console.WriteLine($"- {v.Placa} (Entrada: {v.HoraEntrada})");
            break;

        case "0":
            return;
        default:
            Console.WriteLine("Opção inválida.");
            break;
    }
}
