using System;
using System.Collections.Generic;
using System.Text;

public class NumberToWordsConverter
{
    private static readonly string[] Units = { "", "один", "два", "три", "четыре", "пять", "шесть", "семь", "восемь", "девять" };
    private static readonly string[] UnitsFemale = { "", "одна", "две", "три", "четыре", "пять", "шесть", "семь", "восемь", "девять" };
    private static readonly string[] Teens = { "десять", "одиннадцать", "двенадцать", "тринадцать", "четырнадцать", "пятнадцать", "шестнадцать", "семнадцать", "восемнадцать", "девятнадцать" };
    private static readonly string[] Tens = { "", "", "двадцать", "тридцать", "сорок", "пятьдесят", "шестьдесят", "семьдесят", "восемьдесят", "девяносто" };
    private static readonly string[] Hundreds = { "", "сто", "двести", "триста", "четыреста", "пятьсот", "шестьсот", "семьсот", "восемьсот", "девятьсот" };
    private static readonly string[] Thousands = { "", "тысяча", "тысячи", "тысяч" };
    private static readonly string[] Millions = { "", "миллион", "миллиона", "миллионов" };
    private static readonly string[] Billions = { "", "миллиард", "миллиарда", "миллиардов" };

    public static void Main()
    {
        Console.Write("Введите целое число: ");
        string input = Console.ReadLine();

        string result = ConvertNumberToWords(input);
        Console.WriteLine(result);
    }

    public static string ConvertNumberToWords(string input)
    {
        if (!long.TryParse(input, out long number))
        {
            return "Ошибка: введите корректное целое число.";
        }

        if (number == 0)
        {
            return "ноль";
        }

        bool isNegative = number < 0;
        number = Math.Abs(number);

        string result = ConvertPositiveNumber(number);

        if (isNegative)
        {
            result = "минус " + result;
        }

        return result;
    }

    private static string ConvertPositiveNumber(long number)
    {
        if (number == 0)
        {
            return "ноль";
        }

        var parts = new List<string>();

        // Разбиваем на разряды по 3 цифры
        int billion = (int)(number / 1000000000);
        int million = (int)((number / 1000000) % 1000);
        int thousand = (int)((number / 1000) % 1000);
        int unit = (int)(number % 1000);

        if (billion > 0)
        {
            parts.Add(ConvertThreeDigits(billion, false) + " " + GetDeclension(billion, Billions));
        }

        if (million > 0)
        {
            parts.Add(ConvertThreeDigits(million, false) + " " + GetDeclension(million, Millions));
        }

        if (thousand > 0)
        {
            parts.Add(ConvertThreeDigits(thousand, true) + " " + GetDeclension(thousand, Thousands));
        }

        if (unit > 0)
        {
            parts.Add(ConvertThreeDigits(unit, false));
        }

        return string.Join(" ", parts);
    }

    private static string ConvertThreeDigits(int number, bool isFemale)
    {
        var words = new List<string>();

        int hundreds = number / 100;
        int tens = (number / 10) % 10;
        int units = number % 10;

        if (hundreds > 0)
        {
            words.Add(Hundreds[hundreds]);
        }

        if (tens == 1)
        {
            words.Add(Teens[units]);
        }
        else
        {
            if (tens > 1)
            {
                words.Add(Tens[tens]);
            }

            if (units > 0)
            {
                if (isFemale)
                {
                    words.Add(UnitsFemale[units]);
                }
                else
                {
                    words.Add(Units[units]);
                }
            }
        }

        return string.Join(" ", words);
    }

    private static string GetDeclension(int number, string[] declensions)
    {
        int lastDigit = number % 10;
        int lastTwoDigits = number % 100;

        if (lastTwoDigits >= 11 && lastTwoDigits <= 19)
        {
            return declensions[3]; // тысяч, миллионов и т.д.
        }

        switch (lastDigit)
        {
            case 1:
                return declensions[1]; // тысяча, миллион, миллиард
            case 2:
            case 3:
            case 4:
                return declensions[2]; // тысячи, миллиона, миллиарда
            default:
                return declensions[3]; // тысяч, миллионов и т.д.
        }
    }
}