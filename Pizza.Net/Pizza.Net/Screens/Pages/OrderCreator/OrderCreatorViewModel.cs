﻿using System.Collections.Generic;
using System.Windows.Input;

namespace Pizza.Net.Screens.Pages
{
    class OrderCreatorViewModel : ChangingPagesBaseViewModel, IPageViewModel
    {
        public OrderCreatorViewModel(IOrderCreatorModel orderCreatorModel, IClientsPageViewModel clientsPageViewModel)
        {
            _orderCreatorModel = orderCreatorModel;

            PageViewModels.Add(clientsPageViewModel);
            _nextStepButtonContents.Add("Add client");
            PageViewModels.Add(new PizzasPageViewModel(null));
            _nextStepButtonContents.Add("Add pizzas");
            //PageViewModels.Add(new OverviewAndFinishOrder());
            _nextStepButtonContents.Add("Submit Order");

            AdvanceToNextStep();
        }

        private List<string> _nextStepButtonContents = new List<string>();
        private int _currentStepIndex = 0;
        private IOrderCreatorModel _orderCreatorModel;

        private string _nextStepButtonContent;
        public string NextStepButtonContent
        {
            get
            {
                return _nextStepButtonContent;
            }
            set
            {
                if(value != _nextStepButtonContent)
                {
                    _nextStepButtonContent = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _isPreviousStepButtonEnabled;
        public bool IsPreviousStepButtonEnabled
        {
            get
            {
                return _isPreviousStepButtonEnabled;
            }
            set
            {
                if (value != _isPreviousStepButtonEnabled)
                {
                    _isPreviousStepButtonEnabled = value;
                    OnPropertyChanged();
                }
            }
        }

        private ICommand _nextStepCommand;
        public ICommand NextStepCommand
        {
            get
            {
                if (_nextStepCommand == null)
                {
                    _nextStepCommand = new RelayCommand(
                        execute => AdvanceToNextStep());
                }

                return _nextStepCommand;
            }
        }

        private ICommand _previousCommand;
        public ICommand PreviousCommand
        {
            get
            {
                if (_previousCommand == null)
                {
                    _previousCommand = new RelayCommand(
                        execute => GoToPreviousStep());
                }

                return _previousCommand;
            }
        }

        private ICommand _resetCommand;
        public ICommand ResetCommand
        {
            get
            {
                if (_resetCommand == null)
                {
                    _resetCommand = new RelayCommand(
                        execute => Reset());
                }

                return _resetCommand;
            }
        }

        public string PageName
        {
            get
            {
                return "Order Creator";
            }
        }

        private void AdvanceToNextStep()
        {
            switch(_currentStepIndex)
            {
                case 0:
                    break;
                case 1:
                    var client = (CurrentPageViewModel as ClientsPageViewModel).SelectedClient;
                    if (client == null)
                        break;
                    _orderCreatorModel.AddClient(client);
                    break;
                case 2:
                    ICollection<Pizza.Net.Domain.Pizza> pizzas = null;
                    if (pizzas == null)
                        break;
                    _orderCreatorModel.AddPizza(pizzas);
                    break;
                case 3:
                    _orderCreatorModel.SubmitOrder();
                    Reset();
                    return;
            }
            GoToNextStep();
        }

        private void GoToNextStep()
        {
            _currentStepIndex++;
            CurrentPageViewModel = PageViewModels[_currentStepIndex - 1];
            NextStepButtonContent = _nextStepButtonContents[_currentStepIndex - 1];
            ManagePreviousButtonStatus();
        }

        private void GoToPreviousStep()
        {
            _currentStepIndex--;
            CurrentPageViewModel = PageViewModels[_currentStepIndex - 1];
            NextStepButtonContent = _nextStepButtonContents[_currentStepIndex - 1];
            ManagePreviousButtonStatus();
        }

        private void ManagePreviousButtonStatus()
        {
            if (_currentStepIndex == 0)
                IsPreviousStepButtonEnabled = false;
            else
                IsPreviousStepButtonEnabled = true;
        }

        private void Reset()
        {
            _currentStepIndex = 0;
            AdvanceToNextStep();
        }
    }
}