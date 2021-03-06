namespace ThirteenBase;

/// <summary>
/// Решение с кэшированием вариантов чисел с заданной суммой цифр.
/// Кэш собирается на лету в словарь.
/// Кэш ограничен "верхним" уровнем окончательных результатов расчета
/// </summary>
public sealed class OnlineCacheTopLevel : SolutionBase
{

    /// <summary>
    /// Словарь для кэширования результатов расчета.
    /// </summary>
    private readonly Dictionary<int, int> _cache =
        new(TaskParameters.Default.Base * TaskParameters.Default.LeftRightPartLength);
        // ↑ преаллоцируем словарь исходя из настроек "по умолчанию"

    /// <summary>
    /// Подсчитывает кол-во вариантов чисел заданной длины и с заданной суммой цифр
    /// </summary>
    /// <param name="length">Длина числа</param>
    /// <param name="sum">Сумма цифр</param>
    /// <returns>Кол-во вариантов</returns>
    internal override int CountNumbersWithGivenSumOfDigits(int length, int sum)
    {
        if (length == TaskParameters.LeftRightPartLength)
        {
            if (_cache.TryGetValue(sum, out var count))
            {
                return count;
            }
        }

        if (sum == 0)
        {
            return 1; // с суммой цифр = 0 только один вариант состоящий из всех нулей
        }

        if (length == 0)
        {
            return 0; // с нулевой длиной вариантов нет
        }

        var result = 0;

        for (var i = 0; i < TaskParameters.Base; i++)
        {
            if (sum >= i)
            {
                result += CountNumbersWithGivenSumOfDigits(length - 1, sum - i);
            }
            else
            {
                break;
            }
        }

        if (length == TaskParameters.LeftRightPartLength)
        {
            _cache.Add((sum), result);
        }

        return result;
    }
}
