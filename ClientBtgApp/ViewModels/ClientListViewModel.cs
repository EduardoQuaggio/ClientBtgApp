using ClientBtgApp.Base.Models;
using ClientBtgApp.Base.Repository.Interfaces;
using ClientBtgApp.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace ClientBtgApp.ViewModels;

[QueryProperty(nameof(_reload), "reload")]
public partial class ClientListViewModel : ObservableObject, IQueryAttributable
{
    #region Properties
    private readonly IClientRepository _clientRepository;

    [ObservableProperty]
    ObservableCollection<Client> _clients;

    [ObservableProperty]
    bool _isEmpty;

    bool _reload;
    #endregion

    #region Commands
    [RelayCommand]
    public async Task OpenAddEditClient(Client? client)
    {
        try
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                {"client",  client ?? new Client()}
            };
            await Shell.Current.GoToAsync(nameof(ClientEditAddPage), parameters: parameters);
        }
        catch (Exception e)
        {
            throw e;
        }
    }
    #endregion

    public ClientListViewModel(IClientRepository clientRepository)
    {
        _clientRepository = clientRepository;
        OnLoading();
    }

    public async void OnLoading()
    {
        try
        {
            Clients = new ObservableCollection<Client>(_clientRepository.GetAll());
        }
        catch (Exception e )
        {
            throw e;
        }
        finally
        {
            IsEmpty = Clients.Count() == 0;
        }
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        OnLoading();
    }
}