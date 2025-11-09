using EstacionamentoDIO.Models;

namespace EstacionamentoDIO.Services
{
    public class EstacionamentoService
    {
        private readonly List<Veiculo> _veiculos = new();
        private readonly decimal _valorInicial;
        private readonly decimal _valorHora;

        public EstacionamentoService(decimal valorInicial, decimal valorHora)
        {
            _valorInicial = valorInicial;
            _valorHora = valorHora;
        }

        public bool AdicionarVeiculo(string placa)
        {
            placa = placa.Trim().ToUpper();
            if (string.IsNullOrWhiteSpace(placa)) return false;
            if (_veiculos.Any(v => v.Placa == placa)) return false;
            _veiculos.Add(new Veiculo(placa));
            return true;
        }

        public (bool sucesso, decimal valorTotal) RemoverVeiculo(string placa)
        {
            placa = placa.Trim().ToUpper();
            var veiculo = _veiculos.FirstOrDefault(v => v.Placa == placa);
            if (veiculo == null) return (false, 0m);

            var tempo = DateTime.Now - veiculo.HoraEntrada;
            var horas = (int)Math.Ceiling(tempo.TotalHours);
            if (horas < 1) horas = 1;

            var total = _valorInicial + (horas * _valorHora);

            _veiculos.Remove(veiculo);
            return (true, total);
        }

        public IReadOnlyList<Veiculo> ListarVeiculos() => _veiculos.AsReadOnly();
    }
}
