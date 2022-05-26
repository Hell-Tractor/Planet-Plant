// UNUSED
using System;

/// <summary>
/// Base class of crop property.
/// </summary>
public abstract class CropProperty {
    // property value
    public int Value { get; protected set; }
    protected int _maxValue = 100;

    public void Add(int value) {
        Value = Math.Clamp(Value + value, 0, _maxValue);
    }

    public void Set(int value) {
        Value = Math.Clamp(value, 0, _maxValue);
    }

    public abstract void Init();
}

public class WaterProperty : CropProperty {
    public bool IsAquatic = false;

    public override void Init() {
        Value = IsAquatic ? 100 : 50;
    }
}

public class PestProperty : CropProperty {
    public override void Init() {
        _maxValue = 1;
        Value = 0;
    }

    public bool HasPest() {
        return Value > 0;
    }
}

public class WeedProperty : CropProperty {
    public override void Init() {
        _maxValue = 1;
        Value = 0;
    }

    public bool HasWeed() {
        return Value > 0;
    }
}

public class FertilityProperty : CropProperty {
    public override void Init() {
        Value = 50;
    }
}