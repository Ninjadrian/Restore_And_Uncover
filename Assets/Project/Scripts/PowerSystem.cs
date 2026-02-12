using System;
using UnityEngine;

public static class PowerSytem
{
    public static bool HasPower {  get; private set; } = false;

    public static event Action<bool> OnPowerChanged;

    public static void SetPower(bool value)
    {
        if (HasPower == value) return;

        HasPower = value;
        OnPowerChanged?.Invoke(value);
    }
}
