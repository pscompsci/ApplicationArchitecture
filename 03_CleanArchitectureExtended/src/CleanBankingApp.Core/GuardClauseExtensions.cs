using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace CleanBankingApp.Core
{
    public static class GuardClauseExtensions
    {
        public static T Null<T>(
            this IGuardClause guardClause,
            [NotNull][ValidatedNotNull] T input,
            string parameterName
        )
        {
            if (input is null)
            {
                throw new ArgumentNullException(parameterName);
            }
            return input;
        }

        public static string NullOrEmpty(
            this IGuardClause guardClause,
            [NotNull][ValidatedNotNull] string? input,
            string parameterName
        )
        {
            Guard.Against.Null(input, parameterName);
            if (input == string.Empty)
            {
                throw new ArgumentNullException(
                    $"Required input {parameterName} is empty", parameterName);
            }
            return input;
        }

        public static IEnumerable<T> NullOrEmpty<T>(
            this IGuardClause guardClause,
            [NotNull, ValidatedNotNull] IEnumerable<T>? input,
            string parameterName
        )
        {
            Guard.Against.Null(input, parameterName);
            if (!input.Any())
            {
                throw new ArgumentException($"Required input {parameterName} was empty.", parameterName);
            }

            return input;
        }

        public static string NullOrWhitespace(
            this IGuardClause guardClause,
            [NotNull][ValidatedNotNull] string? input,
            string parameterName
        )
        {
            Guard.Against.NullOrEmpty(input, parameterName);
            if (string.IsNullOrWhiteSpace(input))
            {
                throw new ArgumentException(
                    $"Required input {parameterName} is empty", parameterName);
            }

            return input;
        }

        public static int OutOfRange(
            this IGuardClause guardClause,
            int input,
            string parameterName,
            int rangeFrom,
            int rangeTo
        )
        {
            return OutOfRange<int>(
                guardClause, input, parameterName, rangeFrom, rangeTo);
        }

        public static DateTime OutOfRange(
            this IGuardClause guardClause,
            DateTime input,
            string parameterName,
            DateTime rangeFrom,
            DateTime rangeTo
        )
        {
            return OutOfRange<DateTime>(
                guardClause, input, parameterName, rangeFrom, rangeTo);
        }

        public static decimal OutOfRange(
            this IGuardClause guardClause,
            decimal input,
            string parameterName,
            decimal rangeFrom,
            decimal rangeTo
        )
        {
            return OutOfRange<decimal>(
                guardClause, input, parameterName, rangeFrom, rangeTo);
        }

        public static short OutOfRange(
            this IGuardClause guardClause,
            short input,
            string parameterName,
            short rangeFrom,
            short rangeTo
        )
        {
            return OutOfRange<short>(
                guardClause, input, parameterName, rangeFrom, rangeTo);
        }

        public static double OutOfRange(
            this IGuardClause guardClause,
            double input,
            string parameterName,
            double rangeFrom,
            double rangeTo
        )
        {
            return OutOfRange<double>(
                guardClause, input, parameterName, rangeFrom, rangeTo);
        }

        public static float OutOfRange(
            this IGuardClause guardClause,
            float input,
            string parameterName,
            float rangeFrom,
            float rangeTo
        )
        {
            return OutOfRange<float>(
                guardClause, input, parameterName, rangeFrom, rangeTo);
        }

        private static T OutOfRange<T>(
            this IGuardClause guardClause,
            T input,
            string parameterName,
            T rangeFrom,
            T rangeTo
        )
        {
            Comparer<T> comparer = Comparer<T>.Default;

            if (comparer.Compare(rangeFrom, rangeTo) > 0)
            {
                throw new ArgumentException(
                    $"{nameof(rangeFrom)} should be less or equal than {nameof(rangeTo)}");
            }

            if (comparer.Compare(input, rangeFrom) < 0 ||
                comparer.Compare(input, rangeTo) > 0)
            {
                throw new ArgumentOutOfRangeException(parameterName,
                    $"Input {parameterName} was out of range");
            }
            return input;
        }

        public static long Zero(
            this IGuardClause guardClause,
            long input,
            string parameterName
        )
        {
            return Zero<long>(guardClause, input, parameterName);
        }

        public static decimal Zero(
            this IGuardClause guardClause,
            decimal input,
            string parameterName
           )
        {
            return Zero<decimal>(guardClause, input, parameterName);
        }

        public static float Zero(
            this IGuardClause guardClause,
            float input,
            string parameterName
        )
        {
            return Zero<float>(guardClause, input, parameterName);
        }

        public static double Zero(
            this IGuardClause guardClause,
            double input,
            string parameterName
        )
        {
            return Zero<double>(guardClause, input, parameterName);
        }

        private static T Zero<T>(
            this IGuardClause guardClause,
            T input,
            string parameterName
        ) where T : struct
        {
            if (EqualityComparer<T>.Default.Equals(input, default(T)))
            {
                throw new ArgumentException(
                    $"Required input {parameterName} cannot be zero.",
                    parameterName);
            }
            return input;
        }

        public static int Negative(
            this IGuardClause guardClause,
            int input,
            string parameterName
        )
        {
            return Negative<int>(guardClause, input, parameterName);
        }

        public static long Negative(
            this IGuardClause guardClause,
            long input,
            string parameterName
        )
        {
            return Negative<long>(guardClause, input, parameterName);
        }

        public static decimal Negative(
            this IGuardClause guardClause,
            decimal input,
            string parameterName
        )
        {
            return Negative<decimal>(guardClause, input, parameterName);
        }

        public static float Negative(
            this IGuardClause guardClause,
            float input,
            string parameterName
        )
        {
            return Negative<float>(guardClause, input, parameterName);
        }

        public static double Negative(
            this IGuardClause guardClause,
            double input,
            string parameterName
        )
        {
            return Negative<double>(guardClause, input, parameterName);
        }

        private static T Negative<T>(
            this IGuardClause guardClause,
            T input,
            string parameterName
        ) where T : struct, IComparable
        {
            if (input.CompareTo(default(T)) < 0)
            {
                throw new ArgumentException(
                    $"Required input {parameterName} cannot be negative.",
                    parameterName);
            }

            return input;
        }

        public static int NegativeOrZero(
            this IGuardClause guardClause,
            int input,
            string parameterName
        )
        {
            return NegativeOrZero<int>(guardClause, input, parameterName);
        }

        public static long NegativeOrZero(
            this IGuardClause guardClause,
            long input,
            string parameterName
        )
        {
            return NegativeOrZero<long>(guardClause, input, parameterName);
        }

        public static decimal NegativeOrZero(
            this IGuardClause guardClause,
            decimal input,
            string parameterName
        )
        {
            return NegativeOrZero<decimal>(guardClause, input, parameterName);
        }

        public static float NegativeOrZero(
            this IGuardClause guardClause,
            float input,
            string parameterName
        )
        {
            return NegativeOrZero<float>(guardClause, input, parameterName);
        }

        public static double NegativeOrZero(
            this IGuardClause guardClause,
            double input,
            string parameterName
        )
        {
            return NegativeOrZero<double>(guardClause, input, parameterName);
        }

        private static T NegativeOrZero<T>(
            this IGuardClause guardClause,
            T input,
            string parameterName
        ) where T : struct, IComparable
        {
            if (input.CompareTo(default(T)) <= 0)
            {
                throw new ArgumentException(
                    $"Required input {parameterName} cannot be zero or negative.",
                    parameterName);
            }
            return input;
        }

        public static int OutOfRange<T>(
            this IGuardClause guardClause,
            int input,
            string parameterName
        ) where T : struct, Enum
        {
            if (!Enum.IsDefined(typeof(T), input))
            {
                throw new InvalidEnumArgumentException(
                    $"Required input {parameterName} was not a valid enum value for {typeof(T)}.");
            }
            return input;
        }

        public static T OutOfRange<T>(
            this IGuardClause guardClause,
            T input,
            string parameterName
        ) where T : struct, Enum
        {
            if (!Enum.IsDefined(typeof(T), input))
            {
                throw new InvalidEnumArgumentException(
                    $"Required input {parameterName} was not a valid enum value for {typeof(T)}.");
            }
            return input;
        }

        public static T Default<T>(
            this IGuardClause guardClause,
            [AllowNull, NotNull] T input,
            string parameterName
        )
        {
            if (EqualityComparer<T>.Default.Equals(input, default(T)!) ||
                input is null)
            {
                throw new ArgumentException(
                    $"Parameter [{parameterName}] is default value for type {typeof(T).Name}",
                    parameterName);
            }

            return input;
        }
    }
}
