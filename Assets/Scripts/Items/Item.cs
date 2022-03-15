using UnityEngine;

[System.Serializable]
public class Item
{
    const int Max = 99; // max number of items

    public int number { get; private set; } // how many?

    public enum Type
    {
        none,
        puzzle,
        paw,
        rabbit,
        spade,
        sun
    }

    public Type type { get; private set; } // what kind of item?

    public Item(Type _type = Type.none, int _number = 0)
    {
        Set(_type, _number);
    }

    public void Set(Type _type) { type = _type; }
    public void Set(int _number) { number = _number; }
    public void Set(Type _type, int _number) { type = _type; number = _number; }

    public bool IsValid() { return type != Type.none && number > 0; }
    public void Clear() { type = Type.none; number = 0; }

    public int Plus(int plus)
    {
        number += plus;

        if (number > Max)
        {
            int leftover = number - Max;

            number = Max;

            return leftover;
        }
        if (number < 0)
        {
            int leftover = number;

            number = 0;

            return leftover;
        }

        return 0;
    }


    public static Type GetRandomType()
    {
        var values = System.Enum.GetValues(typeof(Type));

        return (Type)Random.Range(1, values.Length);
    }


}
