using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace EFDemo.Web.Models;

public partial class Artist : INotifyPropertyChanged
{
    private string? _name;
    public int ArtistId { get; set; }

    public string? Name
    {
        get => _name;
        set => SetField(ref _name, value);
    }

    public virtual ObservableCollection<Album> Albums { get; set; }// = new HashSet<Album>();
    
    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}
