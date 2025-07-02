using ClientBtgApp.Base.Models;
using ClientBtgApp.Base.Repository.Interfaces;
using ClientBtgApp.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Graphics.Text;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace ClientBtgApp.ViewModels
{
    [QueryProperty(nameof(Client), "client")]
    public partial class ClientEditAddViewModel: ObservableObject, IQueryAttributable
    {
        #region Properties
        private readonly IClientRepository _clientRepository;

        [ObservableProperty]
        Client _client;

        [ObservableProperty]
        string _titlePage = "Add new client";

        [ObservableProperty]
        string _error;

        [ObservableProperty]
        bool _formsInvalid;

        [ObservableProperty]
        string _age;

        [ObservableProperty]
        bool _edit;
        #endregion

        #region Commands
        [RelayCommand]
        public async Task SaveClient()
        {
            if (ValidateForms())
            {
                if (Edit)
                {
                    _clientRepository.Update(Client);
                    await Shell.Current.DisplayAlert("Warning", "Client edited successfully", "Ok");
                }
                else
                {
                    _clientRepository.Insert(Client);
                    await Shell.Current.DisplayAlert("Warning", "Client created successfully", "Ok");
                }

               
                BackAndReload();
            }
        }

        [RelayCommand]
        public async Task RemoveClient()
        {
            if (await Shell.Current.DisplayAlert("Warning", "Do you want to delete this client?", "Yes", "No"))
            {
                _clientRepository.Delete(Client);

                await Shell.Current.DisplayAlert("Warning", "Client successfully removed", "Ok");
                BackAndReload();
            }
            
        }
        #endregion

        public ClientEditAddViewModel(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            var client = query["client"] as Client;

            if (client.Name is null)
                TitlePage = "Add new client";
            else
            {
                TitlePage = "Edit client";
                Edit = true;
                Age = client.Age.ToString();
            }
        }
       
        public bool ValidateForms()
        {
            try
            {
                Error = null;
                FormsInvalid = false;

                if (Client.Name == string.Empty || Client.Name == null)
                    Error += "- Client Name is empty";
                else if (!Regex.IsMatch(Client.Name, @"^[a-zA-Z ]+$"))
                    Error += "- Client name contains number";

                if (Client.Lastname == string.Empty || Client.Name == null)
                    Error += "\n- Client Lastname is empty";
                else if (!Regex.IsMatch(Client.Lastname, @"^[a-zA-Z ]+$"))
                    Error += "\n- Client Lastname contains number";

                if (Age == null)
                    Error += "\n- Client Age is empty";
                else if (Age.Length == 1 && !Regex.IsMatch(Age, @"^[1-9]+$"))
                    Error += "\n- Client Age invalid";
                else if (Age.Length == 2 && !Regex.IsMatch(Age, @"^[1-9][0-9]+$"))
                    Error += "\n- Client Age invalid";
                else if (Age.Length == 3 && !Regex.IsMatch(Age, @"^[1-9][0-9][0-9]+$"))
                    Error += "\n- Client Age invalid";
                else
                    Client.Age = int.Parse(Age);

                if (Client.Address == string.Empty || Client.Address == null)
                    Error += "\n- Client Address is empty";

                if (Error != null)
                {
                    FormsInvalid = true;
                    return false;
                }

                    return true;
            }
            catch (Exception)
            { 
                return false;
                throw;
            }
        }

        public async void BackAndReload()
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                {"reload",  true}
            };
            await Shell.Current.GoToAsync("..", parameters: parameters);
        }
    }
}
