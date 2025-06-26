using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Mango.Client.Web.Models;

public class ErrorViewModel
{
    public string? RequestId { get; set; }

    public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
}


public sealed class Index : IEquatable<Index>
{
    public int Id { get; }

    public string Value { get; set; } = string.Empty;

    public override int GetHashCode()
    {
        return Id;
    }

    public bool Equals(Index? other)
    {
        return other is not null && Id == other;
    }

    public override bool Equals(object? obj)
    {
        return obj is not null && obj.GetType() == GetType() && Equals((Index)obj);
    }

    public static implicit operator int(Index entry)
    {
        return entry.Id;
    }

    public override string ToString()
    {
        return Value;
    }

    public static bool operator ==(Index? left, Index? right)
    {
        if (left is null)
        {
            if (right is null)
            {
                return true;
            }

            return false;
        }

        return left.Equals(right);
    }

    public static bool operator !=(Index? left, Index? right)
    {
        return !(left == right);
    }
}

public sealed class Indexes : IEnumerable<Index>
{
    private readonly IList<Index> _data;

    private readonly Action<string?>? _propertyChanged;

    private readonly string? _propertyName;

    public Indexes(
        Action<string?>? propertyChanged,
        string? propertyName
    )
    {
        _data = new List<Index>();
        _propertyChanged = propertyChanged;
        _propertyName = propertyName;
    }

    public string? this[int index]
    {
        get
        {
            return _data
                .Where(item => item == index)
                .Select(item => item.ToString())
                .FirstOrDefault();
        }
    }

    public void Add(Index entry)
    {
        if (_data.Any(item => item != entry))
        {
            _data.Add(entry);
            if(_propertyChanged != null)
                _propertyChanged(_propertyName);
        }
    }

    public IEnumerator<Index> GetEnumerator()
    {
        return _data.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}


public sealed class MainViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    public void OnPropertyChanged([CallerMemberName] string? member = null)
    {
        if (PropertyChanged != null)
        {
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(member));
        }
    }

    public readonly Indexes _columns;

    public readonly IList<Indexes> _rows;

    public MainViewModel()
    {
        _columns = new Indexes(
            OnPropertyChanged,
            nameof(Columns)
        );

        _rows = new List<Indexes>();
    }

    public Indexes Columns
    {
        get { return _columns; }
    }

    public IEnumerable<Indexes> Rows
    {
        get { return _rows; }
    }
}