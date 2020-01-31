using System;
using System.ComponentModel;

namespace WPFPokerGame.Models
{
    public abstract class ModelBase : INotifyPropertyChanged
    {
        public bool IsChanged {get; set;}
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
