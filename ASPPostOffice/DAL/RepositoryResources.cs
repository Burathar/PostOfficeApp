using System;
using System.Globalization;

namespace DAL
{
    public static class RepositoryResources
    {
        public static int Int32(this object input)
        {
            int output;
            try
            {
                output = Convert.ToInt32(input.ToString());
            }
            catch (FormatException)
            {
                throw new DalException($"Could not fetch an item: Input \"{input.ToString()}\" could not be converted to an Int32", new FormatException());
            }
            catch (OverflowException)
            {
                throw new DalException($"Could not fetch an item: Input \"{input.ToString()}\" could not be converted to an Int32", new OverflowException());
            }
            return output;
        }

        public static int? Int32N(this object input)
        {
            if (input == null) return null;
            if (input.ToString() == "") return null;
            return Int32(input);
        }

        public static bool Bool(this object input)
        {
            if (input.ToString() == "") return false;
            bool output;
            try
            {
                output = Convert.ToBoolean(input);
            }
            catch (FormatException)
            {
                throw new DalException($"Could not fetch an item: Input \"{input.ToString()}\" could not be converted to a boolean", new FormatException());
            }
            catch (InvalidCastException)
            {
                throw new DalException($"Could not fetch an item: Input \"{input.ToString()}\" could not be converted to a boolean", new InvalidCastException());
            }
            return output;
        }

        public static string String(this object input)
        {
            return input.ToString();
        }

        public static string CheckedString(this object input)
        {
            string output = input.ToString();
            if (string.IsNullOrEmpty(output)) throw new DalException("Could not fetch an item", new ArgumentException("String is Null or Empty", output));
            return output;
        }

        public static float Float(this object input)
        {
            float output;
            try
            {
                output = float.Parse(input.ToString(), NumberStyles.Any, CultureInfo.InvariantCulture);
            }
            catch (FormatException)
            {
                throw new DalException($"Could not fetch an item: Input \"{input.ToString()}\" could not be converted to a float", new FormatException());
            }
            catch (OverflowException)
            {
                throw new DalException($"Could not fetch an item: Input \"{input.ToString()}\" could not be converted to a float", new OverflowException());
            }
            return output;
        }

        public static float? FloatN(this object input)
        {
            if (input.ToString() == "") return null;
            return Float(input);
        }

        public static decimal Decimal(this object input)
        {
            decimal output;
            try
            {
                output = Convert.ToDecimal(input.ToString());
            }
            catch (FormatException)
            {
                throw new DalException($"Could not fetch an item: Input \"{input.ToString()}\" could not be converted to a decimal", new FormatException());
            }
            catch (OverflowException)
            {
                throw new DalException($"Could not fetch an item: Input \"{input.ToString()}\" could not be converted to a decimal", new OverflowException());
            }
            return output;
        }

        public static decimal? DecimalN(this object input)
        {
            if (input.ToString() == "") return null;
            return Decimal(input);
        }

        public static DateTime MakeDateTime(this object input)
        {
            string inputString = input.ToString();
            if (inputString == "") throw new DalException("Could not fetch an item", new ArgumentException("String is Null or Empty"));
            DateTime output;
            try
            {
                output = DateTime.Parse(inputString, CultureInfo.CurrentCulture);
            }
            catch (FormatException)
            {
                throw new DalException($"Could not fetch an item: Input \"{inputString}\" could not be converted to a DateTime format", new FormatException());
            }
            return output;
        }

        public static Byte[] MakeByteArray(this object input)
        {
            if (input == null) throw new DalException("Could not fetch an item", new ArgumentNullException("Input was Null"));
            Byte[] output;
            try
            {
                output = (Byte[])input;
            }
            catch (InvalidCastException)
            {
                throw new DalException($"Could not fetch an item: Input \"{input.ToString()}\" could not be converted to a Byte Array", new InvalidCastException());
            }
            return output;
        }
    }
}