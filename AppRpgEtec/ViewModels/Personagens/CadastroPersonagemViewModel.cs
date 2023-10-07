using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppRpgEtec.Services.Personagens;
using AppRpgEtec.Models;
using System.Collections.ObjectModel;
using AppRpgEtec.Models.Enuns;
using System.Windows.Input;

namespace AppRpgEtec.ViewModels.Personagens
{
    public class CadastroPersonagemViewModel : BaseViewModel
    {
        private PersonagemService pService;
        private ObservableCollection<TipoClasse> listaTiposClasse;
        private TipoClasse tipoClasseSelecionado;
        public ICommand SalvarCommand { get; }
        public ICommand CancelarCommand { get; set; }

        public CadastroPersonagemViewModel()
        {
            string token = Preferences.Get("UsuarioToken", string.Empty);
            pService = new PersonagemService(token);
            _ = ObterClasses();

            SalvarCommand = new Command(async () => { await SalvarPersonagem(); });
            CancelarCommand = new Command(async => CancelarCadastro());
        }

        private async void CancelarCadastro()
        {
            await Shell.Current.GoToAsync("..");
        }

        public async Task SalvarPersonagem()
        {
            try
            { 
                Personagem model = new Personagem()
                {
                    Nome = this.nome,
                    PontosVida = this.PontosVida,
                    Defesa = this.Defesa,
                    Derrotas = this.Derrotas,
                    Disputas = this.Disputas,
                    Forca = this.Forca,
                    Inteligencia = this.Inteligencia,
                    Vitorias = this.Vitorias,
                    Id = this.Id,
                    Classe = (ClasseEnum)tipoClasseSelecionado.Id

                };
                if (model.Id == 0)
                    await pService.PostPersonagemAsync(model);
                await Application.Current.MainPage
                    .DisplayAlert("Mensagem", "Dados salvos com sucesso", "Ok");
                await Shell.Current.GoToAsync(".."); //Remove a pagina atual da pilha de paginas
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage
                    .DisplayAlert("Ops", ex.Message, "Detalhes " + ex.InnerException, "Ok");
            }
        }

        public TipoClasse TipoClasseSelecionado
        {
            get { return tipoClasseSelecionado; }
            set
            {
                if (value != null)
                {
                    tipoClasseSelecionado = value;
                    OnPropertyChanged();
                }
            }
        }
        public ObservableCollection<TipoClasse> ListaTiposClasse
        {
            get { return listaTiposClasse; }
            set
            {
                if (value != null)
                {
                    listaTiposClasse = value;
                    OnPropertyChanged();
                }
            }
        }

        public async Task ObterClasses()
        {
            try
            {
                ListaTiposClasse = new ObservableCollection<TipoClasse>();
                ListaTiposClasse.Add(new TipoClasse() { Id = 1, Descricao = "Cavaleiro" });
                ListaTiposClasse.Add(new TipoClasse() { Id = 2, Descricao = "Mago" });
                ListaTiposClasse.Add(new TipoClasse() { Id = 3, Descricao = "Clerigo" });
                OnPropertyChanged(nameof(ListaTiposClasse));
            }
            catch(Exception ex)
            {
                await Application.Current.MainPage
                    .DisplayAlert("Ops", ex.Message + "Detalhes: " + ex.InnerException, "Ok");
            }
        }

        private int id;
        private string nome;
        private int pontosVida;
        private int forca;
        private int defesa;
        private int inteligencia;
        private int disputas;
        private int vitorias;
        private int derrotas;

        public int Id
        {
            get => id;
            set 
            { 
              id = value;
              OnPropertyChanged();
            } 
        }

        public string Nome
        {
            get => nome;
            set
            {
                nome = value;
                OnPropertyChanged();
            }
        }
        public int PontosVida
        { 
            get => pontosVida;
            set
            {
                pontosVida = value;
                OnPropertyChanged();
            }
        }
        public int Forca
        {
            get => forca;
            set
            {
                forca = value;
                OnPropertyChanged();
            }
        }
        public int Defesa
        {
            get => defesa;
            set
            {
                defesa = value;
                OnPropertyChanged();
            }
        }
        public int Inteligencia
        { 
            get => inteligencia;
            set
            {
                inteligencia = value;
                OnPropertyChanged();
            }
        }
        public int Disputas
        {
            get => disputas;
            set
            { 
                disputas = value;
                OnPropertyChanged();
            }
        }
        public int Vitorias
        { 
            get => vitorias;
            set
            {
                vitorias = value;
                OnPropertyChanged();
            }
        }
        public int Derrotas
        {
            get => derrotas;
            set
            {
                derrotas = value;
                OnPropertyChanged();
            } 
        }
    }
}
